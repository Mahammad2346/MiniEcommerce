using MiniEcommerce.Product.API.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Product.API.Interfaces;

public interface IUnitOfWork
{   
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
