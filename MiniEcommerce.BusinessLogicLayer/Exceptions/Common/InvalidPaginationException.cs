using System.Net;

namespace MiniEcommerce.BusinessLogicLayer.Exceptions.Common;

public sealed class InvalidPaginationException : AppException
{
    public InvalidPaginationException(int pageNumber, int pageSize)
        : base($"Invalid pagination values. PageNumber: {pageNumber}, PageSize: {pageSize}. Both must be greater than zero.", HttpStatusCode.BadRequest) {}
}
