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
    public class TurmaRepository : ITurma
    {
        private readonly EduxContext _contexto;

        public TurmaRepository()
        {
            _contexto = new EduxContext();
        }

        public Turma AlterarTurma(Turma _turmaAlterada)
        {
            try
            {
                Turma _turmaProcurada = BuscarTurmaPorId(_turmaAlterada.Id);
                _turmaProcurada.Id = _turmaAlterada.Id;
                _turmaProcurada.IdCurso = _turmaAlterada.IdCurso;
                _turmaProcurada.Descricao = _turmaAlterada.Descricao;

                _contexto.Turma.Update(_turmaProcurada);
                _contexto.SaveChanges();
                return _turmaProcurada;



            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public Turma BuscarTurmaPorId(int _idTurma)
        {
            try
            {
                return _contexto.Turma.Include("IdCursoNavigation").FirstOrDefault(ctg => ctg.Id == _idTurma);
            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public Turma CadastrarTurma(Turma _turma)
        {
            try
            {
                _contexto.Turma.Add(_turma);
                _contexto.SaveChanges();
                return _turma;
            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            };
        }

        public void ExcluirTurma(int _idTurma)
        {
            try
            {
                Turma _turmaProcurada = BuscarTurmaPorId(_idTurma);
                _contexto.Turma.Remove(_turmaProcurada);
                _contexto.SaveChanges();
            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public List<Turma> ListarTurmas()
        {
            try
            {
                List<Turma> _listaTurmas = _contexto.Turma.Include("IdCursoNavigation")
                                                            .Include(tm => tm.ProfessorTurma)
                                                            .Include(tm => tm.AlunoTurma).ToList();

                foreach(Turma turma in _listaTurmas){

                    turma.IdCursoNavigation.Turma = null;
                }

                return _listaTurmas;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            };
        }
    }
}
