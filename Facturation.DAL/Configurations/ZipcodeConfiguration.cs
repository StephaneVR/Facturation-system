using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturation.DAL.Entities;

namespace Facturation.DAL.Configurations
{
    public class ZipcodeConfiguration : EntityTypeConfiguration<Zipcode>
    {
        public ZipcodeConfiguration()
        {
            this.ToTable("Zipcode");
            this.Property(z => z.Zipcodes);
        }
    }
}
