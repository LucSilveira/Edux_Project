using System;
using System.Collections.Generic;

namespace EduxProject.Domains
{
    public partial class Curtida
    {
        public int Id { get; set; }
        public int? IdDica { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Dica IdDicaNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
