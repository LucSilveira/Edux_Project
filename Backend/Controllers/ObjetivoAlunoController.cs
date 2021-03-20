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
    public class ObjetivoAlunoController : ControllerBase
    {
        private readonly IObjetivoAluno _repository;

        public ObjetivoAlunoController()
        {
            _repository = new ObjetivoAlunoRepository();  
        }


        /// <summary>
        /// Método para listar todos os objetivos de alunos cadastrados
        /// </summary>
        /// <returns>Lista com os resultados encontrados</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var _listaObjetivosAlunos = _repository.ListarObjetivosDosAlunos();

                if(_listaObjetivosAlunos.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaObjetivosAlunos);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para listar todos os alunos que atingiram determinado objetivo
        /// </summary>
        /// <param name="id">Código de identificação do objetivo</param>
        /// <returns>Lista com os alunos encontrados</returns>
        [HttpGet("alunos{id}")]
        public IActionResult GetAlunosPorObjetivo(int id)
        {
            try
            {
                var _listaAlunosObjetivo = _repository.ProcurarAlunosPorObjetivo(id);

                if(_listaAlunosObjetivo.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaAlunosObjetivo);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para listar todos os objetivos de um determinado aluno
        /// </summary>
        /// <param name="id">Código de identificação do aluno</param>
        /// <returns>Lista com os objetivos encontrados</returns>
        [HttpGet("objetivos/{id}")]
        public IActionResult GetObjetivosPorAluno(int id)
        {
            try
            {
                var _listaObjetivosAluno = _repository.ProcurarObjetivosPorAluno(id);

                if (_listaObjetivosAluno.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaObjetivosAluno);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Buscar os dados de um objetivo de um aluno
        /// </summary>
        /// <param name="id">Código de idenficação do objetivo do aluno</param>
        /// <returns>Dados sobre o objetivo do aluno</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                ObjetivoAluno _objetivoAluno = _repository.BuscarObjetivoDoAlunoPorId(id);

                if(_objetivoAluno == null)
                {
                    return NoContent();
                }

                return Ok(_objetivoAluno);
            }
            catch (Exception _e)
            {
                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para cadastrar um objetivo de um aluno
        /// </summary>
        /// <param name="_objetivo">Dados sobre o objetivo do aluno</param>
        /// <returns>Dados cadastrados</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpPost]
        public IActionResult Post([FromBody] ObjetivoAluno _objetivo)
        {
            try
            {
                _repository.CadastrarObjetivosDoAluno(_objetivo);

                return Ok(_objetivo);
            }
            catch (Exception _e)
            {
                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para alterar os dados de um objetivo por aluno
        /// </summary>
        /// <param name="id">Código de identificação do objetivo do aluno</param>
        /// <param name="_objetivo">Dados a serem alterados</param>
        /// <returns>Dados alterados</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ObjetivoAluno _objetivo)
        {
            try
            {
                _repository.AlterarObjetivosDoAluno(_objetivo);

                return Ok(_objetivo);
            }
            catch (DbUpdateConcurrencyException _e)
            {
                var _validarObjetivo = _repository.BuscarObjetivoDoAlunoPorId(id);

                if(_validarObjetivo == null)
                {
                    return NotFound();
                }

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para excluir um objetivo do aluno
        /// </summary>
        /// <param name="id">Código de identificação do objetivo</param>
        /// <returns>Dados referente ao objetivo excluido</returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ObjetivoAluno _objetivoProcurado = _repository.BuscarObjetivoDoAlunoPorId(id);

                if(_objetivoProcurado == null)
                {
                    return NotFound();
                }

                _repository.ExcluirObjetivoDoAluno(id);

                return Ok(_objetivoProcurado);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }
    }
}
