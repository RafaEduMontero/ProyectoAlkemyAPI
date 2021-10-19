using OngProject.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Helper.Pagination.Extensions
{
    public static class ServiceExtension
    {

        public static PaginationDTO<T> PaginationDTO<T>
            (this IService service, int page, List<T> items, int totalItems, int elementsByPage)
            where T : class
        {
            var dtoList = new PaginationDTO<T>
            {
                CurrentPage = page,
                Items = items,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / elementsByPage)
            };
            return dtoList;
        }
    }
}
