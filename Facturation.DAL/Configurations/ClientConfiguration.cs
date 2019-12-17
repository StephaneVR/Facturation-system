using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Facturation.DAL.Entities;


namespace Facturation.DAL.Configurations
{
    class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            this.ToTable("Client");
            this.Property(c => c.Vat).HasMaxLength(10);
            this.Property(c => c.Email).IsRequired();
            this.Property(c => c.Name).HasMaxLength(50);
            this.Property(c => c.LastName).HasMaxLength(50);
        }


    }
}
