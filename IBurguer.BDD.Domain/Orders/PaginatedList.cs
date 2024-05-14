using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBurguer.BDD.Model.Orders
{
    public record PaginatedList<T> where T : class
    {
        public int Total { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}
