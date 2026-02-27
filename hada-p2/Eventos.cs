using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class TocandoArgs : EventArgs
    {
        public String nombre { get; set; }
        public Coordenada coordenadaImpacto { get; set; }

        public TocandoArgs(String nombre, Coordenada coordenadaImpacto)
        {
            this.nombre = nombre;
            this.coordenadaImpacto = coordenadaImpacto;
        }
    }

    public class HundidoArgs : EventArgs
    {
        public String nombre { get; set; }

        public HundidoArgs(String nombre)
        {
            this.nombre= nombre;
        }
    }
}
