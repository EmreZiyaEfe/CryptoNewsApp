using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoNewsApp.Application.Dtos;

namespace CryptoNewsApp.Application.Interfaces
{
    public interface IUserService
    {
        //Admin paneli için 
        Task<CreateUserResult> CreateUserAsync(AddUserDto dto);

        // Kullanıcı kayıt için (şifre parametreli)
        Task<CreateUserResult> CreateUserAsync(AddUserDto dto, string password);

    }
}
