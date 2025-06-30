using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Descritpion { get; set; }
        public string Slug { get; set; }
        public virtual ICollection<News> News { get; set; } = new List<News>();
    }
}
