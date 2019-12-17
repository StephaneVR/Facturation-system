using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturation.DAL.Entities;

namespace Facturation.DAL.Repository
{
    public class InvoiceDetailRepository : IRepository<InvoiceDetail>
    {
        private InvoiceContext InvoiceContext;

        public InvoiceDetailRepository(InvoiceContext invoiceContext)
        {
            InvoiceContext = invoiceContext;
        }


        public void Add(InvoiceDetail t)
        {
            InvoiceContext.InvoiceDetail.Add(t);
            InvoiceContext.Invoice.Attach(t.Invoice);
            InvoiceContext.Entry(t.Invoice).State = EntityState.Unchanged;
        }

        public InvoiceDetail FinById(int? id)
        {
            return InvoiceContext.InvoiceDetail.Find(id);
        }

        public void Modify(InvoiceDetail t)
        {
            InvoiceContext.InvoiceDetail.AddOrUpdate(t);
            InvoiceContext.Invoice.Attach(t.Invoice);
            InvoiceContext.Entry(t.Invoice).State = EntityState.Modified;
            
        }

        public void Remove(InvoiceDetail a)
        {

            var invoice  = FinById(a.Id);
            InvoiceContext.InvoiceDetail.Remove(invoice);
        }

        public List<InvoiceDetail> GetAll()
        {
            return InvoiceContext.InvoiceDetail.ToList();
        }

       
    }
}
