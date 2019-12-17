using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Facturation.DAL.Entities;

namespace Facturation.DAL.Repository
{
    public class ZipcodeRepository : IRepository<Zipcode>
    {
        private InvoiceContext _invoiceContext;

        public ZipcodeRepository(InvoiceContext invoiceContext)
        {
            _invoiceContext = invoiceContext;
        }

        public void Add(Zipcode t)
        {
            _invoiceContext.Zipcode.Add(t);
           
        }

        public Zipcode FinById(int? id)
        {
           return  _invoiceContext.Zipcode.Find(id);
        }

        public void Modify(Zipcode t)
        {
            _invoiceContext.Zipcode.AddOrUpdate(t);
        }

        public List<Zipcode> GetAll()
        {
            return _invoiceContext.Zipcode.ToList();
        }

        
    }
}
