using OngProject.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces.IServices
{
    public interface IActivitiesServices
    {
        Task<IEnumerable<ActivitiesDTO>> GetAll();

        Task<ActivitiesDTO> GetById(int id);

        bool EntityExists(int id);

        Task<ActivitiesDTO> Insert(ActivitiesDTO contactDTO);
    }
}
