using CRUD.Application.Common.Interface;
using CRUD.Application.Common.Models;
using MediatR;
using Serilog;

namespace Craft.Application.Handlers.Users.Command;

public class DeleteUserCommand : IRequest<StatusResponse>
{
    public long Id { get; set; }
}
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, StatusResponse>
{
    private readonly IApplicationContext _dbContext;

    public DeleteUserCommandHandler(IApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StatusResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _dbContext.Users.FindAsync(request.Id);
            if (user == null)
            {
                return StatusResponse.Failure("User was not found");
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return StatusResponse.Success("User deleted Successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "DeleteUserCommand");
            throw;
        }
    }
}