using System;
using System.Collections.Generic;

namespace EduxProject.Domains
{
    public partial class Objetivo
    {
        public Objetivo()
        {
            ObjetivoAluno = new HashSet<ObjetivoAluno>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public int? IdCategoria { get; set; }
        public string Titulo { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual ICollection<ObjetivoAluno> ObjetivoAluno { get; set; }
    }
}
