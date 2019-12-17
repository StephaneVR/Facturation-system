using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Facturation.DAL.Entities
{
   public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Vat { get; set; }
        public Boolean Deleted { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public Zipcode Zipcode { get; set; }
        public int ZipcodeId{ get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
