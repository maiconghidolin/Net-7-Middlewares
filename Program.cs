using Extensions;

var builder = WebApplication.CreateBuilder(args);

{
    var host = builder.Host;

    var services = builder.Services;

    services.AddControllers();

    services.AddEndpointsApiExplorer();

    services.AddSwaggerGen();

}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

// can use like that
//app.UseMiddleware<ExceptionMiddleware>();

// or can use a extension method
app.UseExceptionMiddleware();

app.MapControllers();


app.Run();