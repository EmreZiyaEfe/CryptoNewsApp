using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Core.Entities
{
    public class ArticleCategory : BaseEntity
    {
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
