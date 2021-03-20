using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduxProject.Domains;
using EduxProject.Interfaces;
using EduxProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduxProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        private readonly IRanking _repository;

        public RankingController()
        {
            _repository = new RankingRepository();
        }

        /// <summary>
        /// Método para listar o ranking geral da sala
        /// </summary>
        /// <returns>Lista com as classificações dos alunos</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Ranking> _listaComRanking = _repository.ListarRanking();

                if(_listaComRanking.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaComRanking);

            }catch(Exception _e)
            {
                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para listar o ranking dos alunos de acordo com a turma especificada
        /// </summary>
        /// <param name="id">Código de identificação da turma</param>
        /// <returns>Lista com o ranking dos alunos</returns>
        [HttpGet("turma/{id}")]
        public IActionResult GetRankingTurma(int id)
        {
            try
            {
                List<Ranking> _listaRanking = _repository.ListarRankingDaTurma(id);

                if(_listaRanking.Count == 0)
                {
                    return NoContent();
                }

                return Ok(_listaRanking);

            }
            catch (Exception _e)
            {

                return BadRequest(_e.Message);
            }
        }

        /// <summary>
        /// Método para buscar a classificação específica de um aluno
        /// </summary>
        /// <param name="nome">O nome do aluno que será buscado</param>
        /// <returns>Os dados do ranking do aluno</returns>
        [HttpGet("{nome}")]
        public IActionResult Get(string nome)
        {
            try
            {
                Ranking _ranking = _repository.BuscarRankingDoAluno(nome);

                if(_ranking == null)
                {
                    return NotFound();
                }

                return Ok(_ranking);

            }catch(Exception _e)
            {
                return BadRequest(_e.Message);
            }
        }
    }
}
