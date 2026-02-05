using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Dtos.Auth
{
    public record LoginRequest
    (
        string Email,
        string Password
    );
}
