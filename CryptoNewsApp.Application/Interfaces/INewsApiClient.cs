using CryptoNewsApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Application.Interfaces
{
    public interface INewsApiClient
    {
        Task<IEnumerable<NewsDto>> GetLatestNewsAsync(int count);
        //Task<IEnumerable<NewsDto>> FetchLatestNewsAsync();
    }
}
