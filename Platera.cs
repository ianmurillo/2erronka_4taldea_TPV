using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2taldea
{
    public class Platera
    {
        public virtual int Id { get; set; }
        public virtual string Izena { get; set; }
        public virtual string Deskribapena { get; set; }
        public virtual string Mota { get; set; }
        public virtual string PlateraMota { get; set; }
        public virtual double Prezioa { get; set; }
        public virtual int? Menu { get; set; }
        public virtual string CreatedAt { get; set; }
        public virtual int? CreatedBy { get; set; }
        public virtual string UpdatedAt { get; set; }
        public virtual int? UpdatedBy { get; set; }
        public virtual string DeletedAt { get; set; }
        public virtual int? DeletedBy { get; set; }

        // Relación con EskaeraPlatera
        public virtual ICollection<EskaeraPlatera> EskaeraPlaterak { get; set; }
    }
}

