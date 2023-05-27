# .Net 7 Middlewares

Middlewares are blocks of code used to modify or intercept a request.
They are executed in a pipeline, where a middleware is responsible for calling the next one, until it reaches the endpoint. The return takes the same path backwards.
Therefore, middlewares can be used both to manipulate/abort the request and to change the response.

The image below shows how the request pipeline works:

![log](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/index/_static/request-delegate-pipeline.png?view=aspnetcore-7.0)

In this project, I show you how to build a custom exception handling middleware.
