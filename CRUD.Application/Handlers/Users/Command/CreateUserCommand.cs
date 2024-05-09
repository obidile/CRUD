using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Serilog;
using CRUD.Application.Common.Models;
using CRUD.Application.Common.Interface;
using CRUD.Domain.Entities;
using CRUD.Domain.Enums;

namespace Craft.Application.Handlers.Users.Command;

public class CreateUserCommand : IRequest<StatusResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, StatusResponse>
{

    private readonly IApplicationContext _dbContext;
    public CreateUserCommandHandler(IApplicationContext dbContext)//, IEmailService emailService)
    {
        _dbContext = dbContext;
        //_emailService = emailService;
    }

    public async Task<StatusResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!new EmailAddressAttribute().IsValid(request.Email))
            {
                return StatusResponse.Failure("Invalid email format");
            }
            if (request.Password != request.ConfirmPassword)
            {
                return StatusResponse.Failure("Your Password doesn't match");
            }

            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.ToLower() == request.Email.ToLower(), cancellationToken);
            if (user != null)
            {
                return StatusResponse.Failure("This User already Exist");
            }


            var model = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                AccountType = AccountType.Individual,
                IsActive = true,
                Deactivated = false,
                Password = request.Password,
                CreatedDate = DateTime.UtcNow,
            };

            _dbContext.Users.Add(model);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return StatusResponse.Success("User successfully created");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "CreateUserCommand");
            throw;
        }        
    }


}