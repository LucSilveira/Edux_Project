using System;
using System.Collections.Generic;

namespace EduxProject.Domains
{
    public partial class Turma
    {
        public Turma()
        {
            AlunoTurma = new HashSet<AlunoTurma>();
            ProfessorTurma = new HashSet<ProfessorTurma>();
            Ranking = new HashSet<Ranking>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public int? IdCurso { get; set; }

        public virtual Curso IdCursoNavigation { get; set; }
        public virtual ICollection<AlunoTurma> AlunoTurma { get; set; }
        public virtual ICollection<ProfessorTurma> ProfessorTurma { get; set; }
        public virtual ICollection<Ranking> Ranking { get; set; }
    }
}
