using CRUD.Application;
using CRUD.Application.Common.Interface;
using CRUD.Persistence.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register application layer services
builder.Services.AddApplicationLayer();

// Register infrastructure layer services
//builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("CRUDDb"));
builder.Services.AddScoped<IApplicationContext, ApplicationContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
