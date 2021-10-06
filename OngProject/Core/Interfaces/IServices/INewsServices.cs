using OngProject.Core.DTOs;
using System;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces.IServices
{
    public interface INewsServices
    {
        Task<NewsDTO> GetForId(int id);

        bool EntityExists(int id);
    }
}
