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

        private SisEdutivaEntities3 db = new SisEdutivaEntities3();

        //HOLAAAA

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult Listar()
        {

            //Con procedimientos Almacenados
            var Lista = db.SP_Gestionar_Pagos(null, null, null, null, 1,null,null);

            return Json(new { data = Lista }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GuardarDatos(string username , string password )
        {
            var user = "abc";
            var pass = "dfg";

            if (user == username && pass == password)
            {
                return Json(1);

            }
            else
            {
                return Json(2);
            }

            
        }

        [HttpPost]
        public JsonResult Insertar(int idCliente, string cliente, int dni, decimal precio,DateTime fechainicio,DateTime fechafin)
        {                        
                    //Insertar un Nuevo registro
                    var nuevoPago = new TBLPAGOSDT
                    {
                        
                        Cliente = cliente,
                        DNI = dni,
                        Precio = precio,
                        FechaInicio = fechainicio,
                        FechaFin = fechafin
                    };

                    db.TBLPAGOSDT.Add(nuevoPago);

                        db.SaveChanges();

                        return Json(new { success = true, message = "Datos ingresados correctamente a la tabla." });

        }

        [HttpPost]
        public JsonResult ActualizarPago(int idCliente, string cliente, int dni, decimal precio, DateTime fechainicio, DateTime fechafin)
        {
            var pago = db.TBLPAGOSDT.SingleOrDefault(p => p.IDCliente == idCliente);

            if (pago != null)
            {
                pago.Cliente = cliente;
                pago.DNI = dni;
                pago.Precio = precio;
                pago.FechaInicio = fechainicio;
                pago.FechaFin = fechafin;

                db.SaveChanges();

                return Json(new { success = true, message = "Pago actualizado exitosamente" });
            }

            return Json(new { success = false, message = "No se encontró el pago con IDCliente proporcionado" });
        }




        [HttpPost]
        public JsonResult EliminarPago(int idCliente)
        {
            var pago = db.TBLPAGOSDT.SingleOrDefault(p => p.IDCliente == idCliente);

            if (pago != null)
            {
                db.TBLPAGOSDT.Remove(pago);
                db.SaveChanges();

                return Json(new { success = true, message = "Pago eliminado exitosamente" });
            }

            return Json(new { success = false, message = "No se encontró el pago con IDCliente proporcionado" });
        }


    }
}