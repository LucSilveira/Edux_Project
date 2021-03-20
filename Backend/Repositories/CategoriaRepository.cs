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
    public class CategoriaRepository : ICategoria
    {
        private readonly EduxContext _contexto;

        public CategoriaRepository()
        {
            _contexto = new EduxContext();
        }

        public Categoria AlterarCategoria(Categoria _categoriaAlterada)
        {
            try
            {
                Categoria _categoriaProcurada = BuscarCategoriaPorId(_categoriaAlterada.Id);
                _categoriaProcurada.Id = _categoriaAlterada.Id;
                _categoriaProcurada.Tipo = _categoriaAlterada.Tipo;
                _categoriaProcurada.Objetivo = _categoriaAlterada.Objetivo;

                _contexto.Categoria.Update(_categoriaProcurada);
                //_contexto.Entry(_categoriaAlterada).State = EntityState.Modified;
                _contexto.SaveChanges();

                return _categoriaAlterada;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public Categoria BuscarCategoriaPorId(int _idCategoria)
        {
            try
            {
                return _contexto.Categoria.FirstOrDefault(ctg => ctg.Id == _idCategoria);

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public Categoria CadastrarCategoria(Categoria _categoria)
        {
            try
            {
                _contexto.Categoria.Add(_categoria);
                _contexto.SaveChanges();
                return _categoria;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
          
        }

        public void ExcluirCategoria(int _idCategoria)
        {
            try
            {
                Categoria _categoriaProcurada = BuscarCategoriaPorId(_idCategoria);
                _contexto.Objetivo.RemoveRange(_contexto.Objetivo.Where(obj => obj.IdCategoria == _idCategoria));
                _contexto.Categoria.Remove(_categoriaProcurada);
                _contexto.SaveChanges();
            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
            
        }

        public List<Categoria> ListarCategorias()
        {
            try
            {
                return _contexto.Categoria.ToList();

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }
    }
}
