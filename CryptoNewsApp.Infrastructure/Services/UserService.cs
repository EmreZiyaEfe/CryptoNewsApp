using CryptoNewsApp.Application.Dtos;
using CryptoNewsApp.Application.Interfaces;
using CryptoNewsApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CryptoNewsApp.Application.Dtos;
    using CryptoNewsApp.Application.Interfaces;
    using CryptoNewsApp.Core.Entities;
    using Microsoft.AspNetCore.Identity;

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordGenerator _passwordGenerator;

        public UserService(UserManager<ApplicationUser> userManager, IPasswordGenerator passwordGenerator)
        {
            _userManager = userManager;
            _passwordGenerator = passwordGenerator;
        }

        private async Task<CreateUserResult> CreateUserInternalAsync(AddUserDto dto, string password)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,
                CreatedAt = DateTime.Now,
                Status = dto.Status
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return new CreateUserResult
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            if (!string.IsNullOrEmpty(dto.Role))
            {
                await _userManager.AddToRoleAsync(user, dto.Role);
            }

            return new CreateUserResult
            {
                Success = true,
                Errors = Enumerable.Empty<string>(),
                Password = password
            };
        }

        // 🔹 Register dışındaki kullanım (şifreyi biz üretiyoruz)
        public async Task<CreateUserResult> CreateUserAsync(AddUserDto dto)
        {
            Console.WriteLine($"CreateUserAsync tetiklendi. Email: {dto.Email}");
            //var password = _passwordGenerator.GeneratePassword(); // Şifreyi sistem üretir
            var password = "Abc1234+";
            return await CreateUserInternalAsync(dto, password);
        }

        // 🔹 Register kullanımı (şifre kullanıcıdan geliyor)
        public async Task<CreateUserResult> CreateUserAsync(AddUserDto dto, string password)
        {
            return await CreateUserInternalAsync(dto, password);
        }
    }

}
