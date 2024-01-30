using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TddWebApi.Middlewares;
public class GlobalExceptionHandlerMiddleware : IMiddleware
{

    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try {
            await next(context);
        } 
        catch (Exception e){
            _logger.LogError(e, e.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            ProblemDetails problemDetails = new() {
                Status = StatusCodes.Status500InternalServerError,
                Type = "Error",
                Title = "Internal Server Error",
                Detail = $"Error message: {e.Message}"
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        
        }
    }
}