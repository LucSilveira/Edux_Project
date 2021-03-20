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
    public class ProfessorTurmaRepository : IProfessorTurma
    {
        //Instânciando nossa conexão com o banco de dados
        private readonly EduxContext _contexto;

        //Defindo o método construtor, para sempre que a classe AlunoTurmaRepository seja instânciada
        //ela possa realizar a conexão com o nosso banco de dados;
        public ProfessorTurmaRepository()
        {
            _contexto = new EduxContext();
        }

        public List<ProfessorTurma> ListarTurmasDosProfessores()
        {
            try
            {
                List<ProfessorTurma> _listaProfessores = _contexto.ProfessorTurma.Include("IdUsuarioNavigation")
                                                    .Include("IdTurmaNavigation")
                                                    .Include("IdTurmaNavigation.IdCursoNavigation").ToList();

                foreach (ProfessorTurma _professor in _listaProfessores)
                {
                    _professor.IdTurmaNavigation.ProfessorTurma = null;
                    _professor.IdUsuarioNavigation.ProfessorTurma = null;
                    _professor.IdUsuarioNavigation.AlunoTurma = null;
                    _professor.IdTurmaNavigation.IdCursoNavigation.Turma = null;
                }

                return _listaProfessores;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public List<ProfessorTurma> ProcurarTurmasDoProfessor(int _idProfessor)
        {
            try
            {
                List<ProfessorTurma> _listaProfessores = _contexto.ProfessorTurma
                                    .Where(usr => usr.IdUsuario == _idProfessor)
                                    .Include("IdTurmaNavigation").Include("IdTurmaNavigation.IdCursoNavigation")
                                    .ToList();

                foreach (ProfessorTurma _professor in _listaProfessores)
                {
                    _professor.IdTurmaNavigation.ProfessorTurma = null;
                }

                return _listaProfessores;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public List<ProfessorTurma> ProcurarProfessoresDaTurma(int _idTurma)
        {
            try
            {
                List<ProfessorTurma> _listaProfessores = _contexto.ProfessorTurma
                                    .Where(usr => usr.IdTurma == _idTurma)
                                    .Include("IdUsuarioNavigation").ToList();

                foreach (ProfessorTurma _professor in _listaProfessores)
                {
                    _professor.IdUsuarioNavigation.ProfessorTurma = null;
                }

                return _listaProfessores;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public ProfessorTurma BuscarProfessorTurmaPorId(int _idProfessorTurma)
        {
            try
            {
                return _contexto.ProfessorTurma.FirstOrDefault(atm => atm.Id == _idProfessorTurma);

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public ProfessorTurma CadastrarTurmasDoProfessor(ProfessorTurma _professorTurma)
        {
            try
            {
                _contexto.ProfessorTurma.Add(_professorTurma);

                _contexto.SaveChanges();

                return _professorTurma;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public ProfessorTurma AlterarTurmaDoProfessor(ProfessorTurma _professorTurma)
        {
            try
            {
                _contexto.Entry(_professorTurma).State = EntityState.Modified;

                _contexto.SaveChanges();

                return _professorTurma;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public void ExcluirTurmaDoProfessor(int _idProfessorTurma)
        {
            try
            {
                ProfessorTurma _professorTurmaProcurado = BuscarProfessorTurmaPorId(_idProfessorTurma);

                //_contexto.ObjetivoAluno.RemoveRange(_contexto.ObjetivoAluno.Where(oba => oba.IdAlunoTurma == _idAlunoTurma));

                _contexto.ProfessorTurma.Remove(_professorTurmaProcurado);

                _contexto.SaveChanges();

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }
    }
}
