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
    public class CursoRepository : ICurso
    {
        private readonly EduxContext _contexto;

        public CursoRepository()
        {
            _contexto = new EduxContext();
        }

        public Curso AlterarCurso(Curso _curso)
        {
            try
            {
                _contexto.Entry(_curso).State = EntityState.Modified;

                _contexto.SaveChanges();

                return _curso;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public Curso BuscarCursoPorId(int _idCurso)
        {
            try
            {
                return _contexto.Curso.FirstOrDefault(pf => pf.Id == _idCurso);

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public Curso CadastrarCurso(Curso _curso)
        {
            try
            {
                _contexto.Curso.Add(_curso);

                _contexto.SaveChanges();

                return _curso;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public void ExcluirCurso(int _idCurso)
        {
            try
            {
                Curso _CursoProcurado = BuscarCursoPorId(_idCurso);

                _contexto.Curso.Remove(_CursoProcurado);

                _contexto.SaveChanges();

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public List<Curso> ListarCursos()
        {
            try
            {
                List<Curso> _listaCursos = _contexto.Curso.Include("IdInstituicaoNavigation").ToList();

                foreach(Curso curso in _listaCursos)
                {
                    curso.IdInstituicaoNavigation.Curso = null;
                }

                return _listaCursos;
            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }
    }
}

        
    


    