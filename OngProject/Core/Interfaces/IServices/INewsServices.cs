using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using System;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces.IServices
{
    public interface INewsServices
    {
        Task<NewsDTO> GetById(int id);

        bool EntityExists(int id);
        Task<News> Insert(NewsDTO newsDTO);
    }
}
