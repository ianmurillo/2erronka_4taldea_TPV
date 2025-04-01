using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2taldea
{
    public class Langilea
    {
        public virtual int Id { get; set; }
        public virtual string Izena { get; set; }
        public virtual string Abizena { get; set; }
        public virtual string Pasahitza { get; set; }
        public virtual string Email { get; set; }
        public virtual int? NivelPermisos { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
        public virtual bool? TxatBaimena { get; set; }

        public virtual string UserName { get; set; }
    }
}
