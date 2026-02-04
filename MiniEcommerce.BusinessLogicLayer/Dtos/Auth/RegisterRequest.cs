using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Dtos.Auth
{
    public record RegisterRequest
    (
        string Email,
        string Password
    );
    
}
