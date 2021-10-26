using Microsoft.Extensions.Configuration;
using OngProject.Common;
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
        #region Objects and Constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        public ContactsServices(IUnitOfWork unitOfWork, IMailService mailService)
        {
            this._unitOfWork = unitOfWork;
            this._mailService = mailService;
        } 
        #endregion
        public async Task<IEnumerable<ContactDTO>> GetAll()
        {
            var mapper = new EntityMapper();
            var contactList = await _unitOfWork.ContactsRepository.GetAll();
            var contactDTOList = contactList.Select(x => mapper.FromContactsToContactsDto(x)).ToList();
            return contactDTOList;
        }
        public async Task<ContactDTO> GetById(int id)
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
        public async Task<Result> Insert(ContactInsertDTO contactInsertDTO)
        {
            var mapper = new EntityMapper();
                
            var newContact= mapper.FromContactsDtoToContacts(contactInsertDTO);
            await _unitOfWork.ContactsRepository.Insert(newContact);

            await _unitOfWork.SaveChangesAsync();

            string body = $"{newContact.Name}, Gracias por el contacto! En breve nos estaremos comunicando con vos";
            await _mailService.SendEmailAsync(newContact.Email, body, $"Contacto OnG para {newContact.Name}");

            return new Result().Success("El contacto fue creado correctamente");
        }
    }
}
