﻿using miprimerasp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace miprimerasp.Controllers
{
    [Authorize]
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db= new inventarioEntities())
            {
                return View(db.proveedor.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(proveedor newProveedor)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db= new inventarioEntities())
                {
                    db.proveedor.Add(newProveedor);
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

        public ActionResult Edit (int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    proveedor editProveedor= db.proveedor.Find(id);
                    return View(editProveedor);
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

        public ActionResult Edit(proveedor updateProveedor)
        {
            try
            {
                using(var db= new inventarioEntities())
                {
                    proveedor objPro = db.proveedor.Find(updateProveedor.id);
                    objPro.direccion = updateProveedor.direccion;
                    objPro.nombre = updateProveedor.nombre;
                    objPro.nombre_contacto = updateProveedor.nombre_contacto;
                    objPro.telefono = updateProveedor.telefono;

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
                    proveedor detailproveedor = db.proveedor.Find(id);
                    return View(detailproveedor);

                }
            }
            catch (Exception ex)
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
                using (var db= new inventarioEntities())
                {
                    proveedor delProveedor =db.proveedor.Find(id);
                    db.proveedor.Remove(delProveedor);
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

        public ActionResult DetalleProveedorProducto()
        {
            var db= new inventarioEntities();
            var query = from proveedor in db.proveedor
                        join producto in db.producto on proveedor.id equals producto.id_proveedor
                        select new ProductoProveedor
                        {
                            proveedor = proveedor.nombre,
                            producto = producto.nombre,
                            telefono = proveedor.telefono,
                            descripcion = producto.descripcion,
                            precioUnitario = producto.percio_unitario
                            
                        };
            return View(query);


        }
            
        }
    }

