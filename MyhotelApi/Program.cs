using MyhotelApi.Extensions;
using MyhotelApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseExceptionHandlerMiddleware();
app.UseRequestCultureMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();