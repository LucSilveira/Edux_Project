using EduxProject.Contexts;
using EduxProject.Domains;
using EduxProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Repositories
{
    public class InstituicaoRepository : IInstituicao
    {
        //Instânciando nossa conexão com o banco de dados
        private readonly EduxContext _contexto;

        //Defindo o método construtor, para sempre que a classe UsuarioRepository seja instânciada
        //ela possa realizar a conexão com o nosso banco de dados;
        public InstituicaoRepository()
        {
            _contexto = new EduxContext();
        }

        /// <summary>
        /// Listar todos as instituições cadastradas no sistema
        /// </summary>
        /// <returns>Lista com as instituições</returns>
        public List<Instituicao> ListarInstituicao()
        {
            try
            {
                return _contexto.Instituicao.ToList();

            }catch(Exception _e){

                throw new Exception(_e.Message);
            }
        }

        /// <summary>
        /// Buscar as informações específicas de uma instituição
        /// </summary>
        /// <param name="_idInstituicao">Código de identificação da instituição</param>
        /// <returns>Dados referente a instituição procurada</returns>
        public Instituicao BuscarInstituicaoPorId(int _idInstituicao)
        {
            try
            {
                return _contexto.Instituicao.FirstOrDefault(itt => itt.Id == _idInstituicao);

            }catch (Exception _e){

                throw new Exception(_e.Message);
            }
        }

        /// <summary>
        /// Cadastrar uma nova instituição
        /// </summary>
        /// <param name="_instituicao">Dados capturados pelo endpoint</param>
        /// <returns>Dados sobre a instituição cadastrada</returns>
        public Instituicao CadastrarInstituicao(Instituicao _instituicao)
        {
            try
            {
                _contexto.Instituicao.Add(_instituicao);

                _contexto.SaveChanges();

                return _instituicao;

            }catch (Exception _e){

                throw new Exception(_e.Message);
            }
        }

        /// <summary>
        /// Alterar os dados de uma instituição
        /// </summary>
        /// <param name="_instituicaoAlterada">Dados da insituição a serem alteradas</param>
        /// <returns>Instituição com os novos dados inseridos</returns>
        public Instituicao AlterarInstituicao(Instituicao _instituicaoAlterada)
        {
            try
            {
                Instituicao _instituicaoProcurada = BuscarInstituicaoPorId(_instituicaoAlterada.Id);
                _instituicaoProcurada.Nome = _instituicaoAlterada.Nome;
                _instituicaoProcurada.Cep = _instituicaoAlterada.Cep;
                _instituicaoProcurada.Cidade = _instituicaoAlterada.Cidade;
                _instituicaoProcurada.Bairro = _instituicaoAlterada.Bairro;
                _instituicaoProcurada.Complemento = _instituicaoAlterada.Complemento;
                _instituicaoProcurada.Logradouro = _instituicaoAlterada.Logradouro;
                _instituicaoProcurada.Uf = _instituicaoAlterada.Uf;
                _instituicaoProcurada.Numero = _instituicaoAlterada.Numero;


                _contexto.Instituicao.Update(_instituicaoProcurada);
                _contexto.SaveChanges();

                return _instituicaoAlterada;

            }catch (Exception _e){

                throw new Exception(_e.Message);
            }
           
        }
       
        /// <summary>
        /// Excluir uma instituição específica
        /// </summary>
        /// <param name="_idInstituicao">Código de identificação de um instituição</param>
        public void ExcluirInstituicao(int _idInstituicao)
        {
            try
            {
                Instituicao _instituicaoProcurando = BuscarInstituicaoPorId(_idInstituicao);

                _contexto.Curso.RemoveRange(_contexto.Curso.Where(cs => cs.IdInstituicao == _idInstituicao));

                _contexto.Instituicao.Remove(_instituicaoProcurando);

                _contexto.SaveChanges();

            }catch (Exception _e){

                throw new Exception(_e.Message);
            }
        }
    }
}
