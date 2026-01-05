using MiniEcommerce.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Contracts.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
