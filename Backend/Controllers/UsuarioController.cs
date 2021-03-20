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
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _repository;

        public UsuarioController()
        {
            _repository = new UsuarioRepository();
        }
        /// <summary>
        /// Método para listar usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var _listaUsuarios = _repository.ListarUsuarios();

                if (_listaUsuarios.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaUsuarios);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para listar os professores
        /// </summary>
        /// <returns></returns>
        [HttpGet("professor")]
        public IActionResult GetProfessor()
        {
            try
            {
                var _listaProfessores = _repository.ListarProfessores();

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
        /// Método para listar  os alunos
        /// </summary>
        /// <returns></returns>
        [HttpGet("aluno")]
        public IActionResult GetAluno()
        {
            try
            {
                var _listaUsuarios = _repository.ListarAlunos();

                if (_listaUsuarios.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaUsuarios);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

       /// <summary>
       /// Método para buscar  um usuraio específico
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Usuario _usuario = _repository.BuscarUsuarioPorId(id);

                if (_usuario == null)
                {
                    return NoContent();
                }

                return Ok(_usuario);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] Usuario _usuario)
        {
            try
            {
                _usuario.Senha = Crypto.Criptografar(_usuario.Senha, _usuario.Email.Substring(0, 4));

                _repository.CadastrarUsuario(_usuario);

                return Ok(_usuario);

            }
            catch (Exception _e)
            {

                return BadRequest(new
                {
                    error = _e.Message,
                    data = _usuario
                });
            }
        }

        /// <summary>
        /// Método para cadastar um aluno
        /// </summary>
        /// <param name="_usuario"></param>
        /// <returns></returns>
        [HttpPost("aluno")]
        public IActionResult PostAluno([FromBody] Usuario _usuario)
        {
            try
            {
                _usuario.Senha = Crypto.Criptografar(_usuario.Senha, _usuario.Email.Substring(0, 4));
                _repository.CadastrarAluno(_usuario);
                return Ok(_usuario);

            }
            catch (Exception _e)
            {

                return BadRequest(new
                {
                    error = _e.Message,
                    data = _usuario
                });
            }
        }
        /// <summary>
        /// Método para alterar um usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_usuario"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] Usuario _usuario)
        {
            try
            {
                Usuario _usuarioBuscado = _repository.BuscarUsuarioPorId(id);

                if (_usuario.Arquivo != null)
                {
                    var arquivo = UploadFile.Local(_usuario.Arquivo);

                    _usuarioBuscado.Imagem = arquivo;
                }

                _usuarioBuscado.Nome = _usuario.Nome;
                _usuarioBuscado.Email = _usuario.Email;
                _usuarioBuscado.DataCadastro = _usuarioBuscado.DataCadastro;
                _usuarioBuscado.IdPerfil = _usuarioBuscado.IdPerfil;

                if(_usuario.Senha != null)
                {
                    _usuario.Senha = Crypto.Criptografar(_usuario.Senha, _usuarioBuscado.Email.Substring(0, 4));
                    _usuarioBuscado.Senha = _usuario.Senha;
                }
                else
                {
                    _usuarioBuscado.Senha = _usuarioBuscado.Senha;
                }
                _repository.AlterarUsuario(_usuarioBuscado);

                return Ok(_usuarioBuscado);

            }
            catch (DbUpdateConcurrencyException _e)
            {
                var _validarUsuario = _repository.BuscarUsuarioPorId(id);

                if (_validarUsuario == null)
                {

                    return NotFound("Usuário não encontrado");
                }

                return BadRequest(new {
                    error = _e.Message,
                    data = _usuario
                });
            }
        }

       /// <summary>
       /// Método para excluir um usuario específico
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Usuario _usuarioProcurado = _repository.BuscarUsuarioPorId(id);

                if (_usuarioProcurado == null)
                {
                    return NotFound();
                }

                _repository.ExcluirUsuario(id);

                return Ok(_usuarioProcurado);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }
    }
}
