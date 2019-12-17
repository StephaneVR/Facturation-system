using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;


namespace Facturation.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Vat { get; set; }
        public Boolean Deleted { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public ZipcodeDTO ZipcodeDto { get; set; }
        public int ZipcodeDTOId { get; set; }
        public List<InvoiceDTO> InvoiceDtos { get; set; }



    }
}
