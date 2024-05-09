using CRUD.Domain.Common;
using CRUD.Domain.Enums;

namespace CRUD.Domain.Entities;

public class User : BaseAuditableEntity
{
    public string UniqueID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public AccountType AccountType { get; set; }
    public string ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public string VerificationCode { get; set; }
    public bool IsVerified { get; set; }
    public bool Deactivated { get; set; }
    public string Password { get; set; }
}
public class UserMiniModel
{
    public string UniqueID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public AccountType AccountType { get; set; }
    public bool IsActive { get; set; }
}