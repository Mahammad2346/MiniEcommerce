using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Product.Contracts;
public record CreateProductDto
(
    string Name,
    decimal Price,
    int CategoryId,
    string Description
);
