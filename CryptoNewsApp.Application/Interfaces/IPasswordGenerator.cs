using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Application.Interfaces
{
    public interface IPasswordGenerator
    {
        string GeneratePassword(int length = 8);
    }
}
