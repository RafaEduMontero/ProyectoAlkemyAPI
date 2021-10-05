using OngProject.Core.Entities;
using System.Threading.Tasks;

namespace OngProject.Infrastructure.Repositories.IRepository
{
    public interface IUnitOfWork 
    {
        IBaseRepository<Slides> SlidesRepository { get; }
<<<<<<< HEAD
        IBaseRepository<Category> CategoryRepository {get; }
        
=======
        IBaseRepository<Contacts> ContactsRepository { get; }

>>>>>>> 3d5f331aadcca54d3d11ab9b2282e6df3096e5be
        void Dispose();

        void SaveChanges();

        Task SaveChangesAsync();

    }
}
