using Microsoft.AspNetCore.Identity;
using MiniEcommerce.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.Contracts.Entities;

public class User: IdentityUser<Guid>
{
	public UserRole Role { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
