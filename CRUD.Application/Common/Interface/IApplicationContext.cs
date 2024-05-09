using CRUD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Application.Common.Interface;

public interface IApplicationContext
{
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
