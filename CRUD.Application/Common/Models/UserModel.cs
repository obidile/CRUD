using CRUD.Application.Common.Enums;
using CRUD.Application.Common.Mappers;
using CRUD.Domain.Entities;

namespace CRUD.Application.Common.Models;

public class UserModel : BaseModel, IMapFrom<User>
{
    public string UniqueID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public AccountTypeEnum AccountTypeEnum { get; set; }
    public string ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public string VerificationCode { get; set; }
    public bool IsVerified { get; set; }
    public bool Deactivated { get; set; }
    public string PasswordHush { get; set; }
}

public class UserMiniModel : IMapFrom<User>
{
    public string UniqueID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public AccountTypeEnum AccountTypeEnum { get; set; }
    public string ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public bool Deactivated { get; set; }
}