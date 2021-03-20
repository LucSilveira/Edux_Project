using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduxProject.Domains;
using EduxProject.Interfaces;
using EduxProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduxProject.Controllers
{
    [Authorize(Roles = "Administrador, Professor")]
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfil _repository;

        public PerfilController()
        {
            _repository = new PerfilRepository();
        }

        /// <summary>
        /// Método para listar perfils
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var _listaPerfils = _repository.ListarPerfils();

                if(_listaPerfils.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaPerfils);

            }catch (Exception _e){

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para buscar um perfil específico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Perfil _perfil = _repository.BuscarPerfilPorId(id);

                if(_perfil == null)
                {
                    return NoContent();
                }

                return Ok(_perfil);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para cadastrar um perfil
        /// </summary>
        /// <param name="_perfil"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Perfil _perfil)
        {
            try
            {
                _repository.CadastrarPerfil(_perfil);
                return Ok(_perfil);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }
        /// <summary>
        /// Método para alterar um perfil
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_perfil"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Perfil _perfil)
        {
            try
            {
                _repository.AlterarPerfil(_perfil);

                return Ok(_perfil);

            }
            catch (DbUpdateConcurrencyException)
            {

                var _validarPerfil = _repository.BuscarPerfilPorId(id);

                if (_validarPerfil == null)
                {

                    return NotFound();
                }

                return BadRequest();
            }
        }

        /// <summary>
        /// Método para excluir um perfil específico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Perfil _perfilProcurado = _repository.BuscarPerfilPorId(id);

                if (_perfilProcurado == null)
                {
                    return NotFound();
                }

                _repository.ExcluirPerfil(id);

                return Ok(_perfilProcurado);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }
    }
}
