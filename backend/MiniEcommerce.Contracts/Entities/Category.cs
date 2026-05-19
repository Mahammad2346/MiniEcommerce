using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Contracts.Entities;
public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
