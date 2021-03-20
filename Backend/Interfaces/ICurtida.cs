using EduxProject.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Interfaces
{
    public interface ICurtida
    {
        List<Curtida> ListarCurtidas();
        Curtida BuscarCurtidasPorId(int _idCurtida);
        Curtida CadastrarCurtidas(Curtida _curtida);
        void ExcluirCurtida(int _idCurtida);
    }
}
