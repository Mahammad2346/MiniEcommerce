using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public DateTime CretedAt { get; set;  }

        public decimal TotalPrice { get; set; }
 

    }
}
