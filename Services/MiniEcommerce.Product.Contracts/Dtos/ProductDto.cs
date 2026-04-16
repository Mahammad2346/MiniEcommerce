using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Product.Contracts;
public record ProductDto
(
    int Id,
    string Name,
    decimal Price,
    string Description,
    int CategoryId
);
