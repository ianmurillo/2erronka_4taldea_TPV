using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2taldea
{
    public class Almazena
    {
        public virtual int Id { get; set; }
        public virtual string Izena { get; set; }
        public virtual string Mota { get; set; }
        public virtual string Ezaugarria { get; set; }
        public virtual int Stock { get; set; }
        public virtual string Unitatea { get; set; }
        public virtual int Min { get; set; }
        public virtual int Max { get; set; }
        public virtual string CreatedAt { get; set; }
        public virtual int? CreatedBy { get; set; }
        public virtual string UpdatedAt { get; set; }
        public virtual int? UpdateBy { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
        public virtual int? DeletedBy { get; set; }
    }
}

