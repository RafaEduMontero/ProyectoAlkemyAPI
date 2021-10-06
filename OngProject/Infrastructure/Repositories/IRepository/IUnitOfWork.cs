using OngProject.Core.Entities;
using System.Threading.Tasks;

namespace OngProject.Infrastructure.Repositories.IRepository
{
    public interface IUnitOfWork 
    {
        IBaseRepository<Slides> SlidesRepository { get; }
        IBaseRepository<Category> CategoryRepository {get; }       
        IBaseRepository<Contacts> ContactsRepository { get; }
<<<<<<< HEAD
        IBaseRepository<Member> MemberRepository { get; }
=======
        IBaseRepository<Comments> CommentsRepository { get; }
        IBaseRepository<Organizations> OrganizationsRepository { get; }
>>>>>>> d70b3fa3c1e78ce2b9e902cdaf2fd32ea2ee956e

        void Dispose();

        void SaveChanges();

        Task SaveChangesAsync();

    }
}
