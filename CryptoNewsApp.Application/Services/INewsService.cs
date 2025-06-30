using CryptoNewsApp.Application.Dtos;
using CryptoNewsApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Application.Services
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetLatestNewsAsync(int count);
        Task<IEnumerable<News>> GetNewsByCategoryAsync(int categoryId);
        Task<News> GetNewsByIdAsync(int id);
        Task CreateNewsAsync(News news);
        Task UpdateNewsAsync(News news);
        Task DeleteNewsAsync(int id);

        // API'den haber çeken metodu da ekliyoruz
        Task<IEnumerable<NewsDto>> FetchNewsFromApiAsync(int count);

        Task<int> SaveNewsFromApiAsync(int count);

    }
}
