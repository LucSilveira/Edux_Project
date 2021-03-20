using EduxProject.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Interfaces
{
    public interface IInstituicao
    {
        List<Instituicao> ListarInstituicao();
        Instituicao BuscarInstituicaoPorId(int _idInstituicao);
        Instituicao CadastrarInstituicao(Instituicao _instituicao);
        Instituicao AlterarInstituicao(Instituicao _instituicaoAlterada);
        void ExcluirInstituicao(int _idInstituicao);
    }
}
