namespace API.Errors;

public class ApiErrorResponse
{
    public int StatusCode { get; }
    public string? Message { get; }

    public ApiErrorResponse(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultErrorMessage();
    }

    private string? GetDefaultErrorMessage() => StatusCode switch
    {
        400 => "A bad request, you have made",
        401 => "Authorized, you are not",
        404 => "Resource found, it was not",
        500 => "Error are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change",
        _ => null
    };
}