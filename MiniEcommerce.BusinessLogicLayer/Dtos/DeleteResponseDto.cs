using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Dtos;

public class DeleteResponseDto
{
	public bool Success { get; set; }	
	public required string Message { get; set; }
}
