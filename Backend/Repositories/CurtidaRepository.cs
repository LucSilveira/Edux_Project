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
    public class CurtidaRepository : ICurtida
    {
        private readonly EduxContext _contexto;

        public CurtidaRepository()
        {
            _contexto = new EduxContext();
        }

        public Curtida BuscarCurtidasPorId(int _idCurtida)
        {
            try
            {
                return _contexto.Curtida.FirstOrDefault(dcs => dcs.Id == _idCurtida);
            }
            catch (Exception _e)
            {
                throw new Exception(_e.Message);
            }
        }

        public Curtida CadastrarCurtidas(Curtida _curtida)
        {
            try
            {
                _contexto.Curtida.Add(_curtida);
                _contexto.SaveChanges();
                return _curtida;
            }
            catch (Exception _e)
            {
                throw new Exception(_e.Message);
            }
        }

        public void ExcluirCurtida(int _idCurtida)
        {
            try
            {
                Curtida _curtidaProcurando = BuscarCurtidasPorId(_idCurtida);
                _contexto.Curtida.Remove(_curtidaProcurando);
                _contexto.SaveChanges();

            }catch (Exception _e){

                throw new Exception(_e.Message);
            }
        }

        public List<Curtida> ListarCurtidas()
        {
            try
            {
                List<Curtida> _listaCurtidas = _contexto.Curtida.Include("IdUsuarioNavigation")
                                                    .Include("IdDicaNavigation").ToList();

                foreach(Curtida curtida in _listaCurtidas)
                {
                    curtida.IdUsuarioNavigation.Curtida = null;
                    curtida.IdDicaNavigation.Curtida = null;
                }

                return _listaCurtidas;
            }
            catch (Exception _e)
            {
                throw new Exception(_e.Message);
            }
        }
    }

}