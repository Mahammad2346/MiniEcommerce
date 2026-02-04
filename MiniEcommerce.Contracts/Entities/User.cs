using MiniEcommerce.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Contracts.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
