using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new List<Product>();   
    }
}
