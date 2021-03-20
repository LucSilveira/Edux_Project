using System;
using System.Collections.Generic;

namespace EduxProject.Domains
{
    public partial class Ranking
    {
        public int Id { get; set; }
        public decimal? NotaTotal { get; set; }
        public string NomeAluno { get; set; }
        public int? ObjetivosOculto { get; set; }
        public int? IdTurma { get; set; }

        public virtual Turma IdTurmaNavigation { get; set; }
    }
}
