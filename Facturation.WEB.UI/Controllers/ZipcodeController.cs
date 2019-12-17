using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Facturation.BLL;
using Facturation.DAL;
using Facturation.DTO;

namespace Facturation.WEB.UI.Controllers
{
    [Authorize]
    public class ZipcodeController : Controller
    {
        private ZipcodeLogic _zipcodeLogic;

        public ZipcodeController()
        {
            _zipcodeLogic = new ZipcodeLogic();
        }

        // GET: Zipcode
        public ActionResult Index()
        {
            return View(_zipcodeLogic.GetAll());
        }

        // GET: Zipcode/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ZipcodeDTO zipcodeDTO = _zipcodeLogic.FindById(id);
            if (zipcodeDTO == null)
            {
                return HttpNotFound();
            }
            return View(zipcodeDTO);
        }

        // GET: Zipcode/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Zipcode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Zipcodes")] ZipcodeDTO zipcodeDTO)
        {
            if (ModelState.IsValid)
            {
                _zipcodeLogic.Add(zipcodeDTO);
                return RedirectToAction("Create","Client");
            }

            return View(zipcodeDTO);
        }

        // GET: Zipcode/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ZipcodeDTO zipcodeDTO = _zipcodeLogic.FindById(id);
            if (zipcodeDTO == null)
            {
                return HttpNotFound();
            }
            return View(zipcodeDTO);
        }

        // POST: Zipcode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Zipcodes")] ZipcodeDTO zipcodeDTO)
        {
            if (ModelState.IsValid)
            {
                _zipcodeLogic.Modify(zipcodeDTO);
                return RedirectToAction("Index");
            }
            return View(zipcodeDTO);
        }

        // GET: Zipcode/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ZipcodeDTO zipcodeDTO = db.ZipcodeDTOes.Find(id);
//            if (zipcodeDTO == null)
//            {
//                return HttpNotFound();
//            }
//            return View(zipcodeDTO);
//        }
//
//        // POST: Zipcode/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            ZipcodeDTO zipcodeDTO = db.ZipcodeDTOes.Find(id);
//            db.ZipcodeDTOes.Remove(zipcodeDTO);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }
//
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
    }
}
