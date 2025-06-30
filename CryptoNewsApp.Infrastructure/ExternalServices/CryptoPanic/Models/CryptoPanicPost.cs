using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Infrastructure.ExternalServices.CryptoPanic.Models
{
    public class CryptoPanicPost
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime PublishedAt { get; set; }
        public string? Domain { get; set; }
        public CryptoPanicSource Source { get; set; } // API'den gelen source objesi
        public string? Thumbnail { get; set; }
    }
}
