using Myhotel.Services;
using MyhotelApi.Extensions;
using MyhotelApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

if (((IApplicationBuilder)app).ApplicationServices.GetService<IHttpContextAccessor>() != null)
    HttpContextHelper.Accessor =
        ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IHttpContextAccessor>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseExceptionHandlerMiddleware();
app.UseRequestCultureMiddleware();

/*app.UseAuthentication();
app.UseAuthorization();*/

app.MapControllers();

app.Run();