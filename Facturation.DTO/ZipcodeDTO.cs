using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Facturation.DTO
{
    public class ZipcodeDTO
    {
        public int Id { get; set; }
        public int Zipcodes { get; set; }
        public List<ClientDTO> ClientDtos { get; set; }

    }
}
