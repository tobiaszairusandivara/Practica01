using Practica01.Data.Clases;
using Practica01.Domain;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        FacturaRepository repository = new FacturaRepository();

        // Crear una nueva factura
        Factura nuevaFactura = new Factura
        {
            Fecha = DateTime.Now,
            FormaPago = 4,
            Cliente = "Ramon Salazar"
        };
        bool creado = repository.Create(nuevaFactura);
        Console.WriteLine("Factura creada: " + creado);


        // Obtener todas las facturas
        List<Factura> facturas = repository.GetAll();
        Console.WriteLine("Facturas:");
        foreach (var factura in facturas)
        {
            Console.WriteLine($"NroFactura: {factura.NroFactura}, Fecha: {factura.Fecha}, FormaPago: {factura.FormaPago}, Cliente: {factura.Cliente}");
        }


        // Obtener una factura por ID
        Console.WriteLine("Ingrese el ID de la factura para consultar:");

        // Captura el ID de factura como texto
        string input = Console.ReadLine();

        // Intenta convertir la entrada a un número entero
        if (int.TryParse(input, out int id))
        {
            Factura facturaPorID = repository.GetByID(id);
            if (facturaPorID != null)
            {
                Console.WriteLine($"Factura encontrada: NroFactura: {facturaPorID.NroFactura}, Fecha: {facturaPorID.Fecha}, FormaPago: {facturaPorID.FormaPago}, Cliente: {facturaPorID.Cliente}");
            }
            else
            {
                Console.WriteLine("Factura no encontrada.");
            }
        }
        else
        {
            Console.WriteLine("ID de factura inválido.");
        }


        //Revisar pero anda, prob error de integridad de clave foranea o error con id en DB
        Console.WriteLine("Ingrese el ID de la factura para eliminar:");
        string inputDel = Console.ReadLine();

        // Intenta convertir la entrada a un número entero
        if (int.TryParse(input, out int idDel))
        {
            bool eliminada = repository.Delete(idDel);
            Console.WriteLine("Factura eliminada: " + eliminada);
        }
        else
        {
            Console.WriteLine("ID de factura inválido.");
        }


        //// Alternativa eliminar factura
        //bool eliminada = repository.Delete(1);
        //Console.WriteLine("Factura eliminada: " + eliminada);
    }
}
