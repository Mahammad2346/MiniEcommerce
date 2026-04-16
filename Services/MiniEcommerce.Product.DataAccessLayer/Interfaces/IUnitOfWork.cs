using MiniEcommerce.Product.DataAccessLayer.Interfaces;
using MiniEcommerce.Product.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Product.DataAccessLayer.Interfaces;
public interface IUnitOfWork
{   
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
