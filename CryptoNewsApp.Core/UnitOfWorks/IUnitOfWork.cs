using CryptoNewsApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        INewsRepository News {  get; }
        ICategoryRepository Categories { get; }
        Task<int> CompleteAsync();
    }
}
