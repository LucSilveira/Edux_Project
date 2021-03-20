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
    [Authorize(Roles = "Administrador, Professor")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoria _repository;
        public CategoriaController()
        {
            _repository = new CategoriaRepository();
        }
        /// <summary>
        /// Método para listar as categorias
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var _categoria = _repository.ListarCategorias();
                if(_categoria.Count == 0)
                {
                    return NoContent();
                }
                return Ok(new
                {
                    totalCount = _categoria.Count,
                    data = _categoria
                });
            }
            catch (Exception _e) {

                return BadRequest(_e.Message);
                throw;
            }
           
        }

        /// <summary>
        /// Métodopara buscar uma categoria especifica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Categoria _categoria =_repository.BuscarCategoriaPorId(id); 
                if(_categoria == null)
                {
                    return NoContent();
                }
                return Ok(_categoria);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }

        }

        /// <summary>
        /// Método para cadastrar uma curtida
        /// </summary>
        /// <param name="_categoria"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]Categoria _categoria)
        {
            try
            {
                _repository.CadastrarCategoria(_categoria);
                return Ok(_categoria);
            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para alterar uma categoria
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_categoria"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Categoria _categoria)
        {
            try
            {
                _repository.AlterarCategoria(_categoria);
                return Ok(_categoria);
            }
            catch (DbUpdateConcurrencyException)
            {
                var _validarCategoria = _repository.BuscarCategoriaPorId(id);
                if(_validarCategoria == null)
                {
                    return NotFound();
                }
                return BadRequest();
            }
        }

        /// <summary>
        /// Método para excluir uma categoria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Categoria _categoriaBuscada = _repository.BuscarCategoriaPorId(id);
                if (_categoriaBuscada == null)
                { 
                    return NotFound();
                }
                _repository.ExcluirCategoria(id);
                return Ok(_categoriaBuscada);
            }
            catch (Exception)
            {
                return BadRequest(new 
                {
                    StatusCode = 404,
                    error = "Não foi possivel realizar essa operação"
                });
            }
        }
    }
}
