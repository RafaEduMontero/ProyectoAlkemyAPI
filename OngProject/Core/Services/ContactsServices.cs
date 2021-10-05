using OngProject.Core.DTOs;
using OngProject.Core.Interfaces.IServices;
using OngProject.Core.Mapper;
using OngProject.Infrastructure.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Services
{
    public class ContactsServices : IContactsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ContactsDTO>> GetAll()
        {
            var mapper = new EntityMapper();
            var contactList = await _unitOfWork.ContactsRepository.GetAll();
            var contactDTOList = contactList.Select(x => mapper.FromContactsToContactsDto(x)).ToList();
            return contactDTOList;
        }

        public async Task<ContactsDTO> GetById(int id)
        {

            var mapper = new EntityMapper();
            var contact = await _unitOfWork.ContactsRepository.GetById(id);
            var contactDTO = mapper.FromContactsToContactsDto(contact);
            return contactDTO;
        }
        public bool EntityExists(int id)
        {
            return _unitOfWork.ContactsRepository.EntityExists(id);
        }

    }
}
