using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturation.DAL.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceCode { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public bool Deleted { get; set; }
        public bool Finished { get; set; }
        public List<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
