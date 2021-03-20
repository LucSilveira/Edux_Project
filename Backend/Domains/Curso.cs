using System;
using System.Collections.Generic;

namespace EduxProject.Domains
{
    public partial class Curso
    {
        public Curso()
        {
            Turma = new HashSet<Turma>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public int? IdInstituicao { get; set; }

        public virtual Instituicao IdInstituicaoNavigation { get; set; }
        public virtual ICollection<Turma> Turma { get; set; }
    }
}
