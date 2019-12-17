using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Facturation.DAL.Entities;

namespace Facturation.DAL.Configurations
{
    public class InvoiceDetailConfiguration : EntityTypeConfiguration<InvoiceDetail>
    {
        public InvoiceDetailConfiguration()
        {
            this.ToTable("InvoiceDetail");
            this.Property(id => id.Name).HasMaxLength(50);
        }
    }
}
