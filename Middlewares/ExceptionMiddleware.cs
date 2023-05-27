using Models;
using System.Net;

namespace Middlewares;

public class ExceptionMiddleware
{

    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
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

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = GetStatusCode(ex);

        var modelErros = new List<ExceptionModel>();

        GetErrorMessage(modelErros, ex);

        var result = System.Text.Json.JsonSerializer.Serialize(modelErros);
        return response.WriteAsync(result);
    }

    private static int GetStatusCode(Exception ex)
    {
        if (ex is UnauthorizedAccessException)
            return (int)HttpStatusCode.Unauthorized;

        else if (ex is ApplicationException)
            return (int)HttpStatusCode.BadRequest;

        else if (ex is KeyNotFoundException)
            return (int)HttpStatusCode.NotFound;

        return (int)HttpStatusCode.InternalServerError;
    }

    private static void GetErrorMessage(List<ExceptionModel> modelErros, Exception ex)
    {
        modelErros.Add(new ExceptionModel(ex.Message));

        if (ex.InnerException != null)
            GetErrorMessage(modelErros, ex.InnerException);

    }

}

