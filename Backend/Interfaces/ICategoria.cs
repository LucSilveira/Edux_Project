using EduxProject.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Interfaces
{
    public interface ICategoria
    {
        List<Categoria> ListarCategorias();
        Categoria BuscarCategoriaPorId(int _idCategoria);
        Categoria CadastrarCategoria(Categoria _categoria);
        Categoria AlterarCategoria(Categoria _categoriaAlterada);
        void ExcluirCategoria(int _idCategoria);
    }
}
