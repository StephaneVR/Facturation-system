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
   public class ZipcodeLogic
   {
       private UnitOfWork _unitOfWork;

        public ZipcodeLogic()
        {
            _unitOfWork = new UnitOfWork();
        }
        public static Zipcode Map(ZipcodeDTO e)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<ZipcodeDTO, Zipcode>());
            var mapper = config.CreateMapper();
            mapper = new Mapper(config);
            Zipcode entity = mapper.Map<Zipcode>(e);
            return entity;

        }
        public static ZipcodeDTO Map(Zipcode e)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Zipcode, ZipcodeDTO>());
            var mapper = config.CreateMapper();
            mapper = new Mapper(config);
            ZipcodeDTO dto = mapper.Map<ZipcodeDTO>(e);
            return dto;

        }

        public void Add(ZipcodeDTO z)
        {
            _unitOfWork.ZipcodeRepository.Add(Map(z));
            _unitOfWork.Save();
            
        }

        public List<ZipcodeDTO> GetAll()
        {
            List<ZipcodeDTO> zipcodeDtos = new List<ZipcodeDTO>();
            List<Zipcode> zipcodes = _unitOfWork.ZipcodeRepository.GetAll();
            foreach (var z in zipcodes)
            {
                zipcodeDtos.Add(Map(z));
            }

            return zipcodeDtos;
        }
        public ZipcodeDTO FindById(int? id)
        {
            Zipcode zipcode = _unitOfWork.ZipcodeRepository.FinById(id);
            return Map(zipcode);
        }
        public void Modify(ZipcodeDTO i)
        {
            _unitOfWork.ZipcodeRepository.Modify(Map(i));
            _unitOfWork.Save();
            
        }
    }
}
