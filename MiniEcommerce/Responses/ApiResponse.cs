namespace MiniEcommerce.Responses;

public class ApiResponse<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public string? Error { get; init; }

    private ApiResponse(bool success, T? data, string? error)
    {
        Success = success;
        Data = data;
        Error = error;
    }

    public static ApiResponse<T> Ok(T? data) => new(true, data, null);
    public static ApiResponse<T> Fail(string error) => new(false, default, error);
}
