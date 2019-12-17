using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Facturation.DTO;
using Facturation.WEB.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Facturation.WEB.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        ApplicationDbContext context;
            public RoleController()
            {
                context = new ApplicationDbContext();
            }

            public ActionResult Index()
            {
                var Roles = context.Roles.ToList();
                return View(Roles);
            }

            // GET: Role
            public ActionResult Create()
            {

                return View();
            }
            //POST Role
            [HttpPost]
            public ActionResult Create(IdentityRole role)
            {
                
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                if (! roleManager.RoleExists(role.Name))
                {
                    context.Roles.Add(role);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
               
                return RedirectToAction("Index");
            }
//            GET: Invoice/Delete/5
                   public ActionResult Delete(int? id)
                   {
                       if (id == null)
                       {
                           return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                       }

                       IdentityRole role = context.Roles.Find(id);
                       if (role == null)
                       {
                           return HttpNotFound();
                       }
                       return View(role);
                   }
            
                   // POST: Invoice/Delete/5
                   [HttpPost]
                   public ActionResult Delete(int id)
                   {
                       IdentityRole role = context.Roles.Find(id);
                       context.Roles.Remove(role);
                       context.SaveChanges();
                       return RedirectToAction("Index");
                   }
 
    }
}