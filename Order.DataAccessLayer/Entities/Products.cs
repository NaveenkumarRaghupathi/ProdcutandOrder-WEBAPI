using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DataAccessLayer.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int StockQty { get; set; }
        public string? Status { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime Modifieddate { get; set; }
    }
}
