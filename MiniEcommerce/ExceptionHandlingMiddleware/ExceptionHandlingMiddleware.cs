using MiniEcommerce.BusinessLogicLayer.Exceptions.Category;
using MiniEcommerce.BusinessLogicLayer.Exceptions.Common;
using MiniEcommerce.BusinessLogicLayer.Exceptions.Product;
using System.Net;
using System.Text.Json;

namespace MiniEcommerce.ExceptionHandlingMiddleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode;
        string message = exception.Message;

        switch (exception)
        {
            case CategoryNotFoundException:
            case ProductNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                break;

            case CategoryAlreadyExistsException:
            case ProductAlreadyExistsException:
                statusCode = HttpStatusCode.Conflict;
                break;

            case CategoryNameEmptyException:
            case ProductNameEmptyException:
            case InvalidCategoryIdException:
            case InvalidProductPriceException:
            case InvalidPaginationException:
                statusCode = HttpStatusCode.BadRequest;
                break;

            case CategoryHasProductsException:
                statusCode = HttpStatusCode.UnprocessableEntity;
                break;

            case ProductCategoryNotFoundException:
                statusCode = HttpStatusCode.BadRequest;
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;
                message = "An unexpected error occurred.";
                break;
        }

        var response = new
        {
            error = message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
