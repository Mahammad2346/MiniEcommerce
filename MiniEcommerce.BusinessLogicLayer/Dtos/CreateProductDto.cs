using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Dtos;

public record CreateProductDto
(
    string Name,
    decimal Price,
    int CategoryId,
    string Description
);
