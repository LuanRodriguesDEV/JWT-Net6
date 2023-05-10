using api.MIddleware;
using api.Programs;
using BackEnd_Clinica.MIddleware;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInjections();
builder.Services.AddSwaggerGen();
builder.Services.AddCorsPolicy();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.DbConnectionSetup(builder.Configuration);
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "React";
});
var app = builder.Build();
app.UseSpaStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseMiddleware(typeof(ErrorHandleMiddleware));
app.UseMiddleware(typeof(AuthenticatorMiddleware));
app.UseAuthorization();

app.UseCors("CorsPolicy");
app.MapControllers();



app.Run();
