using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using miprimerasp.Models;
using Org.BouncyCastle.Asn1.Misc;
using Rotativa;

namespace miprimerasp.Controllers
{
    [Authorize]
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
                return View(db.compra.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(compra newCompra)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities())
                {
                    db.compra.Add(newCompra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;

            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    compra editCompra = db.compra.Find(id);
                    return View(editCompra);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(compra updateCompra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities())
                {
                    var findCompra = db.compra.Find(updateCompra.id);
                    findCompra.id_cliente = updateCompra.id_cliente;
                    findCompra.total = updateCompra.total;
                    findCompra.fecha = updateCompra.fecha;
                    findCompra.id_usuario = updateCompra.id_usuario;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }

        }

        public ActionResult Details(int id)
        {
            try
            {
                using (var db= new inventarioEntities())
                {
                    compra detailCompra = db.compra.Find(id);
                    return View(detailCompra);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using(var db= new inventarioEntities())
                {
                    compra deleteCompra = db.compra.Find(id);
                    db.compra.Remove(deleteCompra);
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

        public static string NombreUsuario(int? idUsuario)
        {
            using (var db = new inventarioEntities())
            {
                return db.usuario.Find(idUsuario).nombre;
            }
        }

        public ActionResult listaUsuarios()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.usuario.ToList());
            }
        }

        public static string NombreCliente(int? idCliente)
        {
            using (var db = new inventarioEntities())
            {
                return db.cliente.Find(idCliente).nombre;
            }
        }

        public ActionResult listaClientes()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.cliente.ToList());
            }
        }

        public ActionResult DetalleClienteCompra()
        {
            try
            {
                var db = new inventarioEntities();

                var query = from compra in db.compra
                            join cliente in db.cliente on compra.id_cliente equals cliente.id
                            select new ClienteCompra
                            {
                                NombreCliente = cliente.nombre,
                                TotalCompra = compra.total,
                                FechaCompra = compra.fecha,
                                NombreUsuario = compra.usuario.nombre

                            };
                return View(query);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }

        }

        public ActionResult imprimirReporte()
        {
            return new ActionAsPdf("DetalleClienteCompra") { FileName = "reporte.pdf" };
        }

        
    }
}