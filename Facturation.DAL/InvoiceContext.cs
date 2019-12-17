using System;
using System.Collections.Generic;
using System.Data.Entity;
using Facturation.DAL.Entities;
using Facturation.DAL.Configurations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturation.DAL
{
    public class InvoiceContext : DbContext,IinvoiceDatasource
    {
        public InvoiceContext():base("Facturation")
        {
        }

        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetail { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Zipcode> Zipcode { get; set; }


        IQueryable<Invoice> IinvoiceDatasource.Invoices => Invoice;

        IQueryable<Zipcode> IinvoiceDatasource.Zipcodes => Zipcode;

        IQueryable<InvoiceDetail> IinvoiceDatasource.InvoiceDetails => InvoiceDetail;

        IQueryable<Client> IinvoiceDatasource.CLients => Client;

       

        public void CreateModelUser(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClientConfiguration());
            modelBuilder.Configurations.Add(new InvoiceConfiguration());
            modelBuilder.Configurations.Add(new InvoiceDetailConfiguration());
            modelBuilder.Entity<Zipcode>().MapToStoredProcedures();
            base.OnModelCreating(modelBuilder);

        }

       

       
    }
  

}
