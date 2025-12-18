using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
