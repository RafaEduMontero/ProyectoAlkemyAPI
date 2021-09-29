using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Infrastructure.Repositories.IRepository
{
    public interface IUnitOfWork
    {
        void Dispose();

        void SaveChanges();

        Task SaveChangesAsync();

    }
}
