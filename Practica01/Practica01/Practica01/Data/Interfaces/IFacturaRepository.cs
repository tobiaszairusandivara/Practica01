using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Practica01.Domain;

namespace Practica01.Data.Interfaces
{
    public interface IFacturaRepository
    {
        bool Create(Factura oFactura);
        List<Factura> GetAll();
        Factura GetByID(int id);
        bool Delete(int id);
    }
}
