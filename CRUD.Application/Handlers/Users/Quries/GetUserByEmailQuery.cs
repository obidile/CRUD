using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRUD.Application.Common.Interface;
using CRUD.Application.Common.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ChiSquares.AuthService.Application.Handlers.Users.Queries;

public class GetUserByEmailQuery : IRequest<UserModel>
{
    public string Email { get; set; }
    public GetUserByEmailQuery(string email)
    {
        Email = email;
    }
}

public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
{
    public GetUserByEmailQueryValidator()
    {
        RuleFor(v => v.Email).NotEmpty().EmailAddress();
    }
}

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserModel>
{
    private readonly IApplicationContext _dbContext;
    private readonly IMapper _mapper;

    public GetUserByEmailQueryHandler(IApplicationContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<UserModel> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _dbContext.Users.AsNoTracking().ProjectTo<UserModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                throw new Exception("Email address was not found");
            }

            return user;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "GetUserByEmailQuery");
            throw;
        }
    }
}
