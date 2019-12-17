using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
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
    public class ClientLogic
    {
        private UnitOfWork _unitOfWork;

        public ClientLogic()
        {
           _unitOfWork = new UnitOfWork();
        }
        public static Client Map(ClientDTO e)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, Client>());
            var mapper = config.CreateMapper();
            mapper = new Mapper(config);
            Client entity = mapper.Map<Client>(e);
            entity.ZipcodeId = e.ZipcodeDTOId;
            entity.Zipcode = unitOfWork.ZipcodeRepository.FinById(entity.ZipcodeId);
            return entity;

        }
        public static ClientDTO Map(Client e)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>());
            var mapper = config.CreateMapper();
            mapper = new Mapper(config);
            ClientDTO dto = mapper.Map<ClientDTO>(e);
            dto.ZipcodeDTOId = e.ZipcodeId;
            ZipcodeLogic logic = new ZipcodeLogic();
            dto.ZipcodeDto = logic.FindById(dto.ZipcodeDTOId);
            return dto;

        }

        public void Add(ClientDTO c)
        {

            _unitOfWork.ClientRepo.Add(Map(c));
            _unitOfWork.Save();
            

        }

        public void Modify(ClientDTO c)
        {
            _unitOfWork.ClientRepo.Modify(Map(c));
            _unitOfWork.Save();
           
        }

        public ClientDTO FinById(int? id)
        {
            Client c = _unitOfWork.ClientRepo.FinById(id);
            return Map(c);
        }

        public List<ClientDTO> GetActiveClients()
        {
            List<Client> clients = _unitOfWork.ClientRepo.GetAll().Where(c => c.Deleted == false).ToList();
            List<ClientDTO> clientDtos  = new List<ClientDTO>();

            foreach (Client c in clients)
            {
                
                clientDtos.Add(Map(c));
            }

            return clientDtos;
        }
        



    }
}
