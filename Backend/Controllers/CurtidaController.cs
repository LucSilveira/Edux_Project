using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduxProject.Domains;
using EduxProject.Interfaces;
using EduxProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduxProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CurtidaController : ControllerBase
    {
        private readonly ICurtida _repository;
        public CurtidaController()
        {
            _repository = new CurtidaRepository();
        }
        /// <summary>
        /// Método para listar as Curtidas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var _listarCurtida = _repository.ListarCurtidas();
                if (_listarCurtida.Count == 0)
                {
                    return NoContent();
                }
                return Ok(_listarCurtida);
            }
            catch (Exception )
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Método para buscar curtidas especificas
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Curtida _curtida = _repository.BuscarCurtidasPorId(id);
                if (_curtida == null)
                {
                    return NoContent();
                }
                return Ok(_curtida);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para cadastrar uma curtida
        /// </summary>
        /// <param name="_curtida"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Curtida _curtida)
        {
            try
            {
                _repository.CadastrarCurtidas(_curtida);
                return Ok(_curtida);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para excluir uma curtida
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Curtida _curtidaProcurada = _repository.BuscarCurtidasPorId(id);
                if (_curtidaProcurada == null)
                {
                    return NotFound();
                }
                _repository.ExcluirCurtida(id);
                return Ok(_curtidaProcurada);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }
    }
}
