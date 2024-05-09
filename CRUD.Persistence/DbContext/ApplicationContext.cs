using CRUD.Application.Common.Interface;
using CRUD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Persistence.DbContext;

public class ApplicationContext : Microsoft.EntityFrameworkCore.DbContext, IApplicationContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
    { }

    public DbSet<User> Users { get; set; }
}