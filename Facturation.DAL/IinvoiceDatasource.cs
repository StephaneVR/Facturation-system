using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturation.DAL.Entities;



namespace Facturation.DAL
{
    interface IinvoiceDatasource
    {
        
        IQueryable<Invoice> Invoices { get; }
        IQueryable<InvoiceDetail> InvoiceDetails { get; }
        IQueryable<Zipcode> Zipcodes { get; }
        IQueryable<Client> CLients { get; }
      
      
    }
}
