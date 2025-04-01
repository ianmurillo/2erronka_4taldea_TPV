using System;

namespace _2taldea
{
    public class Eskaera
    {
        public virtual int Id { get; set; }
        public virtual bool Egoera { get; set; }
        public virtual bool? Done { get; set; }
        public virtual DateTime? EskaeraDone { get; set; }
        public virtual bool Ordainduta { get; set; }

        // Relación con la tabla Platera a través de la tabla EskaeraPlatera
        public virtual ICollection<EskaeraPlatera> EskaeraPlaterak { get; set; }

        // Propiedades de navegación (referencias a otras clases)
        public virtual Langilea Langilea { get; set; }
        public virtual Mahaia Mahaila { get; set; }
    }
}
