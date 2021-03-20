using EduxProject.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Interfaces
{
    interface IObjetivoAluno
    {
        List<ObjetivoAluno> ListarObjetivosDosAlunos();
        List<ObjetivoAluno> ProcurarAlunosPorObjetivo(int _idObjetivo);
        List<ObjetivoAluno> ProcurarObjetivosPorAluno(int _idAluno);
        ObjetivoAluno BuscarObjetivoDoAlunoPorId(int _idObjetivoAluno);
        Task<ObjetivoAluno> CadastrarObjetivosDoAluno(ObjetivoAluno _objetivoAluno);
        Task<ObjetivoAluno> AlterarObjetivosDoAluno(ObjetivoAluno _objetivoAluno);
        Task<ObjetivoAluno> ExcluirObjetivoDoAluno(int _idObjetivoAluno);
    }
}
