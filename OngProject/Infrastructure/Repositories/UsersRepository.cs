using Microsoft.EntityFrameworkCore;
using OngProject.Core.Entities;
using OngProject.Infrastructure.Data;
using OngProject.Infrastructure.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Infrastructure.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository(ApplicationDbContext _context) : base(_context) { }

        public async Task<User> GetByEmail(string email)
        {
            IQueryable<User> query = _context.Users.Include(u => u.Role);
            var user = await query.Where(x => x.Email.ToUpper() == email.ToUpper() && x.IsDeleted == false).FirstOrDefaultAsync();
            return user;
        }
    }
}
