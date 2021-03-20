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
    public class InstituicaoController : ControllerBase
    {
        private readonly IInstituicao _repository;
        public InstituicaoController()
        {
            _repository = new InstituicaoRepository();
        }
        /// <summary>
        /// Método para listar as instiuiçoes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var _listarInstituicao = _repository.ListarInstituicao();
                if (_listarInstituicao.Count == 0)
                {
                    return NoContent();
                }
                return Ok(_listarInstituicao);
            }
            catch (Exception _e)
            {
                return BadRequest(_e.Message);
            }

        }
        /// <summary>
        /// Método para buscar uma instituiçao especifica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Instituicao _instituicao = _repository.BuscarInstituicaoPorId(id);
                if (_instituicao == null)
                {
                    return NoContent();
                }
                return Ok(_instituicao);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }


        /// <summary>
        /// Método para cadastrar uma instiuiçao
        /// </summary>
        /// <param name="_instituicao"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpPost]
        public IActionResult Post([FromBody] Instituicao _instituicao)
        {
            try
            {
                _repository.CadastrarInstituicao(_instituicao);
                return Ok(_instituicao);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }


        /// <summary>
        ///  Método para excluir uma instiuicao especifica
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_instituicao"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Instituicao _instituicao)
        {
            try
            {
                _repository.AlterarInstituicao(_instituicao);
                return Ok(_instituicao);

            }
            catch (DbUpdateConcurrencyException)
            {

                var _validarInstituicao = _repository.BuscarInstituicaoPorId(id);
                if (_validarInstituicao == null)
                {
                    return NotFound();
                }
                return BadRequest();
            }
        }

        /// <summary>
        /// Neste metodo excluimos uma instituicao 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrador, Professor")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Instituicao _instituicaoProcurada = _repository.BuscarInstituicaoPorId(id);

                if (_instituicaoProcurada == null)
                {
                    return NotFound();
                }
                _repository.ExcluirInstituicao(id);

                return Ok(_instituicaoProcurada);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }
    }
}
