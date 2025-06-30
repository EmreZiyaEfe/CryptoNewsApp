using CryptoNewsApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Infrastructure.Services
{
    public class PasswordGenerator : IPasswordGenerator
    {
        private const string ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
        private readonly Random _random = new();
        public string GeneratePassword(int length = 8)
        {
            var res = new char[length];
            for (int i = 0; i < length; i++)
            {
                res[i] = ValidChars[_random.Next(ValidChars.Length)];
            }
            return new string(res);
        }
    }
}
