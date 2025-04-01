using System;

namespace _2taldea
{
    public class Mahaia
    {
        public virtual int Id { get; set; }
        public virtual string MahailaZenbakia { get; set; }
        public virtual int Eserlekuak { get; set; }
        public virtual bool Habilitado { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }

        // Propiedades relacionadas con Eskaera
        public virtual ICollection<Eskaera> Eskaerak { get; set; }
    }
}

