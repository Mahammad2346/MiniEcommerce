using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.DataAccessLayer.Context
{
    public class MiniEcommerceDbContext : DbContext
    {
        public MiniEcommerceDbContext(DbContextOptions<MiniEcommerceDbContext>options) : base(options)
        {
                
        }
    }
}
