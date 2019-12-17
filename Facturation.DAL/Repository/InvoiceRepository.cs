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
   public class InvoiceRepository : IRepository<Invoice>
   {
       private InvoiceContext InvoiceContext;

       public InvoiceRepository(InvoiceContext invoiceContext)
       {
           InvoiceContext = invoiceContext;
       }

       public void Add(Invoice t)
        {
            InvoiceContext.Invoice.Add(t);
            InvoiceContext.Client.Attach(t.Client);
            InvoiceContext.Entry(t.Client).State = EntityState.Unchanged;
        }

        public Invoice FinById(int? id)
        {
            return InvoiceContext.Invoice.Find(id);
        }

        public void Modify(Invoice t)
        {
            InvoiceContext.Invoice.AddOrUpdate(t);
            InvoiceContext.Client.Attach(t.Client);
            InvoiceContext.Entry(t.Client).State = EntityState.Modified;
        }

        public List<Invoice> GetAll()
        {
           return InvoiceContext.Invoice.ToList();
        }

     
    }
}
