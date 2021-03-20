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
    public class PerfilRepository : IPerfil
    {
        //Instânciando nossa conexão com o banco de dados
        private readonly EduxContext _contexto;

        //Defindo o método construtor, para sempre que a classe UsuarioRepository seja instânciada
        //ela possa realizar a conexão com o nosso banco de dados;
        public PerfilRepository()
        {
            _contexto = new EduxContext();
        }

        /// <summary>
        /// Listar todos os tipos de perfils cadastrados
        /// </summary>
        /// <returns>Lista com toso os perfils</returns>
        public List<Perfil> ListarPerfils()
        {
            try
            {
                return _contexto.Perfil.ToList();

            }catch(Exception _e){

                throw new Exception(_e.Message);
            }
        }

        /// <summary>
        /// Buscar as informações específicas de um perfil
        /// </summary>
        /// <param name="_idPerfil">Código de identificação do perfil</param>
        /// <returns>Dados referente ao perfil buscado</returns>
        public Perfil BuscarPerfilPorId(int _idPerfil)
        {
            try
            {
                return _contexto.Perfil.FirstOrDefault(pf => pf.Id == _idPerfil);

            }catch (Exception _e){

                throw new Exception(_e.Message);
            } 
        }

        /// <summary>
        /// Cadastrar um novo perfil
        /// </summary>
        /// <param name="_perfil">Dados capturados pelo endpoint</param>
        /// <returns>Dados sobre o perfil cadastrado</returns>
        public Perfil CadastrarPerfil(Perfil _perfil)
        {
            try
            {
                _contexto.Perfil.Add(_perfil);

                _contexto.SaveChanges();

                return _perfil;

            }catch (Exception _e){

                throw new Exception(_e.Message);
            }
        }

        /// <summary>
        /// Alterar os dados de um perfil específico
        /// </summary>
        /// <param name="_perfil">Código de identificação do perfil</param>
        /// <returns>Perfil com os novos dados inseridos</returns>
        public Perfil AlterarPerfil(Perfil _perfil)
        {
            try
            {
                _contexto.Entry(_perfil).State = EntityState.Modified;
                _contexto.SaveChanges();

                return _perfil;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        /// <summary>
        /// Excluir um tipo de perfil
        /// </summary>
        /// <param name="_idPerfil">Código de identificação do perfil</param>
        public void ExcluirPerfil(int _idPerfil)
        {
            try
            {
                Perfil _perfilProcurado = BuscarPerfilPorId(_idPerfil);

                _contexto.Usuario.RemoveRange(_contexto.Usuario.Where(usr => usr.IdPerfil == _idPerfil));

                _contexto.Perfil.Remove(_perfilProcurado);

                _contexto.SaveChanges();

            }catch (Exception _e){

                throw new Exception(_e.Message);
            }
        }
    }
}
