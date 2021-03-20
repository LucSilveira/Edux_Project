using EduxProject.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Interfaces
{
    public interface ITurma
    {
        List<Turma> ListarTurmas();
        Turma BuscarTurmaPorId(int _idTurma);
        Turma CadastrarTurma(Turma _turma);
        Turma AlterarTurma(Turma _turmaAlterada);
        void ExcluirTurma(int _idTurma);
    }
}
