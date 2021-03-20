using EduxProject.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Interfaces
{
    public interface IDica
    {
        List<Dica> listarDicas();
        Dica BuscarDicaPorId(int _idDica);
        Dica CadastrarDica(Dica _dica);
        Dica AlterarDica(Dica _dica);
        void ExcluirDica(int _idDica);
    }
}
