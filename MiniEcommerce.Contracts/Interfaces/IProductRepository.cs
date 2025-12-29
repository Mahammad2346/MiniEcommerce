using MiniEcommerce.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Contracts.Interfaces;

public interface IProductRepository: IRepository<Product>
{
    Task<IReadOnlyCollection<Product>> GetProductsAsync(int pageNumber, int pageSize, int? categoryId, CancellationToken cancellationToken);

}
