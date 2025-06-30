using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoNewsApp.Infrastructure.ExternalServices.CryptoPanic.Models
{
    public class CryptoPanicResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<CryptoPanicPost> Results { get; set; }
    }
}
