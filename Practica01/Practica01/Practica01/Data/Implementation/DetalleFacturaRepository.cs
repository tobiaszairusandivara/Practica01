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
    public class DetalleFacturaRepository : IDetalleFacturaRepository
    {
        private DataHelper dh;
        private SqlConnection _connection;

        public DetalleFacturaRepository()
        {
            dh = DataHelper.GetInstance();
            _connection = dh.GetConnection();
        }
        public bool Create(DetalleFactura oDetalle)
        {
            try
            {
                if (oDetalle == null)
                {
                    return false;
                }

                var parametros = new List<SQLParameter>
                {
                    new SQLParameter("@nro_factura", oDetalle.NroFactura), // Asegúrate de que esta propiedad esté definida
                    new SQLParameter("@id_articulo", oDetalle.Id_Articulo),
                    new SQLParameter("@cantidad", oDetalle.Cantidad)
                };

                return dh.ExecuteCRUDSPQuery("SP_CREAR_DETALLE_FACTURA", parametros);
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


        public List<DetalleFactura> GetAll(int id)
        {
            var parametros = new List<SQLParameter>();
            parametros.Add(new SQLParameter("@nro_factura", id));
            List<DetalleFactura> lstDetalle = new List<DetalleFactura>();
            var helper = DataHelper.GetInstance();
            var spHelp = helper.ExecuteSPQuery("SP_GET_ALL_DETALLE", parametros);
            foreach (DataRow row in spHelp.Rows)
            {
                int id_detalle = Convert.ToInt32(row["id_detalle"]);
                int id_articulo = Convert.ToInt32(row["id_articulo"]);
                int cantidad = Convert.ToInt32(row["cantidad"]);

                DetalleFactura oDetalleFactura = new DetalleFactura()
                {
                    Id_Detalle = id_detalle,
                    Id_Articulo = id_articulo,
                    Cantidad = cantidad
                };
                lstDetalle.Add(oDetalleFactura);
            }
            return lstDetalle;
        }


        public DetalleFactura GetByID(int id)
        {
            var parametros = new List<SQLParameter>();
            parametros.Add(new SQLParameter("@nro_factura", id));
            DataTable spHelpD = DataHelper.GetInstance().ExecuteSPQuery("SP_GET_ID_DETALLE", parametros);
            if(spHelpD != null && spHelpD.Rows.Count == 1)
            {
                DataRow row = spHelpD.Rows[0];
                int id_detalle = Convert.ToInt32(row["id_detalle"]);
                int id_articulo = Convert.ToInt32(row["id_articulo"]);
                int cantidad = Convert.ToInt32(row["cantidad"]);

                DetalleFactura oDetalleFactura = new DetalleFactura()
                {
                    Id_Detalle = id_detalle,
                    Id_Articulo = id_articulo,
                    Cantidad = cantidad
                };
                return oDetalleFactura;
            }
            return null;
        }
    }
}
