using Practica01.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Data.Interfaces
{
    public interface IDetalleFacturaRepository
    {
        bool Create(DetalleFactura oDetalleFactura);
        List<DetalleFactura> GetAll(int id);
        DetalleFactura GetByID(int id);
    }
}
