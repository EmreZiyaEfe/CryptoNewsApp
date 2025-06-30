using CryptoNewsApp.Core.Entities;
using CryptoNewsApp.Core.Repositories;
using CryptoNewsApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> GetCategoryWithNewsAsync(int categoryId)
        {
            return await _context.Categories
                .Include(c => c.News)
                .FirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}
