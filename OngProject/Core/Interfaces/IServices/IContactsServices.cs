using OngProject.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces.IServices
{
    public interface IContactsServices
    {
        Task<IEnumerable<ContactsDTO>> GetAll();
        Task<ContactsDTO> GetById(int id);
        bool EntityExists(int id);
    }
}
