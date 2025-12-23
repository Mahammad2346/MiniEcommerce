using Mapster;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Mappings
{
    public class CategoryMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Category, CategoryDto>();
            config.NewConfig<CategoryDto, Category>();
        }
    }
}
