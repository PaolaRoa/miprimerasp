using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using miprimerasp.Models;
using Rotativa;
namespace miprimerasp.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            using (var db= new inventarioEntities())
            {
                return View(db.producto.ToList());
            }
            
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(producto newProducto)
        {
            if (!ModelState.IsValid)
                return View();
            try 
            {
                using (var db = new inventarioEntities())
                {
                    db.producto.Add(newProducto);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }

           
            }
            catch(Exception ex)
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
                using(var db = new inventarioEntities())
                {
                   var showProducto= db.producto.Find(id);
                    return View(showProducto);
                    
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        
        public ActionResult Edit(producto updateProducto)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto objProducto = db.producto.Find(updateProducto.id);
                    objProducto.nombre = updateProducto.nombre;
                    objProducto.percio_unitario = updateProducto.percio_unitario;
                    objProducto.descripcion = updateProducto.descripcion;
                    objProducto.id_proveedor = updateProducto.id_proveedor;
                    objProducto.cantidad = updateProducto.cantidad;

                    db.SaveChanges();
                    return RedirectToAction("index");

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
                   producto detailProducto=  db.producto.Find(id);
                    return View(detailProducto);

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
                using (var db = new inventarioEntities())
                {
                    producto deleteProducto = db.producto.Find(id);
                    db.producto.Remove(deleteProducto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

      
        public static string NombreProveedor(int? idProveedor)
        {
            using (var db = new inventarioEntities())
            {
                return db.proveedor.Find(idProveedor).nombre;
            }
        }

        
           
        public ActionResult listaProveedores()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.proveedor.ToList());
            }
        }

        public ActionResult reporteProductos()
        {
            using(var db = new inventarioEntities())
            {
                return View(db.producto.ToList());
            }
        }

        public ActionResult imprimirReporte()
        {
            return new ActionAsPdf("reporteProductos") { FileName= "reporte.pdf" };
        }

        public ActionResult addImage(String message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]

        public ActionResult addImage(HttpPostedFileBase productImage)
        {

        }
    }

    
    

}