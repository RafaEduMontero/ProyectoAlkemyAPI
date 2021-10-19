using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Helper.Pagination.Extensions
{
    public static class ControllerExtension
    {
        internal static PaginationDTO<T> AddPageLinks<T>
            (this ControllerBase controller, string route, int page, PaginationDTO<T> response)
            where T : class
        {
            if (response.TotalPages==0)
            {
                return response;
            }

            string urlBase = string.Concat(controller.Request.Scheme, "://", controller.Request.Host.ToUriComponent());
            if (response.CurrentPage > 1)
            {
                var prevRoute = controller.Url.RouteUrl(route, new { page = page - 1 });

                response.AddResourceLink(LinkedResourceType.Prev, urlBase + prevRoute);
            }

            if (response.CurrentPage < response.TotalPages)
            {
                var nextRoute = controller.Url.RouteUrl(route, new { page = page + 1 });

                response.AddResourceLink(LinkedResourceType.Next, urlBase + nextRoute);
            }

            return response;
        }

    }
}
