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
    public class InvoiceController : Controller
    {
        private InvoiceLogic _invoiceLogic;
        private InvoiceDetailLogic _invoiceDetailLogic;
        private ClientLogic _clientLogic;

        public InvoiceController()
        {
            _clientLogic = new ClientLogic();
            _invoiceLogic = new InvoiceLogic();
            _invoiceDetailLogic = new InvoiceDetailLogic();
        }

        // GET: Invoice
        public ActionResult Index(string sortOrder,string searchString, string currentFilter, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var invoices = from invoice in _invoiceLogic.GetActiveInvoices()
                select invoice;
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
                invoices = invoices.Where(s => s.ClientDto.Name.Contains(searchString) || s.InvoiceCode.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    invoices = invoices.OrderByDescending(s => s.ClientDto.Name);
                    break;
                case "Date":
                    invoices = invoices.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    invoices = invoices.OrderByDescending(s => s.InvoiceCode);
                    break;
                default:
                    invoices = invoices.OrderBy(s => s.ClientDto.Name);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(invoices.ToPagedList(pageNumber,pageSize));

//            return View(_invoiceLogic.GetActiveInvoices());
        }

        // GET: Invoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InvoiceDTO invoiceDTO = _invoiceLogic.FindById(id);
            List<InvoiceDetailDTO> detailDtos = _invoiceDetailLogic.GetAll().Where(i => i.InvoiceDTOId == invoiceDTO.Id).ToList();
            invoiceDTO.InvoiceDetails = detailDtos;
            ViewBag.invoice = invoiceDTO;
            ViewBag.TotalPriceWithVAT = _invoiceLogic.GetTotalWithVat(invoiceDTO);
            ViewBag.TotalPriceWithoutVat = _invoiceLogic.GetTotalWithoutVat(invoiceDTO);
            ViewBag.PricesWithoutVat = _invoiceLogic.PriceOfDetailLineOfInvoiceWithoutVat(invoiceDTO);
            ViewBag.PricesWithVat = _invoiceLogic.PriceOfDetailLineOfInvoiceWithVat(invoiceDTO);
            return View(invoiceDTO);
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            ViewBag.ClientDTOId = new SelectList(_clientLogic.GetActiveClients(), "Id", "Name");
            return View();
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,InvoiceCode,Deleted,Finished,ClientDTOId")] InvoiceDTO invoiceDTO)
        {
            if (ModelState.IsValid)
            {
                invoiceDTO.InvoiceCode =  _invoiceLogic.GetinvoiceCode(invoiceDTO.Date);
                _invoiceLogic.Add(invoiceDTO);
                return RedirectToAction("Index");
            }

            ViewBag.ClientDTOId = new SelectList(_clientLogic.GetActiveClients(), "Id", "Name", invoiceDTO.ClientDTOId);
            return View(invoiceDTO);
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InvoiceDTO invoiceDTO = _invoiceLogic.FindById(id);
            if (invoiceDTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientDTOId = new SelectList(_clientLogic.GetActiveClients(), "Id", "Name", invoiceDTO.ClientDTOId);
            return View(invoiceDTO);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,InvoiceCode,Deleted,Finished,ClientDTOId")] InvoiceDTO invoiceDTO)
        {
            if (ModelState.IsValid)
            {
                invoiceDTO.InvoiceCode = _invoiceLogic.GetinvoiceCode(invoiceDTO.Date);
                _invoiceLogic.Modify(invoiceDTO);
                return RedirectToAction("Index");
            }
            ViewBag.ClientDTOId = new SelectList(_clientLogic.GetActiveClients(), "Id", "Name", invoiceDTO.ClientDTOId);
            return View(invoiceDTO);
        }

        // GET: Invoice/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            InvoiceDTO invoiceDTO = db.InvoiceDTOes.Find(id);
//            if (invoiceDTO == null)
//            {
//                return HttpNotFound();
//            }
//            return View(invoiceDTO);
//        }
//
//        // POST: Invoice/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            InvoiceDTO invoiceDTO = db.InvoiceDTOes.Find(id);
//            db.InvoiceDTOes.Remove(invoiceDTO);
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
