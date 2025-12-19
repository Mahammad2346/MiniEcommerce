using Microsoft.EntityFrameworkCore;
using MiniEcommerce.BusinessLogicLayer.Entities;
using MiniEcommerce.DataAccessLayer.Context;
using MiniEcommerce.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.DataAccessLayer.Repositories;
public class CategoryRepository(MiniEcommerceDbContext context) : Repository<Category>(context), ICategoryRepository {}
