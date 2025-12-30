using MiniEcommerce.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Contracts.Interfaces;

public interface IUnitOfWork
{   
    IProductRepository Products { get; }
    IRepository<Category> Categories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
