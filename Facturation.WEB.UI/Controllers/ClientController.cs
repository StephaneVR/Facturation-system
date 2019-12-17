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
using PagedList;

namespace Facturation.WEB.UI.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private ClientLogic _clientLogic;
        private ZipcodeLogic _zipcodeLogic;

        public ClientController()
        {
            _clientLogic = new ClientLogic();
            _zipcodeLogic = new ZipcodeLogic();
        }

        // GET: Client
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var clients = from client in _clientLogic.GetActiveClients()
                select client;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(s => s.Name.Contains(searchString) || s.LastName.Contains(searchString)
                                                                           || s.Email.Contains(searchString) ||
                                                                           s.Vat.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    clients = clients.OrderByDescending(s => s.Name);
                    break;
                default:
                    clients = clients.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(clients.ToPagedList(pageNumber, pageSize));

           
        }

        // GET: Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClientDTO clientDTO = _clientLogic.FinById(id);
            if (clientDTO == null)
            {
                return HttpNotFound();
            }
            return View(clientDTO);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            ViewBag.ZipcodeDTOId = new SelectList(_zipcodeLogic.GetAll(), "Id", "Zipcodes");
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LastName,Vat,Deleted,Email,Adress,ZipcodeDTOId")] ClientDTO clientDTO)
        {
            if (ModelState.IsValid)
            {
                _clientLogic.Add(clientDTO);
                return RedirectToAction("Index");
            }

            ViewBag.ZipcodeDTOId = new SelectList(_zipcodeLogic.GetAll(), "Id", "Id", clientDTO.ZipcodeDTOId);
            return View(clientDTO);
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClientDTO clientDTO = _clientLogic.FinById(id);
            if (clientDTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ZipcodeDTOId = new SelectList(_zipcodeLogic.GetAll(), "Id", "Zipcodes", clientDTO.ZipcodeDTOId);
            return View(clientDTO);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LastName,Vat,Deleted,Email,Adress,ZipcodeDTOId")] ClientDTO clientDTO)
        {
            if (ModelState.IsValid)
            {
                _clientLogic.Modify(clientDTO);
                return RedirectToAction("Index");
            }
            ViewBag.ZipcodeDTOId = new SelectList(_zipcodeLogic.GetAll(), "Id", "Zipcodes", clientDTO.ZipcodeDTOId);
            return View(clientDTO);
        }

        // GET: Client/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ClientDTO clientDTO = db.ClientDTOes.Find(id);
//            if (clientDTO == null)
//            {
//                return HttpNotFound();
//            }
//            return View(clientDTO);
//        }
//
//        // POST: Client/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            ClientDTO clientDTO = db.ClientDTOes.Find(id);
//            db.ClientDTOes.Remove(clientDTO);
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
