using EduxProject.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Interfaces
{
    public interface IAlunoTurma
    {
        List<AlunoTurma> ListarTurmasDosAlunos();
        List<AlunoTurma> ProcurarTurmasDoAluno(int _idAluno);
        List<AlunoTurma> ProcurarAlunosDaTurma(int _idTurma);
        AlunoTurma BuscarAlunoTurmaPorId(int _idAlunoTurma);
        AlunoTurma CadastrarTurmasDoAluno(AlunoTurma _alunoTurma);
        AlunoTurma AlterarTurmaDoAluno(AlunoTurma _alunoTurma);
        void ExcluirTurmaDoAluno(int _idAlunoTurma);
    }
}
