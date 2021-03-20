using EduxProject.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Interfaces
{
    public interface IPerfil
    {
        List<Perfil> ListarPerfils();
        Perfil BuscarPerfilPorId(int _idPerfil);
        Perfil CadastrarPerfil(Perfil _perfil);
        Perfil AlterarPerfil(Perfil _perfil);
        void ExcluirPerfil(int _idPerfil);
    }
}
