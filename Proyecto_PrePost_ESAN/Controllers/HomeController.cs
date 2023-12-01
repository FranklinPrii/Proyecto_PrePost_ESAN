using Proyecto_PrePost_ESAN.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_PrePost_ESAN.Controllers
{
    public class HomeController : Controller
    {

        private SisEdutivaEntities1 db = new SisEdutivaEntities1();

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Listar()
        {

            //Con procedimientos Almacenados
            var Lista = db.SP_Gestionar_Pagos(null, null, null, null, 1);



            return Json(new { data = Lista }, JsonRequestBehavior.AllowGet);



        }

        public JsonResult InsertarOActualizarPago(int idCliente, string cliente, int dni, decimal precio)
        {
            try
            {
                // Verificar si el IDCliente ya existe
                var pagoExistente = db.TBLPAGOSDT.SingleOrDefault(p => p.IDCliente == idCliente);

                if (pagoExistente == null)
                {
                    //Actualizame el pago existente
                    pagoExistente.Cliente = cliente;
                    pagoExistente.DNI = dni;
                    pagoExistente.Precio = precio;


                }
                else
                {
                    //Insertar un Nuevo registro
                    var nuevoPago = new TBLPAGOSDT
                    {
                        IDCliente = idCliente,
                        Cliente = cliente,
                        DNI = dni,
                        Precio = precio
                    };

                    db.TBLPAGOSDT.Add(nuevoPago);

                }

                db.SaveChanges();

                return Json(new { success = true, message = "Datos ingresados correctamente a la tabla." });



            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = "Error al insertar datos: " + ex.Message });
            }

        }

        public JsonResult ActualizarPago(int idCliente, string cliente, int dni, decimal precio)
        {
            var pago = db.TBLPAGOSDT.SingleOrDefault(p => p.IDCliente == idCliente);

            if (pago != null)
            {
                pago.Cliente = cliente;
                pago.DNI = dni;
                pago.Precio = precio;                                                                                       

                db.SaveChanges();

                return Json(new { success = true, message = "Pago actualizado exitosamente" });
            }

            return Json(new { success = false, message = "No se encontró el pago con IDCliente proporcionado" });
        }



    }
}