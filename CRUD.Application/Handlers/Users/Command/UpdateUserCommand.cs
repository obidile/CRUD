using CRUD.Application.Common.Interface;
using CRUD.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Craft.Application.Handlers.Users.Command;

public class UpdateUserCommand : IRequest<StatusResponse>
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, StatusResponse>
{
    private readonly IApplicationContext _dbContext;

    public UpdateUserCommandHandler(IApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StatusResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (user == null)
            {
                return StatusResponse.Failure("User was not found");
            }

            if (!new EmailAddressAttribute().IsValid(request.Email))
            {
                return StatusResponse.Failure("Invalid email format");
            }

            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
            if (existingUser != null)
            {
                return StatusResponse.Failure("Email address is already in use by another user.");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.LastModifiedDate = DateTime.UtcNow;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return StatusResponse.Success("User was successfully updated");

        }
        catch (Exception ex)
        {
            Log.Error(ex, "UpdateUserCommand");
            throw;
        }
    }
}
