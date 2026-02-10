using MiniEcommerce.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Contracts.Interfaces
{
    public interface ITokenService
    {
        string Generate(User user);
    }
}
