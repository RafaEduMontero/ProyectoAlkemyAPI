using OngProject.Core.Models;
using System.Threading.Tasks;

namespace OngProject.Infrastructure.Repositories.IRepository
{
    public interface IUnitOfWork 
    {
        IBaseRepository<SlidesModel> SlidesRepository { get; }
        void Dispose();

        void SaveChanges();

        Task SaveChangesAsync();

    }
}
