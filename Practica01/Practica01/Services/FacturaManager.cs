using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Data;
using Practica01.Data.Clases;
using Practica01.Data.Interfaces;
using Practica01.Domain;

namespace Practica01.Services
{
    public class FacturaManager
    {
        private IRepository _repository;

        public FacturaManager()
        {
            _repository = new FacturaRepository();
        }
        public bool CreateFactura(Factura oFactura)
        {
            return _repository.Create(oFactura);
        }
        public List<Factura> GetFactura()
        {
            return _repository.GetAll();
        }
        public Factura GetFacturaByID(int id)
        {
            return _repository.GetByID(id);
        }
        public bool DeleteFactura(int id)
        {
            return _repository.Delete(id);
        }
    }
}
