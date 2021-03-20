using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduxProject.Domains;
using EduxProject.Interfaces;
using EduxProject.Repositories;
using EduxProject.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduxProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DicaController : ControllerBase
    {
        private readonly IDica _repository;

        public DicaController()
        {
            _repository = new DicaRepository();
        }

        /// <summary>
        /// Método para listar todas as dicas cadastradas
        /// </summary>
        /// <returns>Lista com as dicas cadastradas</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var _listaDica = _repository.listarDicas();

                if (_listaDica.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaDica);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para buscar os dados específicos de uma dica
        /// </summary>
        /// <param name="id">Código de identificação de uma dica</param>
        /// <returns>Dados referente a dica especificada</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Dica _dica = _repository.BuscarDicaPorId(id);

                if (_dica == null)
                {
                    return NoContent();
                }

                return Ok(_dica);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para cadastrar uma dica
        /// </summary>
        /// <param name="_dica">Dado para adicionar a nova dica</param>
        /// <returns>Dica com os dados cadastrados</returns>
        [HttpPost]
        public IActionResult Post([FromForm] Dica _dica)
        {
            try
            {
                if(_dica.Arquivo != null)
                {
                    var arquivo = UploadFile.Local(_dica.Arquivo);

                    _dica.Imagem = arquivo;
                }

                _repository.CadastrarDica(_dica);

                return Ok(_dica);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para alterar os dados referentes a uma dica específica
        /// </summary>
        /// <param name="id">Código de identificação de uma dica</param>
        /// <param name="_dica">Dados a serem alterados</param>
        /// <returns>Dados alterados sobre a dica informada</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] Dica _dica)
        {
            try
            {
                Dica _dicaBuscada = _repository.BuscarDicaPorId(id);

                if (_dica.Arquivo != null)
                {
                    var arquivo = UploadFile.Local(_dica.Arquivo);

                    _dicaBuscada.Imagem = arquivo;
                }

                _dicaBuscada.Texto = _dica.Texto;
                _dicaBuscada.IdUsuario = _dica.IdUsuario;

                _repository.AlterarDica(_dicaBuscada);

                return Ok(_dicaBuscada);

            }
            catch (DbUpdateConcurrencyException)
            {

                var _validarPerfil = _repository.BuscarDicaPorId(id);

                if (_validarPerfil == null)
                {

                    return NotFound();
                }

                return BadRequest();
            }
        }

        /// <summary>
        /// Método para excluir uma dica específica
        /// </summary>
        /// <param name="id">Código de idenficação de uma dica</param>
        /// <returns>Dados referente a uma dica excluida</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Dica _dicaProcurado = _repository.BuscarDicaPorId(id);

                if (_dicaProcurado == null)
                {
                    return NotFound();
                }

                _repository.ExcluirDica(id);

                return Ok(_dicaProcurado);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }
    }
}
