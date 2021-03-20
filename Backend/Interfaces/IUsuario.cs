using EduxProject.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Interfaces
{
    public interface IUsuario
    {
        List<Usuario> ListarUsuarios();
        List<Usuario> ListarProfessores();
        List<Usuario> ListarAlunos();
        Usuario BuscarUsuarioPorId(int _idUsuario);
        Usuario CadastrarUsuario(Usuario _usuario);
        Usuario CadastrarAluno(Usuario _usuario);
        Usuario AlterarUsuario(Usuario _usuario);
        void ExcluirUsuario(int _idUsuario);
    }
}
