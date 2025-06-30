using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Infrastructure.ExternalServices.NewsData
{
    public class NewsDataApiRepsonse
    {
        public string Status { get; set; }
        public List<NewsDataResult> Results { get; set; }
    }
}
