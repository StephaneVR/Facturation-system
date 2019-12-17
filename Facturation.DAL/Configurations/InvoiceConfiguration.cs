using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Facturation.DAL.Entities;

namespace Facturation.DAL.Configurations
{
    public class InvoiceConfiguration : EntityTypeConfiguration<Invoice>
    {
        public InvoiceConfiguration()
        {
            this.ToTable("Invoice");
            this.Property(i => i.InvoiceCode).HasMaxLength(9)
                .IsRequired()
                ;
        }
    }
}

