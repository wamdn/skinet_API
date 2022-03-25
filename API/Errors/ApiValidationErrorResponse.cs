namespace API.Errors;

public class ApiValidationErrorResponse : ApiErrorResponse
{
    public IEnumerable<string> Errors { get; init; } = Array.Empty<string>();

    public ApiValidationErrorResponse(int statusCode, string? message = null) : base(statusCode, message)
    {
    }
}