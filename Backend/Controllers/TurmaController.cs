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
    public class TurmaController : ControllerBase
    { 
        private readonly ITurma _repository;
        public TurmaController()
        {
            _repository = new TurmaRepository();
     }

        
        /// <summary>
        /// Método para listar as turmas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var _listarCategorias = _repository.ListarTurmas();
                if (_listarCategorias.Count == 0)
                {
                    return NoContent();
                }
                return Ok(_listarCategorias);
            }
            catch (Exception _e)
            {
                return BadRequest(_e.Message);
            }
           
        }
        /// <summary>
        /// Método para buscar uma turma especifica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Turma _turma = _repository.BuscarTurmaPorId(id);
                if (_turma == null)
                {
                    return NoContent();
                }
                return Ok(_turma);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }
        /// <summary>
        /// Método para cadastrar uma turma
        /// </summary>
        /// <param name="_turma"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpPost]
        public IActionResult Post([FromBody] Turma _turma)
        {
            try
            {
            _repository.CadastrarTurma(_turma);
            return Ok(_turma);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Metodo para alterar uma turma
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_turma"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Turma _turma)
        {
            try
            {
                _repository.AlterarTurma(_turma);
                return Ok(_turma);

            }
            catch (DbUpdateConcurrencyException)
            {

                var _validarTurma = _repository.BuscarTurmaPorId(id);
                if (_validarTurma == null)
                {
                    return NotFound();
                }
                return BadRequest();
            }
        }
        /// <summary>
        /// Método para excluir uma turma especifica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Turma _TurmaProcurada = _repository.BuscarTurmaPorId(id);
                if (_TurmaProcurada == null)
                {
                    return NotFound();
                }
                _repository.ExcluirTurma(id);
                return Ok(_TurmaProcurada);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }
    }
}
