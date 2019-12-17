using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturation.DAL.Entities
{
    public class Zipcode
    {
        public int Id { get; set; }
        public int Zipcodes { get; set; }
        public List<Client> Clients { get; set; }

    }
}
