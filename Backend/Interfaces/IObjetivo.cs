using EduxProject.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Interfaces
{
    public interface IObjetivo
    {
        List<Objetivo> listarObjetivos();
        Objetivo BuscarObjetivoPorId(int _idObjetivo);
        Objetivo CadastrarObjetivo(Objetivo _objetivo);
        Objetivo AlterarObjetivo(Objetivo _objetivo);
        void ExcluirObjetivo(int _idObjetivo);
    }
}
