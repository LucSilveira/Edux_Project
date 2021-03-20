using EduxProject.Contexts;
using EduxProject.Domains;
using EduxProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Repositories
{
    public class RankingRepository : IRanking
    {
        UsuarioRepository _repositoryUsuario = new UsuarioRepository();
        ObjetivoRepository _repositoryObjetivo = new ObjetivoRepository();

        private readonly EduxContext _contexto;

        public RankingRepository()
        {
            _contexto = new EduxContext();
        }

        public List<Ranking> ListarRanking()
        {
            try
            {
                List<Ranking> _listaDeRanking = _contexto.Ranking.OrderByDescending(rkg => rkg.NotaTotal).Include("IdTurmaNavigation").ToList();

                foreach(Ranking _ranking in _listaDeRanking)
                {
                    _ranking.IdTurmaNavigation.Ranking = null;
                }

                return _listaDeRanking;

            }catch(Exception _e)
            {
                throw new Exception(_e.Message);
            }
        }

        public List<Ranking> ListarRankingDaTurma(int _idTurma)
        {
            try
            {
                List<Ranking> _rankingDaTurma = _contexto.Ranking.Where(rkg => rkg.IdTurma == _idTurma)
                                                                   .OrderByDescending(rkg => rkg.NotaTotal)
                                                                   .Include("IdTurmaNavigation").ToList();

                foreach(Ranking _ranking in _rankingDaTurma)
                {
                    _ranking.IdTurmaNavigation.Ranking = null;
                }

                return _rankingDaTurma;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public Ranking BuscarRankingDoAluno(string _nomeAluno)
        {
            try
            {
                return _contexto.Ranking.FirstOrDefault(rkg => rkg.NomeAluno == _nomeAluno);

            }catch(Exception _e)
            {
                throw new Exception(_e.Message);
            }
        }

        public Ranking AdicionarAlunoAoRanking(AlunoTurma _aluno)
        {
            try
            {
                Ranking _ranking = new Ranking();

                //Pegando os dados de alunoTurma e aprovetando o IdUsuario para buscar as informações do
                //próprio usuário;
                Usuario _alunoCadastrado = _repositoryUsuario.BuscarUsuarioPorId((int)_aluno.IdUsuario);

                //Atribuindo o nome de usuário e sua turma ao seu ranking
                _ranking.NomeAluno = _alunoCadastrado.Nome;
                _ranking.IdTurma = _aluno.IdTurma;

                //Adicionando  ranking ao banco de dados
                _contexto.Ranking.Add(_ranking);

                _contexto.SaveChanges();

                return _ranking;

            }catch(Exception _e)
            {
                throw new Exception(_e.Message);
            }
        }

        public async Task<Ranking> SomarNotaRankingAluno(ObjetivoAluno _aluno, AlunoTurma _alunoTurma)
        {
            try
            {
                //Buscando os dados do Usuario de acordo com o IdUsuairo inserindo no objeto alunoTurma
                Usuario usuario = _repositoryUsuario.BuscarUsuarioPorId((int)_alunoTurma.IdUsuario);

                //Procurando o ranking do aluno para poder alterar sua nota
                Ranking _rankingProcurado = BuscarRankingDoAluno(usuario.Nome);

                //Adicionando a nota atribuida ao objetivo concluido a sua nota geral do ranking
                _rankingProcurado.NotaTotal += _aluno.Nota;

                //Buscando os dados do Objetivo concluido para verficarmos se o mesmo é um objetivo oculto
                Objetivo _objetivo = _repositoryObjetivo.BuscarObjetivoPorId((int)_aluno.IdObjetivo);

                //Caso seja um objetivo oculto, somaremos a quantidade de objetivos oculto concluidos do aluno
                if(_objetivo.IdCategoria == 3)
                {
                    _rankingProcurado.ObjetivosOculto += 1;
                }

                //Salvando as alterações da nota do aluno
                _contexto.Entry(_rankingProcurado).State = EntityState.Modified;

                await _contexto.SaveChangesAsync();

                return _rankingProcurado;

            }catch(Exception _e)
            {
                throw new Exception(_e.Message);
            }
        }

        public async Task<Ranking> AlterarNotaRankingAluno(ObjetivoAluno _objetivoNovo, ObjetivoAluno _objetivoAntigo, AlunoTurma _alunoTurma)
        {
            try
            {
                //Buscando os dados do Usuario de acordo com o IdUsuairo inserindo no objeto alunoTurma
                Usuario usuario = _repositoryUsuario.BuscarUsuarioPorId((int)_alunoTurma.IdUsuario);

                //Procurando o ranking do aluno para poder alterar sua nota
                Ranking _rankingProcurado = BuscarRankingDoAluno(usuario.Nome);

                //Calculo para verificar a diferença da nota corrigida e fazer a correção da nota geral do aluno
                /*
                    Caso a nota corrigida seja maior que a nota anterior o sistema vai verificar a diferança entre as duas

                    exemplo: "Nota antiga" = 90, "Nova nota" = 50, a direfença das duas (90 - 50) será = 40, onde
                    o resultado da diferença será abatido do valor total (NotaTotal - 40);
                 */
                _rankingProcurado.NotaTotal -= (_objetivoAntigo.Nota - _objetivoNovo.Nota);
                
                //Verificando os dados do objetivo que contem as alterações
                Objetivo _objetivoNovoProcurado = _repositoryObjetivo.BuscarObjetivoPorId((int)_objetivoNovo.IdObjetivo);

                //Verificando os dados do objetivo alterado
                Objetivo _objetivoAntigoProcurado = _repositoryObjetivo.BuscarObjetivoPorId((int)_objetivoAntigo.IdObjetivo);

                //Verificando se o objetivo que foi alterado era um objetivo oculto, e o novo objetivo cujo foi
                //alterado, não é mais um objetivo oculto, para que assim possamos corrigir a quantidade de objetivos
                //ocultos encontrados pelo aluno
                if(_objetivoNovoProcurado.IdCategoria != 3 && _objetivoAntigoProcurado.IdCategoria == 3)
                {
                    _rankingProcurado.ObjetivosOculto -= 1;
                }

                //Salvando os dados corrigidos
                _contexto.Entry(_rankingProcurado).State = EntityState.Modified;

                await _contexto.SaveChangesAsync();

                return _rankingProcurado;

            }
            catch(Exception _e)
            {
                throw new Exception(_e.Message);
            }
        }

        public async Task<Ranking> DiminuirNotaRankingAluno(ObjetivoAluno _aluno, AlunoTurma _alunoTurma)
        {
            try
            {
                //Buscando os dados do Usuario de acordo com o IdUsuairo inserindo no objeto alunoTurma
                Usuario usuario = _repositoryUsuario.BuscarUsuarioPorId((int)_alunoTurma.IdUsuario);

                //Procurando o ranking do aluno para poder alterar sua nota
                Ranking _rankingProcurado = BuscarRankingDoAluno(usuario.Nome);

                //Verificando a nota do objetivo excluido e abatendo seu valor da nota total do aluno
                _rankingProcurado.NotaTotal -= _aluno.Nota;

                //Buscando os dados do Objetivo concluido para verficarmos se o mesmo é um objetivo oculto
                Objetivo _objetivo = _repositoryObjetivo.BuscarObjetivoPorId((int)_aluno.IdObjetivo);

                //Caso seja um objetivo oculto, subtrairemos a quantidade de objetivos oculto concluidos do aluno
                if (_objetivo.IdCategoria == 3)
                {
                    _rankingProcurado.ObjetivosOculto -= 1;
                }

                //Salvando os dados corrigidos
                _contexto.Entry(_rankingProcurado).State = EntityState.Modified;

                await _contexto.SaveChangesAsync();

                return _rankingProcurado;
            }
            catch(Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }
    }
}
