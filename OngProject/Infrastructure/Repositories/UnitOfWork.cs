﻿using OngProject.Core.Entities;
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
        private readonly IBaseRepository<Member> _memberRepository;

        public IBaseRepository<Organizations> NewsRepository => _organizationsRepository ?? new BaseRepository<Organizations>(_context);
        public IBaseRepository<Member> NewsRpository => _memberRepository ?? new BaseRepository<Member>(_context);


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