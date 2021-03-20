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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorTurmaController : ControllerBase
    {
        private readonly IProfessorTurma _repository;

        public ProfessorTurmaController()
        {
            _repository = new ProfessorTurmaRepository();
        }

        /// <summary>
        /// Método para listar todos os Professores e suas respectivas turmas
        /// </summary>
        /// <returns>Lista com todos os professores e suas turmas</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var _listaProfessores = _repository.ListarTurmasDosProfessores();

                if (_listaProfessores.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaProfessores);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para listar todas as turmas de acordo com o professor específico
        /// </summary>
        /// <param name="id">Código de identificação do professor</param>
        /// <returns>Lista com as turmas do professor informado</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpGet("professor/{id}")]
        public IActionResult GetTurmasDoProfessor(int id)
        {
            try
            {
                var _listaProfessor = _repository.ProcurarTurmasDoProfessor(id);

                if (_listaProfessor.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaProfessor);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para listar todos os professores de acordo com uma turma específica
        /// </summary>
        /// <param name="id">Código de identificação da Turma</param>
        /// <returns>Lista com os professores da turma informada</returns>
        [HttpGet("turma/{id}")]
        public IActionResult GetProfessoresDaTurma(int id)
        {
            try
            {
                var _listaProfessorTurma = _repository.ProcurarProfessoresDaTurma(id);

                if (_listaProfessorTurma.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaProfessorTurma);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para buscar os dados de um professor em sua turma
        /// </summary>
        /// <param name="id">Código de identificação de turma do professor</param>
        /// <returns>Dados referente a matrícula da turma sobre o professor</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                ProfessorTurma _professor = _repository.BuscarProfessorTurmaPorId(id);

                if (_professor == null)
                {
                    return NoContent();
                }

                return Ok(_professor);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para cadastrar uma turma referente a um professor
        /// </summary>
        /// <param name="_professor">Dados referente a turma de um professor</param>
        /// <returns>Dados referente a turma do professor cadastrada</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpPost]
        public IActionResult Post([FromBody] ProfessorTurma _professor)
        {
            try
            {
                _repository.CadastrarTurmasDoProfessor(_professor);
                return Ok(_professor);

            }
            catch (Exception _e)
            {

                return BadRequest(new
                {
                    error = _e.Message,
                    data = _professor
                });
            }
        }

        /// <summary>
        /// Método para alterar os dados de uma turma de acordo com o professor
        /// </summary>
        /// <param name="id">Código de identificação da turma do professor</param>
        /// <param name="_professor">Dados da turma do professor</param>
        /// <returns>Dados alterados</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProfessorTurma _professor)
        {
            try
            {
                _repository.AlterarTurmaDoProfessor(_professor);

                return Ok(_professor);

            }
            catch (DbUpdateConcurrencyException _e)
            {
                var _validarProfessor = _repository.BuscarProfessorTurmaPorId(id);

                if (_validarProfessor == null)
                {

                    return NotFound();
                }

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para excluir a turma de um professor
        /// </summary>
        /// <param name="id">Código de identificação da turma do professor</param>
        /// <returns>Dados refente a turma do professor</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ProfessorTurma _professorProcurado = _repository.BuscarProfessorTurmaPorId(id);

                if (_professorProcurado == null)
                {
                    return NotFound();
                }

                _repository.ExcluirTurmaDoProfessor(id);

                return Ok(_professorProcurado);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }
    }
}
