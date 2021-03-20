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
    public class AlunoTurmaController : ControllerBase
    {
        private readonly IAlunoTurma _repository;

        public AlunoTurmaController()
        {
            _repository = new AlunoTurmaRepository();
        }

        /// <summary>
        /// Método para listar todos os Alunos e suas respectivas turmas
        /// </summary>
        /// <returns>Lista com todos os alunos e suas turmas</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var _listaAlunos = _repository.ListarTurmasDosAlunos();

                if (_listaAlunos.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaAlunos);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para listar todas as turmas de acordo com o aluno específico
        /// </summary>
        /// <param name="id">Código de identificação do Aluno</param>
        /// <returns>Lista com as turmas do aluno informado</returns>
        [HttpGet("aluno/{id}")]
        public IActionResult GetTurmasDoAluno(int id)
        {
            try
            {
                var _listaAluno = _repository.ProcurarTurmasDoAluno(id);

                if (_listaAluno.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaAluno);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para listar todos os alunos de acordo com uma turma específica
        /// </summary>
        /// <param name="id">Código de identificação da Turma</param>
        /// <returns>Lista com os alunos da turma informada</returns>
        [HttpGet("turma/{id}")]
        public IActionResult GetAlunosDaTurma(int id)
        {
            try
            {
                var _listaAlunoTurma = _repository.ProcurarAlunosDaTurma(id);

                if (_listaAlunoTurma.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaAlunoTurma);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para buscar os dados de um aluno em sua turma
        /// </summary>
        /// <param name="id">Código de identificação de turma do aluno</param>
        /// <returns>Dados referente a matrícula da turma sobre o aluno</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                AlunoTurma _aluno = _repository.BuscarAlunoTurmaPorId(id);

                if (_aluno == null)
                {
                    return NoContent();
                }

                return Ok(_aluno);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para cadastrar uma turma referente a um aluno
        /// </summary>
        /// <param name="_aluno">Dados referente a turma de um aluno</param>
        /// <returns>Dados referente a turma do aluno cadastrada</returns>
        [HttpPost]
        public IActionResult Post([FromBody] AlunoTurma _aluno)
        {
            try
            {
                _repository.CadastrarTurmasDoAluno(_aluno);
                return Ok(_aluno);

            }
            catch (Exception _e)
            {

                return BadRequest(new
                {
                    error = _e.Message,
                    data = _aluno
                });
            }
        }

        /// <summary>
        /// Método para alterar os dados de uma turma de acordo com o aluno
        /// </summary>
        /// <param name="id">Código de identificação da turma do aluno</param>
        /// <param name="_aluno">Dados da turma do aluno</param>
        /// <returns>Dados alterados</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AlunoTurma _aluno)
        {
            try
            {
                _repository.AlterarTurmaDoAluno(_aluno);

                return Ok(_aluno);

            }
            catch (DbUpdateConcurrencyException _e)
            {
                var _validarAluno = _repository.BuscarAlunoTurmaPorId(id);

                if (_validarAluno == null)
                {

                    return NotFound();
                }

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para excluir a turma de um aluno
        /// </summary>
        /// <param name="id">Código de identificação da turma do aluno</param>
        /// <returns>Dados refente a turma do aluno</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                AlunoTurma _alunoProcurado = _repository.BuscarAlunoTurmaPorId(id);

                if (_alunoProcurado == null)
                {
                    return NotFound();
                }

                _repository.ExcluirTurmaDoAluno(id);

                return Ok(_alunoProcurado);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }
    }
}
