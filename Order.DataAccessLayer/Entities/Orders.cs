using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DataAccessLayer.Entities
{
    public class Orders
    {
        public int Id { get; set; }
        public string OrderCode { get; set; } = null!;
        public int ProductId { get; set; }
        public string? ProductCode { get; set; }
        public int OrderQty { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
