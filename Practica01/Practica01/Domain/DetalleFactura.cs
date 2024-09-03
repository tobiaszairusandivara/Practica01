using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Domain
{
    public class DetalleFactura
    {
        private Articulo articulo;
        private int cantidad;

        public DetalleFactura()
        {
            articulo = new Articulo();
            cantidad = 0;
        }

        public DetalleFactura(Articulo articulo, int cantidad)
        {
            this.articulo = articulo;
            this.cantidad = cantidad;
        }

        public Articulo Articulo
        {
            get { return this.articulo; }
            set { this.articulo = value; }
        }

        public int Cantidad
        {
            get { return this.cantidad; }
            set { this.cantidad = value; }
        }
    }
}
