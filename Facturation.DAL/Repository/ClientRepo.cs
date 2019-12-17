using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Facturation.DAL.Repository;
using Facturation.DAL.Entities;


namespace Facturation.DAL.Repository
{
   public class ClientRepo: IRepository<Client>
   {
       private InvoiceContext _invoiceContext;

       public ClientRepo(InvoiceContext invoiceContext)
       {
           _invoiceContext = invoiceContext;
       }


       public void Add(Client t)
        {

            _invoiceContext.Client.Add(t);
            _invoiceContext.Zipcode.Attach(t.Zipcode);
            _invoiceContext.Entry(t.Zipcode).State = EntityState.Unchanged;


        }

        public Client FinById(int? id)
        {
            return _invoiceContext.Client.Find(id);


        }


        public void Modify(Client t)
        {
            _invoiceContext.Client.AddOrUpdate(t);
            _invoiceContext.Zipcode.Attach(t.Zipcode);
            _invoiceContext.Entry(t.Zipcode).State = EntityState.Modified;
           
        }

        public List<Client> GetAll()
        {
            var allClients = _invoiceContext.Client.SqlQuery("Select * from dbo.Clients").ToList();
            return allClients;
        }

      
    }

  
}
