using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Practica01.Data.Interfaces;
using Practica01.Data.Utils;
using Practica01.Domain;
using System.Data;
using System.Data.Common;

namespace Practica01.Data.Clases
{
    public class FacturaRepository : IFacturaRepository
    {
        private DataHelper dh;
        private SqlConnection _connection;

        public FacturaRepository()
        {
            dh = DataHelper.GetInstance();
            _connection = dh.GetConnection();
        }

        public bool Create(Factura oFactura)
        {
            try
            {
                if (oFactura == null)
                {
                    return false;
                }

                var parametros = new List<SQLParameter>
                {
                    new SQLParameter("@fecha", oFactura.Fecha),
                    new SQLParameter("@id_forma_pago", oFactura.FormaPago),
                    new SQLParameter("@cliente", oFactura.Cliente)
                };

                return dh.ExecuteCRUDSPQuery("SP_CREAR_FACTURA", parametros);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Exception: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }



        public List<Factura> GetAll()
        {
            List<Factura> lstFactura = new List<Factura>();
            var helper = DataHelper.GetInstance();
            var spHelp = helper.ExecuteSPQuery("SP_GET_ALL_FACTURA", null);
            foreach(DataRow row in spHelp.Rows)
            {
                int nroFactura = Convert.ToInt32(row["nro_factura"]);
                DateTime fecha = Convert.ToDateTime(row["fecha"]);
                int formaPago = Convert.ToInt32(row["id_forma_pago"]);
                string cliente = Convert.ToString(row["cliente"]);

                Factura oFactura = new Factura()
                {
                    NroFactura = nroFactura,
                    Fecha = fecha,
                    FormaPago = formaPago,
                    Cliente = cliente
                };
                lstFactura.Add(oFactura);
            }
            return lstFactura;
        }


        public Factura GetByID(int id)
        {
            var parametros = new List<SQLParameter>();
            parametros.Add(new SQLParameter("@nro_factura", id));
            DataTable spHelp = DataHelper.GetInstance().ExecuteSPQuery("SP_GET_ID_FACTURA", parametros);
            if(spHelp != null &&  spHelp.Rows.Count == 1)
            {
                DataRow row = spHelp.Rows[0];
                int nroFactura = Convert.ToInt32(row["nro_factura"]);
                DateTime fecha = Convert.ToDateTime(row["fecha"]);
                int formaPago = Convert.ToInt32(row["id_forma_pago"]);
                string cliente = Convert.ToString(row["cliente"]);

                Factura oFactura = new Factura()
                {
                    NroFactura = nroFactura,
                    Fecha = fecha,
                    FormaPago = formaPago,
                    Cliente = cliente
                };
                return oFactura;
            }
            return null;
        }//cambiar esto


        public bool Delete(int id)
        {
            var parametros = new List<SQLParameter>();
            parametros.Add(new SQLParameter("@nro_factura", id));
            bool del = DataHelper.GetInstance().ExecuteCRUDSPQuery("SP_DEL_FACTURA", parametros);
            return del;
        }
    }
}
