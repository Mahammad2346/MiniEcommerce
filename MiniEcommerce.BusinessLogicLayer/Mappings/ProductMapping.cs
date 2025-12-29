using Mapster;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Mappings
{
    public class ProductMapping: IRegister
    {
        public void Register(TypeAdapterConfig config) 
        {
            config.NewConfig<Product, UpdateProductDto>();
            config.NewConfig<UpdateProductDto, Product>();    
        }  
    }
}
