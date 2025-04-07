using System;
using System.Collections.Generic;

namespace _2taldea
{
    public class AlmazenaPlatera
    {
        public virtual int Id { get; set; }
        public virtual int Kantitatea { get; set; }

        public virtual Platera Platera { get; set; }
        public virtual Almazena Almazena { get; set; }
    }
}