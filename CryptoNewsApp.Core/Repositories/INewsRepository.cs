using CryptoNewsApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Core.Repositories
{
    public interface INewsRepository : IRepository<News>
    {
        Task<IEnumerable<News>> GetLatestNewsAsync(int count);
        Task<IEnumerable<News>> GetNewsByCategoryAsync(int categoryId);
        Task<bool> AnyAsync(Expression<Func<News, bool>> predicate);
    }
}
