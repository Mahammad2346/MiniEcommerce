using MiniEcommerce.BusinessLogicLayer.Entities;
using MiniEcommerce.DataAccessLayer.Context;
using MiniEcommerce.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MiniEcommerce.DataAccessLayer.Repositories;
public class ProductRepository(MiniEcommerceDbContext context) : Repository<Product>(context), IProductRepository {}