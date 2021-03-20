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
    public class DicaRepository : IDica
    {
        private readonly EduxContext _contexto;
        public DicaRepository()
        {
            _contexto = new EduxContext();
        }

        public Dica AlterarDica(Dica _dica)
        {
            try
            {
                _contexto.Entry(_dica).State = EntityState.Modified;

                _contexto.SaveChanges();

                return _dica;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public Dica BuscarDicaPorId(int _idDica)
        {
            try
            {
                return _contexto.Dica.FirstOrDefault(pf => pf.Id == _idDica);

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public Dica CadastrarDica(Dica _dica)
        {
            try
            {
                _contexto.Dica.Add(_dica);

                _contexto.SaveChanges();

                return _dica;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public void ExcluirDica(int _idDica)
        {
            try
            {
                Dica _dicaProcurada = BuscarDicaPorId(_idDica);

                _contexto.Usuario.RemoveRange(_contexto.Usuario.Where(usr => usr.IdPerfil == _idDica));

                _contexto.Dica.Remove(_dicaProcurada);

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public List<Dica> listarDicas()
        {
            try
            {
                List<Dica> _listaDicas = _contexto.Dica.Include("IdUsuarioNavigation").ToList();

                foreach(Dica dica in _listaDicas)
                {
                    dica.IdUsuarioNavigation.Dica = null;
                }

                return _listaDicas;
            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }
    }
}
