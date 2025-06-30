using CryptoNewsApp.Core.Repositories;
using CryptoNewsApp.Core.UnitOfWork;
using CryptoNewsApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, INewsRepository news, ICategoryRepository categories)
        {
            _context = context;
            News = news;
            Categories = categories;
        }

        public INewsRepository News {  get; private set; }
        public ICategoryRepository Categories {  get; private set; }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
