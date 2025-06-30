using CryptoNewsApp.Application.Dtos;
using CryptoNewsApp.Application.Interfaces;
using CryptoNewsApp.Core.Entities;
using CryptoNewsApp.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Application.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewsApiClient _newsApiClient;

        public NewsService(IUnitOfWork unitOfWork, INewsApiClient newsApiClient)
        {
            _unitOfWork = unitOfWork;
            _newsApiClient = newsApiClient;
        }

        public async Task CreateNewsAsync(News news)
        {
            await _unitOfWork.News.AddAsync(news);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteNewsAsync(int id)
        {
            var news = await _unitOfWork.News.GetByIdAsync(id);
            if (news != null)
            {
                _unitOfWork.News.Remove(news);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<IEnumerable<News>> GetLatestNewsAsync(int count)
        {
            return await _unitOfWork.News.GetLatestNewsAsync(count);
        }

        public async Task<IEnumerable<News>> GetNewsByCategoryAsync(int categoryId)
        {
            return await _unitOfWork.News.GetNewsByCategoryAsync(categoryId);
        }

        public async Task<News> GetNewsByIdAsync(int id)
        {
            return await _unitOfWork.News.GetByIdAsync(id);
        }

        public async Task UpdateNewsAsync(News news)
        {
            _unitOfWork.News.Update(news);
            await _unitOfWork.CompleteAsync();
        }

        //Apiden haber çekme metodu yaz
        public async Task<IEnumerable<NewsDto>> FetchNewsFromApiAsync(int count)
        {
            return await _newsApiClient.GetLatestNewsAsync(count);
        }

        //DTOyu news modeline döüştürüyoruz
        private News ConvertToNewsEntity(NewsDto dto)
        {
            return new News
            {
                //Id = dto.Id, // Eğer api id veriyorsa, yoksa bu satırı kaldır
                Title = dto.Title,
                Content = dto.Content,
                SourceUrl = dto.Url, // dto.Url → entity.SourceUrl
                PublishDate = dto.PublishDate ?? DateTime.Now, // PublishDate null ise şimdiki zamanı atıyoruz
                ImageUrl = dto.ImageUrl,
                IsPublished = true,

                // Kategori api’den string olarak gelmiş, entity int CategoryId ister
                // Burada kategoriyi eşlemek için ayrıca bir mapping mantığı yazmalısın
                CategoryId = 0, // ya da default kategori id'si koy
                Category = null                   // İstersen daha sonra yüklenebilir
            };
        }

        public async Task<int> SaveNewsFromApiAsync(int count)
        {
            var dtos = await _newsApiClient.GetLatestNewsAsync(count);
            var newsEntities = dtos.Select(dto => ConvertToNewsEntity(dto)).ToList();
            int addedCount = 0;

            foreach (var news in newsEntities)
            {
                // Basit kontrol: Aynı başlık ve yayın tarihi varsa atla
                var exists = await _unitOfWork.News.AnyAsync(n => n.Title == news.Title && n.PublishDate == news.PublishDate);
                if (!exists)
                {
                    await _unitOfWork.News.AddAsync(news);
                    addedCount++;
                }
            }
            if (addedCount > 0)
                await _unitOfWork.CompleteAsync();
            
            return addedCount;
        }
    }
}
