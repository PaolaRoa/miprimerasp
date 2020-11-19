using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;
using Antlr.Runtime;
using miprimerasp.Models;
using Renci.SshNet.Messages;

namespace miprimerasp.Controllers
{
    public class UsuarioController : Controller
    {
        [Authorize]
        // GET: Usuario
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {

                return View(db.usuario.ToList());
            }

        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //decoradores//
        [HttpPost]// va a recibir datos
        [ValidateAntiForgeryToken]//evita datos errados

        [Authorize]
        public ActionResult Create(usuario newUser)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using(var db=new inventarioEntities())
                {
                    db.usuario.Add(newUser);
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

        [Authorize]
        public ActionResult Edit(int id)
        {
            try 
            { 
                using(var db= new inventarioEntities())
                {
                    usuario findUser = db.usuario.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);
                }
            
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;

            }

        }
        //decoradores//
        [HttpPost]// va a recibir datos
        [ValidateAntiForgeryToken]//evita datos errados
        [Authorize]
        public ActionResult Edit(usuario updateUser)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    usuario objUser = db.usuario.Find(updateUser.id);
                    objUser.nombre = updateUser.nombre;
                    objUser.apellido = updateUser.apellido;
                    objUser.fecha_nacimiento = updateUser.fecha_nacimiento;
                    objUser.password = updateUser.password;

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

        [Authorize]
        public ActionResult Details(int id)
        {
            try
            {
                using(var db= new inventarioEntities())
                {
                    usuario detailUser = db.usuario.Find(id);

                    return View(detailUser);

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;

            }
        }

        [Authorize]
        public ActionResult Delete (int id)
        {
            try
            {
                using (var db= new inventarioEntities())
                {
                    usuario userDelete = db.usuario.Find(id);
                    db.usuario.Remove(userDelete);
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
        
        public ActionResult Login (String message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login (string user, string password)
        {
            try
            {
                using (var db= new inventarioEntities())
                {
                    var userLog = db.usuario.FirstOrDefault(u => u.email == user && u.password == password);
                    if(userLog != null)
                    {
                        FormsAuthentication.SetAuthCookie(userLog.email, true);
                        return RedirectToAction("Index", "Producto");
                    }
                    else
                    {
                        return Login("Datos erroneos, verifiquelos");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

        [Authorize]

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
