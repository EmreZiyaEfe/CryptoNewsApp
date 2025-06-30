using CryptoNewsApp.Core.Entities;
using CryptoNewsApp.Infrastructure.Data;
using CryptoNewsApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Core.Repositories
{
    public class NewsRepository : Repository<News>, INewsRepository
    {

        private readonly ApplicationDbContext _context;
        //private readonly DbSet<T> _dbSet;


        public NewsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetLatestNewsAsync(int count)
        {
            return await _context.News
                .Include(n => n.Category)
                .Where(n => n.IsPublished)
                .OrderByDescending(n => n.PublishDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> GetNewsByCategoryAsync(int categoryId)
        {
            return await _context.News
                .Include(n => n.Category)
                .Where(n => n.CategoryId == categoryId && n.IsPublished)
                .OrderByDescending(n => n.PublishDate)
                .ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<News, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}
