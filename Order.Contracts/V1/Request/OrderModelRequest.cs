using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Contracts.V1.Request
{
    public class OrderModelRequest
    {
        public int ProductId { get; set; }
        public string? ProductCode { get; set; }
        public int OrderQty { get; set; }
    }
}
