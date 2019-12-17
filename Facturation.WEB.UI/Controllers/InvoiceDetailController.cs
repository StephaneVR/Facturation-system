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
    public class InvoiceDetailController : Controller
    {
        
        private InvoiceDetailLogic _invoiceDetailLogic;
        private InvoiceLogic _invoiceLogic;

        public InvoiceDetailController()
        {
            _invoiceLogic = new InvoiceLogic();
            _invoiceDetailLogic = new InvoiceDetailLogic();
        }

        // GET: InvoiceDetail
        public ActionResult Index()
        {
            
            return View(_invoiceDetailLogic.GetAll());
        }

        // GET: InvoiceDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InvoiceDTO invoiceDetailDTO = _invoiceLogic.FindById(id);
            InvoiceDetailDTO invoiceDetailDto = _invoiceDetailLogic.FindById(invoiceDetailDTO.Id);
            ViewBag.priceWithVat = _invoiceDetailLogic.TotalPriceWithVat(invoiceDetailDto);
            ViewBag.priceWithoutVAT = _invoiceDetailLogic.TotalPriceWithoutVat(invoiceDetailDto);
            return View(invoiceDetailDto);
        }

        // GET: InvoiceDetail/Create
        public ActionResult Create(InvoiceDTO invoiceDto)
        {
            
            ViewBag.InvoiceDTOId = new SelectList(_invoiceLogic.GetActiveInvoices().Where(i => i.Id == invoiceDto.Id), "Id", "InvoiceCode");
            return View();

        }

        // POST: InvoiceDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Discount,Price,Pieces,Vat,Name,InvoiceDTOId,TotalPrice,TotalPriceWithoutVat")] InvoiceDetailDTO invoiceDetailDTO)
        {
            if (ModelState.IsValid)
            {
                    _invoiceDetailLogic.Add(invoiceDetailDTO);
                    return RedirectToAction("Details" , "Invoice",new{id = invoiceDetailDTO.InvoiceDTOId});
            }
            ViewBag.InvoiceDTOId = new SelectList(_invoiceLogic.GetActiveInvoices().Where(i => i.Id == invoiceDetailDTO.InvoiceDTOId), "Id", "InvoiceCode", invoiceDetailDTO.InvoiceDTOId);
            return View(invoiceDetailDTO);
        }

        // GET: InvoiceDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InvoiceDetailDTO invoiceDetailDTO = _invoiceDetailLogic.FindById(id);
            if (invoiceDetailDTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceDTOId = new SelectList(_invoiceLogic.GetActiveInvoices(), "Id", "InvoiceCode", invoiceDetailDTO.InvoiceDTOId);
            return View(invoiceDetailDTO);
        }

        // POST: InvoiceDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Discount,Price,Pieces,Vat,Name,InvoiceDTOId,TotalPrice,TotalPriceWithoutVat")] InvoiceDetailDTO invoiceDetailDTO)
        {
            if (ModelState.IsValid)
            {
                _invoiceDetailLogic.Modify(invoiceDetailDTO);
                return RedirectToAction("Details" , "Invoice",new {id = invoiceDetailDTO.InvoiceDTOId});
            }
            ViewBag.InvoiceDTOId = new SelectList(_invoiceLogic.GetActiveInvoices(), "Id", "InvoiceCode", invoiceDetailDTO.InvoiceDTOId);
            return View(invoiceDetailDTO);
        }

        //GET: InvoiceDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InvoiceDetailDTO invoiceDetailDTO = _invoiceDetailLogic.FindById(id);
            if (invoiceDetailDTO == null)
            {
                return HttpNotFound();
            }
            return View(invoiceDetailDTO);
        }

        // POST: InvoiceDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceDetailDTO invoiceDetailDTO = _invoiceDetailLogic.FindById(id);
            _invoiceDetailLogic.Remove(invoiceDetailDTO);
            return RedirectToAction("Details","Invoice",new{invoiceDetailDTO.InvoiceDto.Id});
        }
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
