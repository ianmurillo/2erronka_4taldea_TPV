using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2taldea
{
    public class EskaeraPlatera
    {
        public virtual int Id { get; set; }
        public virtual string NotaGehigarriak { get; set; }
        public virtual DateTime? EskaeraOrdua { get; set; }
        public virtual DateTime? AteratzeOrdua { get; set; }
        public virtual bool Egoera { get; set; }
        public virtual bool Done { get; set; }

        // Propiedades de navegación (referencias a otras clases)
        public virtual Eskaera Eskaera { get; set; }
        public virtual Platera Platera { get; set; }
    }
}
