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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduxProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICurso _repository;

        public CursoController()
        {
            _repository = new CursoRepository();
        }

        /// <summary>
        /// Método para listar todos os cursos cadastrados
        /// </summary>
        /// <returns>Lista com todos os cursos</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var _listaCursos = _repository.ListarCursos();

                if (_listaCursos.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaCursos);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para buscar os dados de um curso específico
        /// </summary>
        /// <param name="id">Código de identificação de um curso</param>
        /// <returns>Dados referente a um curso específico</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Curso _curso = _repository.BuscarCursoPorId(id);

                if (_curso == null)
                {
                    return NoContent();
                }

                return Ok(_curso);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para cadastrar um novo curso
        /// </summary>
        /// <param name="_curso">Dados a semrem inseridos no novo curso</param>
        /// <returns>Curso cadastrados com os dados informados</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpPost]
        public IActionResult Post([FromBody] Curso _curso)
        {
            try
            {
                _repository.CadastrarCurso(_curso);
                return Ok(_curso);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para alterar os dados de um curso específico
        /// </summary>
        /// <param name="id">Código de identificação do curso</param>
        /// <param name="_curso">Dados para serem inseridos</param>
        /// <returns>Curso com os dados alterados</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Curso _curso)
        {
            try
            {
                _repository.AlterarCurso(_curso);

                return Ok(_curso);

            }
            catch (DbUpdateConcurrencyException)
            {

                var _validarCurso = _repository.BuscarCursoPorId(id);

                if (_validarCurso == null)
                {

                    return NotFound();
                }

                return BadRequest();
            }
        }

        /// <summary>
        /// Método para excluir os dados de um curso
        /// </summary>
        /// <param name="id">Código de identificação de um curso</param>
        /// <returns>Dados referente ao curso excluido</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Curso _perfilCurso = _repository.BuscarCursoPorId(id);

                if (_perfilCurso == null)
                {
                    return NotFound();
                }

                _repository.ExcluirCurso(id);

                return Ok(_perfilCurso);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }
    }
}
