using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturation.DAL.Repository;

namespace Facturation.DAL
{
    public class UnitOfWork : IDisposable
    {
        private InvoiceContext context = new InvoiceContext();
        private ClientRepo _clientRepo;
        private InvoiceDetailRepository _invoiceDetailRepository;
        private InvoiceRepository _invoiceRepository;
        private ZipcodeRepository _zipcodeRepository;
        public ClientRepo ClientRepo
        {
            get
            {

                if (this._clientRepo == null)
                {
                    this._clientRepo = new ClientRepo(context);
                }
                return _clientRepo;
            }
        }
        public InvoiceRepository InvoiceRepository
        {
            get
            {

                if (this._invoiceRepository == null)
                {
                    this._invoiceRepository = new InvoiceRepository(context);
                }
                return _invoiceRepository;
            }
        }
        public InvoiceDetailRepository InvoiceDetailRepository
        {
            get
            {

                if (this._invoiceDetailRepository == null)
                {
                    this._invoiceDetailRepository = new InvoiceDetailRepository(context);
                }
                return _invoiceDetailRepository;
            }
        }
        public ZipcodeRepository ZipcodeRepository
        {
            get
            {

                if (this._zipcodeRepository == null)
                {
                    this._zipcodeRepository = new ZipcodeRepository(context);
                }
                return _zipcodeRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}




