using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Facturation.DAL;
using Facturation.DAL.Entities;
using Facturation.DAL.Repository;
using Facturation.DTO;

namespace Facturation.BLL
{
    public class InvoiceLogic
    {
        private UnitOfWork _unitOfWork;
        private InvoiceDetailLogic _invoiceDetailLogic;

        public InvoiceLogic()
        {
            _unitOfWork =new UnitOfWork();
            _invoiceDetailLogic = new InvoiceDetailLogic();
        }
        public static Invoice Map(InvoiceDTO e)
        {
           UnitOfWork _unitOfWork = new UnitOfWork();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<InvoiceDTO, Invoice>());
            var mapper = config.CreateMapper();
            mapper = new Mapper(config);
            Invoice entity = mapper.Map<Invoice>(e);
            entity.ClientId = e.ClientDTOId;
            entity.Client = _unitOfWork.ClientRepo.FinById(entity.ClientId);
            return entity;

        }
        public static InvoiceDTO Map(Invoice e)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Invoice, InvoiceDTO>());
            var mapper = config.CreateMapper();
            mapper = new Mapper(config);
            InvoiceDTO dto = mapper.Map<InvoiceDTO>(e);
            dto.ClientDTOId = e.ClientId;
            ClientLogic clogic = new ClientLogic(); 
            dto.ClientDto = clogic.FinById(dto.ClientDTOId);
            return dto;

        }

        

        public List<InvoiceDTO> GetActiveInvoices()
        {
            
            List<Invoice> details = _unitOfWork.InvoiceRepository.GetAll().Where(i => i.Finished == false).ToList();
            List<InvoiceDTO> invoiceDtos = new List<InvoiceDTO>();

            foreach (Invoice c in details)
            {

                invoiceDtos.Add(Map(c));
            }

            return invoiceDtos;
        }
        public List<InvoiceDTO>GetactiveWhenFinish()
        {

            return GetActiveInvoices().Where(i=> i.Finished == false).ToList();
        }

        public string GetinvoiceCode(DateTime invoicedate)
        {
            
            string year = invoicedate.ToString("yyyy");
            string month = invoicedate.ToString("MM");
            int counter = GetbiggestCounter(invoicedate);
            return $"{year}{month}-{++counter:0000}";
        }

        public int GetbiggestCounter(DateTime invoiceTime)
        {
            List<InvoiceDTO> invoiceDtos = GetActiveInvoices();
            List<InvoiceDTO> sameMonthInvoices = invoiceDtos.Where(i => i.Date.Month == invoiceTime.Month).ToList();
            if (sameMonthInvoices.Count == 0)
            {
                return 0;
            }



            return sameMonthInvoices.Max(i => int.Parse(i.InvoiceCode.Split('-')[1]));
        }

        public void Add(InvoiceDTO i)
        {
            _unitOfWork.InvoiceRepository.Add(Map(i));
            _unitOfWork.Save();
            
        }

        public void Modify(InvoiceDTO i)
        {
            _unitOfWork.InvoiceRepository.Modify(Map(i));
            _unitOfWork.Save();
            
        }
        
        public InvoiceDTO FindById(int? id)
        {
            Invoice invoice = _unitOfWork.InvoiceRepository.FinById(id);
            return Map(invoice);
        }

        public decimal GetTotalWithVat(InvoiceDTO a)
        {
            Invoice invoice = _unitOfWork.InvoiceRepository.FinById(a.Id);
            List<InvoiceDetailDTO> invoiceDetails = _invoiceDetailLogic.GetAll().Where(i => i.InvoiceDTOId == invoice.Id).ToList();

            decimal price = 0;
            foreach (var item in invoiceDetails)
            {
                price += _invoiceDetailLogic.TotalPriceWithVat(item);
            }
            
            return price;

        }
        public decimal GetTotalWithoutVat(InvoiceDTO a)
        {
            Invoice invoice = _unitOfWork.InvoiceRepository.FinById(a.Id);
            List<InvoiceDetailDTO> invoiceDetails = _invoiceDetailLogic.GetAll().Where(i => i.InvoiceDTOId == invoice.Id).ToList();
            decimal price = 0;
            foreach (var item in invoiceDetails)
            {
                price = price + _invoiceDetailLogic.TotalPriceWithoutVat(item);
            }

            return price;

        }
        public decimal[]PriceOfDetailLineOfInvoiceWithVat(InvoiceDTO a)
        {
            Invoice invoice = _unitOfWork.InvoiceRepository.FinById(a.Id);
            List<InvoiceDetailDTO> InvoiceDetailDTO = _invoiceDetailLogic.GetAll().Where(i => i.InvoiceDTOId == invoice.Id).ToList();
            decimal[] price = new decimal[InvoiceDetailDTO.Count];
            for (int i = 0; i < InvoiceDetailDTO.Count; i++)
            {
                price[i] = _invoiceDetailLogic.TotalPriceWithVat(InvoiceDetailDTO[i]);
            }
            
            return price;
        }
        public decimal[] PriceOfDetailLineOfInvoiceWithoutVat(InvoiceDTO a)
        {
            Invoice invoice = _unitOfWork.InvoiceRepository.FinById(a.Id);
            List<InvoiceDetailDTO> InvoiceDetailDTO = _invoiceDetailLogic.GetAll().Where(i => i.InvoiceDTOId == invoice.Id).ToList();
            decimal[] price = new decimal[InvoiceDetailDTO.Count];
            for (int i = 0; i < InvoiceDetailDTO.Count; i++)
            {
                price[i] = _invoiceDetailLogic.TotalPriceWithoutVat(InvoiceDetailDTO[i]);
            }

            return price;
        }




    }
}
