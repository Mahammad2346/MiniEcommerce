using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Dtos
{
    public class ProductDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public decimal Price { get; init; }
        public string Description { get; init; } = null!;
        public int CategoryId { get; init; }
    }
}
