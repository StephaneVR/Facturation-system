using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturation.DAL.Entities
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public int Pieces { get; set; }
        public int Vat { get; set; }
        public string Name { get; set; }
        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }
  
    }
}
