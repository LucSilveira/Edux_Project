using EduxProject.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Interfaces
{
    interface IRanking
    {
        List<Ranking> ListarRanking();
        List<Ranking> ListarRankingDaTurma(int _idTurma);
        Ranking AdicionarAlunoAoRanking(AlunoTurma _aluno);
        Ranking BuscarRankingDoAluno(string _nomeAluno);
        Task<Ranking> SomarNotaRankingAluno(ObjetivoAluno _aluno, AlunoTurma _alunoTurma);
        Task<Ranking> AlterarNotaRankingAluno(ObjetivoAluno _objetivoNovo, ObjetivoAluno _objetivoAntigo, AlunoTurma _alunoTurma);
        Task<Ranking> DiminuirNotaRankingAluno(ObjetivoAluno _aluno, AlunoTurma _alunoTurma);
    }
}
