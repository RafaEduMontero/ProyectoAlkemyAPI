using OngProject.Core.Entities;
using OngProject.Infrastructure.Data;
using OngProject.Infrastructure.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly IBaseRepository<Organizations> _organizationsRepository;
        private readonly IBaseRepository<Testimonials> _testimonialsRepository;
        public IBaseRepository<Organizations> NewsRepository => _organizationsRepository ?? new BaseRepository<Organizations>(_context);
        public IBaseRepository<Testimonials> TestimonialsRepository => _testimonialsRepository ?? new BaseRepository<Testimonials>(_context);
        private readonly IBaseRepository<Member> _memberRepository;
        public IBaseRepository<Member> NewsRpository => _memberRepository ?? new BaseRepository<Member>(_context);
        private readonly IBaseRepository<Activities> _ActivitiesRepository;

        private readonly IBaseRepository<User> _userRepository;

        public IBaseRepository<Activities> ActivitiesRepository => _ActivitiesRepository ?? new BaseRepository<Activities>(_context);
        public IBaseRepository<User> UserRpository => _userRepository ?? new BaseRepository<User>(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
