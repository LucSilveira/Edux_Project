using EduxProject.Contexts;
using EduxProject.Domains;
using EduxProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        //Instânciando nossa conexão com o banco de dados
        private readonly EduxContext _contexto;

        //Defindo o método construtor, para sempre que a classe UsuarioRepository seja instânciada
        //ela possa realizar a conexão com o nosso banco de dados;
        public UsuarioRepository()
        {
            _contexto = new EduxContext();
        }

        public List<Usuario> ListarUsuarios()
        {
            try
            {
                List<Usuario> _listaUsuarios = _contexto.Usuario.Include("IdPerfilNavigation")
                                                    .Include("ProfessorTurma").Include("AlunoTurma").ToList();

                foreach(Usuario usuario in _listaUsuarios)
                {
                    usuario.IdPerfilNavigation.Usuario = null;
                }

                return _listaUsuarios;

            }catch (Exception _e){

                throw new Exception(_e.Message);
            }
        }

        public List<Usuario> ListarAlunos()
        {
            try
            {
                List<Usuario> _listaProfessor = _contexto.Usuario
                                    .Where(usr => usr.IdPerfilNavigation.Permissao == "Aluno")
                                    .Include("AlunoTurma").ToList();

                return _listaProfessor;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public List<Usuario> ListarProfessores()
        {
            try
            {
                List<Usuario> _listaProfessor = _contexto.Usuario
                                    .Where(usr => usr.IdPerfilNavigation.Permissao == "Professor")
                                    .Include("ProfessorTurma").ToList();

                return _listaProfessor;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public Usuario BuscarUsuarioPorId(int _idUsuario)
        {
            try
            {
                return _contexto.Usuario.FirstOrDefault(usr => usr.Id == _idUsuario);

            }
            catch (Exception _e){

                throw new Exception(_e.Message);
            }
        }

        public Usuario CadastrarUsuario(Usuario _usuario)
        {
            try
            {
                Usuario _user = new Usuario
                {
                    Nome = _usuario.Nome,
                    Email = _usuario.Email,
                    Senha = _usuario.Senha,
                    IdPerfil = 1,
                    DataCadastro = DateTime.Now,
                    Imagem = "padrao.jpg"
                };

                _contexto.Usuario.Add(_user);

                /*foreach (ProfessorTurma _proff in _usuario.ProfessorTurma)
                {
                    _user.ProfessorTurma.Add(new ProfessorTurma
                    {
                        IdUsuario = _usuario.Id,
                        IdTurma = _proff.IdTurma,
                        Matricula = _proff.Matricula
                    });
                }*/

                _contexto.SaveChanges();

                return _user;

            }catch (Exception _e){

                throw new Exception(_e.Message);
            }
        }

        public Usuario CadastrarAluno(Usuario _usuario)
        {
            try
            {
                Usuario _user = new Usuario
                {
                    Nome = _usuario.Nome,
                    Email = _usuario.Email,
                    Senha = _usuario.Senha,
                    IdPerfil = 2,
                    DataCadastro = DateTime.Now,
                    Imagem = "padrao.jpg"
                };

                _contexto.Usuario.Add(_user);

                /*foreach (AlunoTurma _alunos in _usuario.AlunoTurma)
                {
                    _user.AlunoTurma.Add(new AlunoTurma
                    {
                        IdUsuario = _usuario.Id,
                        IdTurma = _alunos.IdTurma,
                        Matricula = _alunos.Matricula
                    });
                }*/

                _contexto.SaveChanges();

                return _user;

            }
            catch (Exception _e)
            {

                throw new Exception(_e.Message);
            }
        }

        public Usuario AlterarUsuario(Usuario _usuario)
        {
            try
            {
                _contexto.Entry(_usuario).State = EntityState.Modified;

                _contexto.SaveChanges();

                return _usuario;

            }catch (Exception _e){

                throw new Exception(_e.Message);
            }
        }

        public void ExcluirUsuario(int _idUsuario)
        {
            try
            {
                Usuario _usuarioProcurado = BuscarUsuarioPorId(_idUsuario);

                /*_contexto.ProfessorTurma.RemoveRange(_contexto.ProfessorTurma.Where(pff => pff.IdUsuario == _idUsuario));
                _contexto.AlunoTurma.RemoveRange(_contexto.AlunoTurma.Where(aln => aln.IdUsuario == _idUsuario));
                _contexto.ObjetivoAluno.RemoveRange(_contexto.ObjetivoAluno.Where(obj => obj.IdAlunoTurmaNavigation.IdUsuario == _idUsuario));
                _contexto.Dica.RemoveRange(_contexto.Dica.Where(dca => dca.IdUsuario == _idUsuario));*/

                _contexto.Remove(_usuarioProcurado);
                _contexto.SaveChanges();
            }
            catch (Exception _e){

                throw new Exception(_e.Message);
            }
        }
    }
}
