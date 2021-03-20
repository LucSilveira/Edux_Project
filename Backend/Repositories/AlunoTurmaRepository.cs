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
    public class AlunoTurmaRepository : IAlunoTurma
    {
        RankingRepository _repositoryRanking = new RankingRepository();

        //Instânciando nossa conexão com o banco de dados
        private readonly EduxContext _contexto;

        //Defindo o método construtor, para sempre que a classe AlunoTurmaRepository seja instânciada
        //ela possa realizar a conexão com o nosso banco de dados;
        public AlunoTurmaRepository()
        {
            _contexto = new EduxContext();
        }

        public List<AlunoTurma> ListarTurmasDosAlunos()
        {
            try
            {
                List<AlunoTurma> _listaAlunos = _contexto.AlunoTurma.Include("IdUsuarioNavigation")
                                                    .Include("IdTurmaNavigation")
                                                    .Include("IdTurmaNavigation.IdCursoNavigation").ToList();

                foreach (AlunoTurma _aluno in _listaAlunos)
                {
                    _aluno.IdTurmaNavigation.AlunoTurma = null;
                    _aluno.IdUsuarioNavigation.AlunoTurma = null;
                    _aluno.IdUsuarioNavigation.ProfessorTurma = null;
                    _aluno.IdTurmaNavigation.IdCursoNavigation.Turma = null;
                }

                return _listaAlunos;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public List<AlunoTurma> ProcurarTurmasDoAluno(int _idAluno)
        {
            try
            {
                List<AlunoTurma> _listaAlunos = _contexto.AlunoTurma
                                    .Where(usr => usr.IdUsuario == _idAluno)
                                    .Include("IdTurmaNavigation")
                                    .Include("IdTurmaNavigation.IdCursoNavigation").ToList();

                foreach(AlunoTurma _aluno in _listaAlunos)
                {
                    _aluno.IdTurmaNavigation.AlunoTurma = null;
                    _aluno.IdTurmaNavigation.IdCursoNavigation.Turma = null;
                }

                return _listaAlunos;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public List<AlunoTurma> ProcurarAlunosDaTurma(int _idTurma)
        {
            try
            {
                List<AlunoTurma> _listaAlunos = _contexto.AlunoTurma
                                    .Where(usr => usr.IdTurma == _idTurma)
                                    .Include("IdUsuarioNavigation").ToList();

                foreach (AlunoTurma _aluno in _listaAlunos)
                {
                    _aluno.IdUsuarioNavigation.AlunoTurma = null;
                }

                return _listaAlunos;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public AlunoTurma BuscarAlunoTurmaPorId(int _idAlunoTurma)
        {
            try
            {
                return _contexto.AlunoTurma.FirstOrDefault(atm => atm.Id == _idAlunoTurma);

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public AlunoTurma CadastrarTurmasDoAluno(AlunoTurma _alunoTurma)
        {
            try
            {
                _contexto.AlunoTurma.Add(_alunoTurma);

                _contexto.SaveChanges();

                _repositoryRanking.AdicionarAlunoAoRanking(_alunoTurma);

                return _alunoTurma;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public AlunoTurma AlterarTurmaDoAluno(AlunoTurma _alunoTurma)
        {
            try
            {
                _contexto.Entry(_alunoTurma).State = EntityState.Modified;

                _contexto.SaveChanges();

                return _alunoTurma;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public void ExcluirTurmaDoAluno(int _idAlunoTurma)
        {
            try
            {
                AlunoTurma _alunoTurmaProcurado = BuscarAlunoTurmaPorId(_idAlunoTurma);

                //_contexto.ObjetivoAluno.RemoveRange(_contexto.ObjetivoAluno.Where(oba => oba.IdAlunoTurma == _idAlunoTurma));

                _contexto.AlunoTurma.Remove(_alunoTurmaProcurado);

                _contexto.SaveChanges();

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }
    }
}
