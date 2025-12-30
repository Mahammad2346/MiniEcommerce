using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Dtos;

public record ProductDto
(
    int Id,
    string Name,
    decimal Price,
    string Description,
    int CategoryId
);
