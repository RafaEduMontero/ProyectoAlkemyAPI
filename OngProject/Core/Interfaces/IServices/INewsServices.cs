using Microsoft.AspNetCore.Mvc;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using OngProject.Core.Helper.Pagination;
using System;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces.IServices
{
    public interface INewsServices
    {
        Task<NewsDTO> GetById(int id);

        bool EntityExists(int id);
        Task<News> Insert(NewsDTO newsDTO);

        Task<Result> Delete(int id);
        Task<Result> Update(int id, NewsUpdateDTO newsUpdateDTO);
        Task<PaginationDTO<NewsDTO>> GetByPage(string route, int page);
    }
}
