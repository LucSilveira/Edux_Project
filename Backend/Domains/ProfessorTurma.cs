using System;
using System.Collections.Generic;

namespace EduxProject.Domains
{
    public partial class ProfessorTurma
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdTurma { get; set; }

        public virtual Turma IdTurmaNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
