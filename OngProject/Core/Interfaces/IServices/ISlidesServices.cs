using OngProject.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces.IServices
{
    public interface ISlidesServices
    {
        Task<IEnumerable<SlidesDTO>> GetAll();
    }
}
