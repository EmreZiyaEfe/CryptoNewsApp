using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Application.Dtos
{
    public class CreateUserResult
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
        public string Password { get; set; }  // Admin paneli için
    }
}
