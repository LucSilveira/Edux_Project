using EduxProject.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Interfaces
{
    public interface ICurso
    {
        List<Curso> ListarCursos();
        Curso BuscarCursoPorId(int _idCurso);
        Curso CadastrarCurso(Curso _curso);
        Curso AlterarCurso(Curso _curso);
        void ExcluirCurso(int _idCurso);
    }
}
