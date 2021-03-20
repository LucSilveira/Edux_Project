using EduxProject.Contexts;
using EduxProject.Domains;
using EduxProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Repositories
{
    public class ObjetivoAlunoRepository : IObjetivoAluno
    {
        RankingRepository _rankingRepository = new RankingRepository();
        AlunoTurmaRepository _alunoTurmaRepository = new AlunoTurmaRepository();

        private readonly EduxContext _contexto;

        public ObjetivoAlunoRepository()
        {
            _contexto = new EduxContext();
        }

        public List<ObjetivoAluno> ListarObjetivosDosAlunos()
        {
            try
            {
                List<ObjetivoAluno> _listaObjetivos = _contexto.ObjetivoAluno.Include("IdAlunoTurmaNavigation")
                                                            .Include("IdObjetivoNavigation").ToList();

                foreach(ObjetivoAluno _objetivo in _listaObjetivos)
                {
                    _objetivo.IdAlunoTurmaNavigation.ObjetivoAluno = null;
                    _objetivo.IdObjetivoNavigation.ObjetivoAluno = null;
                }

                return _listaObjetivos;
            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public List<ObjetivoAluno> ProcurarAlunosPorObjetivo(int _idObjetivo)
        {
            try
            {
                List<ObjetivoAluno> _listaAlunosObjetivo = _contexto.ObjetivoAluno
                                                        .Where(oba => oba.IdObjetivo == _idObjetivo)
                                                        .Include("IdAlunoTurmaNavigation").ToList();

                foreach(ObjetivoAluno _objetivo in _listaAlunosObjetivo)
                {
                    _objetivo.IdAlunoTurmaNavigation.ObjetivoAluno = null;
                }

                return _listaAlunosObjetivo;
            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public List<ObjetivoAluno> ProcurarObjetivosPorAluno(int _idAluno)
        {
            try
            {
                List<ObjetivoAluno> _listaObjetivosAluno = _contexto.ObjetivoAluno
                                            .Where(oba => oba.IdAlunoTurma == _idAluno)
                                            .Include("IdObjetivoNavigation").ToList();

                foreach(ObjetivoAluno _aluno in _listaObjetivosAluno)
                {
                    _aluno.IdObjetivoNavigation.ObjetivoAluno = null;
                }

                return _listaObjetivosAluno;
            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public ObjetivoAluno BuscarObjetivoDoAlunoPorId(int _idObjetivoAluno)
        {
            try
            {
                return _contexto.ObjetivoAluno.FirstOrDefault(oba => oba.Id == _idObjetivoAluno);
            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public async Task<ObjetivoAluno> CadastrarObjetivosDoAluno(ObjetivoAluno _objetivoAluno)
        {
            try
            {
                _objetivoAluno.DataAlcancado = DateTime.Now;

                await _contexto.ObjetivoAluno.AddAsync(_objetivoAluno);

                await _contexto.SaveChangesAsync();

                //Buscando a turma do aluno de acordo com o objetivo concluido para verificarmos sua nota geral
                AlunoTurma _alunoTurma = _alunoTurmaRepository.BuscarAlunoTurmaPorId((int)_objetivoAluno.IdAlunoTurma);

                //Passando os dados da turma do aluno e da sua nota atingida para computarmos sua nota geral
                await _rankingRepository.SomarNotaRankingAluno(_objetivoAluno, _alunoTurma);

                return _objetivoAluno;
            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public async Task<ObjetivoAluno> AlterarObjetivosDoAluno(ObjetivoAluno _objetivoAluno)
        {
            try
            {
                //Verificando o objetivoAluno previamente cadastrados para verificar as alterações
                ObjetivoAluno _objetivo = BuscarObjetivoDoAlunoPorId(_objetivoAluno.Id);

                //Buscando o objeto alunoTurma de acordo com o IdAlunoTurma do objetivoAluno cadastrado
                AlunoTurma _alunoTurma = _alunoTurmaRepository.BuscarAlunoTurmaPorId((int)_objetivoAluno.IdAlunoTurma);

                //Passando ambos os objetos para o nosso ranking, para reavaliarmos a nota do aluno
                await _rankingRepository.AlterarNotaRankingAluno(_objetivoAluno, _objetivo, _alunoTurma);

                _objetivo.IdAlunoTurma = _objetivoAluno.IdAlunoTurma;
                _objetivo.IdObjetivo = _objetivoAluno.IdObjetivo;
                _objetivo.Nota = _objetivoAluno.Nota;


                _contexto.Entry(_objetivo).State = EntityState.Modified;

                await _contexto.SaveChangesAsync();

                return _objetivo;
            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public async Task<ObjetivoAluno> ExcluirObjetivoDoAluno(int _idObjetivoAluno)
        {
            try
            {
                //Buscando os dados do objetivo excluido para alterarmos a sua nota geral
                ObjetivoAluno _objetivoAlunoProcurada = BuscarObjetivoDoAlunoPorId(_idObjetivoAluno);

                //Buscando os dados da turma do aluno para filtramos os dados do ranking do aluno
                AlunoTurma _alunoTurma = _alunoTurmaRepository.BuscarAlunoTurmaPorId((int)_objetivoAlunoProcurada.IdAlunoTurma);

                //Passando os dados para o sistema de ranking para abatermos a nota excluida a sua media geral
                await _rankingRepository.DiminuirNotaRankingAluno(_objetivoAlunoProcurada, _alunoTurma);

                _contexto.ObjetivoAluno.Remove(_objetivoAlunoProcurada);

                _contexto.SaveChanges();

                return _objetivoAlunoProcurada;
            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }
    }
}
