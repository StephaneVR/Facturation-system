using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;


namespace Facturation.DTO
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceCode { get; set; }
        public bool Deleted { get; set; }
        public bool Finished { get; set; }
        public List<InvoiceDetailDTO> InvoiceDetails { get; set; }
        public ClientDTO ClientDto { get; set; }
        public int ClientDTOId { get; set; }
       



    }
}
