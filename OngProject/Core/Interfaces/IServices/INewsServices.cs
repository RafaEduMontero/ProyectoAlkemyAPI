using OngProject.Core.DTOs;
using System;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces.IServices
{
    public interface INewsServices
    {
        Task<NewsDTO> GetNewsForId(int id);
    }
}
