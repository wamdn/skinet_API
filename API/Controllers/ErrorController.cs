﻿using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("errors/{statusCode}")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : BaseApiController
{
    public IActionResult Error(int statusCode)
    {
        return new ObjectResult(new ApiErrorResponse(statusCode));
    }
}