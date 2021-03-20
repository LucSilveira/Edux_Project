using EduxProject.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Interfaces
{
    public interface IProfessorTurma
    {
        List<ProfessorTurma> ListarTurmasDosProfessores();
        List<ProfessorTurma> ProcurarTurmasDoProfessor(int _idProfessor);
        List<ProfessorTurma> ProcurarProfessoresDaTurma(int _idTurma);
        ProfessorTurma BuscarProfessorTurmaPorId(int _idProfessorTurma);
        ProfessorTurma CadastrarTurmasDoProfessor(ProfessorTurma _professorTurma);
        ProfessorTurma AlterarTurmaDoProfessor(ProfessorTurma _professorTurma);
        void ExcluirTurmaDoProfessor(int _idProfessorTurma);
    }
}
