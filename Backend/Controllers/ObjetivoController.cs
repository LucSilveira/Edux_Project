using EduxProject.Domains;
using EduxProject.Interfaces;
using EduxProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EduxProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class ObjetivoController : ControllerBase
    {
        private readonly IObjetivo _repository;

        public ObjetivoController()
        {
            _repository = new ObjetivoRepository();
        }

        /// <summary>
        /// Método para listar todos os objetivos cadastrados
        /// </summary>
        /// <returns>ista com todos os objetivos</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var _listaObjetivos = _repository.listarObjetivos();

                if (_listaObjetivos.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaObjetivos);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para buscar os dados específicos de um objetivo
        /// </summary>
        /// <param name="id">Código de identificação de um objetivo</param>
        /// <returns>Dados pertinentes ao objetivo</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Objetivo _objetivo = _repository.BuscarObjetivoPorId(id);

                if (_objetivo == null)
                {
                    return NoContent();
                }

                return Ok(_objetivo);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para cadastrar um novo objetivo
        /// </summary>
        /// <param name="_objetivo">Dados pertinentes ao objetivo</param>
        /// <returns>Objetivo cadastrados com os dados informados</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpPost]
        public IActionResult Post([FromBody] Objetivo _objetivo)
        {
            try
            {
                _repository.CadastrarObjetivo(_objetivo);
                return Ok(_objetivo);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para alterar os dados referente ao objetivo informado
        /// </summary>
        /// <param name="id">Código de identificação do objetivo</param>
        /// <param name="_objetivo">Dados a serem alterados</param>
        /// <returns>Objetivo com os dados novos</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Objetivo _objetivo)
        {
            try
            {
                _repository.AlterarObjetivo(_objetivo);

                return Ok(_objetivo);

            }
            catch (DbUpdateConcurrencyException)
            {

                var _validarObjetivo = _repository.BuscarObjetivoPorId(id);

                if (_validarObjetivo == null)
                {

                    return NotFound();
                }

                return BadRequest();
            }
        }

        /// <summary>
        /// Método para excluir um objetivo
        /// </summary>
        /// <param name="id">Código de identificação de um objetivo</param>
        /// <returns>Dados sobre o objetivo excluido</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Objetivo _objetivoProcurado = _repository.BuscarObjetivoPorId(id);

                if (_objetivoProcurado == null)
                {
                    return NotFound();
                }

                _repository.ExcluirObjetivo(id);

                return Ok(_objetivoProcurado);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }
    }

}
