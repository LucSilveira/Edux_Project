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
    public class ObjetivoRepository : IObjetivo
    {
        private readonly EduxContext _contexto;
        public ObjetivoRepository()
        {
            _contexto = new EduxContext();
        }

        public Objetivo AlterarObjetivo(Objetivo _objetivo)
        {
            try
            {
                Objetivo _objetivoProcurado = BuscarObjetivoPorId(_objetivo.Id);

                _objetivoProcurado.Descricao = _objetivo.Descricao;

                _contexto.Objetivo.Update(_objetivoProcurado);

                _contexto.SaveChanges();

                return _objetivoProcurado;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public Objetivo BuscarObjetivoPorId(int _idObjetivo)
        {
            try
            {
                return _contexto.Objetivo.FirstOrDefault(pf => pf.Id == _idObjetivo);

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
            throw new NotImplementedException();
        }

        public Objetivo CadastrarObjetivo(Objetivo _objetivo)
        {
            try
            {
                _contexto.Objetivo.Add(_objetivo);

                _contexto.SaveChanges();

                return _objetivo;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public void ExcluirObjetivo(int _idObjetivo)
        {
            try
            {
                Objetivo _ObjetivoProcurado = BuscarObjetivoPorId(_idObjetivo);

                _contexto.Usuario.RemoveRange(_contexto.Usuario.Where(usr => usr.IdPerfil == _idObjetivo));

                _contexto.Objetivo.Remove(_ObjetivoProcurado);

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public List<Objetivo> listarObjetivos()
        {
            try
            {
                List<Objetivo> _listaObjetivos = _contexto.Objetivo.Include("IdCategoriaNavigation").ToList();

                foreach(Objetivo objetivo in _listaObjetivos)
                {
                    objetivo.IdCategoriaNavigation.Objetivo = null;
                }

                return _listaObjetivos;
            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }
    }
}
