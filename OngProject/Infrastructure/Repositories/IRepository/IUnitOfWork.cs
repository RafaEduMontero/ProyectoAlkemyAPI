using OngProject.Core.Entities;
using System.Threading.Tasks;

namespace OngProject.Infrastructure.Repositories.IRepository
{
    public interface IUnitOfWork 
    {
        IBaseRepository<Slides> SlidesRepository { get; }
        IBaseRepository<Category> CategoryRepository {get; }       

        IBaseRepository<Contacts> ContactsRepository { get; }

        void Dispose();

        void SaveChanges();

        Task SaveChangesAsync();

    }
}
