using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace Facturation.DTO
{
    public class InvoiceDetailDTO
    {
        public int Id { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public int Pieces { get; set; }
        public int Vat { get; set; }
        public string Name { get; set; }
        public InvoiceDTO InvoiceDto { get; set; }
        public int InvoiceDTOId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalPriceWithoutVat { get; set; }


    }
}
