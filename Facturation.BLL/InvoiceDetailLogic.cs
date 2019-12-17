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
   public class InvoiceDetailLogic
   {
       private UnitOfWork _unitOfWork;
       

       public InvoiceDetailLogic()
       {
           _unitOfWork = new UnitOfWork();


       }
        public static InvoiceDetail Map(InvoiceDetailDTO e)
        {
            UnitOfWork _unitOfWork = new UnitOfWork();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<InvoiceDetailDTO, InvoiceDetail>());
            var mapper = config.CreateMapper();
            mapper = new Mapper(config);
            InvoiceDetail entity = mapper.Map<InvoiceDetail>(e);
            entity.InvoiceId = e.InvoiceDTOId;
            entity.Invoice = _unitOfWork.InvoiceRepository.FinById(entity.InvoiceId);
            return entity;

        }
        public static InvoiceDetailDTO Map(InvoiceDetail e)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<InvoiceDetail, InvoiceDetailDTO>());
            var mapper = config.CreateMapper();
            mapper = new Mapper(config);
            InvoiceDetailDTO dto = mapper.Map<InvoiceDetailDTO>(e);
            dto.InvoiceDTOId = e.InvoiceId;
            InvoiceLogic invoiceLogic = new InvoiceLogic();
            dto.InvoiceDto = invoiceLogic.FindById(dto.InvoiceDTOId);
            return dto;

        }

        public void Add(InvoiceDetailDTO id)
        {
            _unitOfWork.InvoiceDetailRepository.Add(Map(id));
            _unitOfWork.Save();
          
        }

        public void Modify(InvoiceDetailDTO id)
        {
            _unitOfWork.InvoiceDetailRepository.Modify(Map(id));
            _unitOfWork.Save();
            
        }

        public InvoiceDetailDTO FindById(int? id)
        {
            InvoiceDetail invoiceDetail = _unitOfWork.InvoiceDetailRepository.FinById(id);
            return Map(invoiceDetail);
        }

        public List<InvoiceDetailDTO> GetAll()
        {
            List<InvoiceDetail> details = _unitOfWork.InvoiceDetailRepository.GetAll();
            List<InvoiceDetailDTO> detailDtos = new List<InvoiceDetailDTO>();

            foreach (InvoiceDetail c in details)
            {

                detailDtos.Add(Map(c));
            }

            return detailDtos;
        }

        public decimal TotalPriceWithVat(InvoiceDetailDTO a)
        {
            var totalPrice = a.Pieces * a.Price;
            var totalpriceDiscount = (totalPrice / 100) * a.Discount;
            var totalPriceWithDiscount = totalPrice - totalpriceDiscount;
            var totalPriceVat = (totalPriceWithDiscount / 100) * a.Vat;
            var total = totalPriceWithDiscount + totalPriceVat;
            a.TotalPrice = total;
            return Math.Round(a.TotalPrice, 2);
        }
        public decimal TotalPriceWithoutVat(InvoiceDetailDTO a)
        {


            var totalPrice = a.Pieces * a.Price;
            var totalpriceDiscount = (totalPrice / 100) * a.Discount;
            var totalPriceWithDiscount = totalPrice - totalpriceDiscount;
            a.TotalPriceWithoutVat = totalPriceWithDiscount;
            return Math.Round(a.TotalPriceWithoutVat, 2);
        }

        public void Remove(InvoiceDetailDTO a)
        {
            _unitOfWork.InvoiceDetailRepository.Remove(Map(a));
            _unitOfWork.Save();
        }


    }
}
