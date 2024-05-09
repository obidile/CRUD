namespace CRUD.Application.Common.Models;

public class BaseModel
{
    public long Id { get; set; }
    public DateTime CreatedDate { get; set; }
}

public abstract class BaseAuditableModel : BaseModel
{
    public string CreatedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public string LastModifiedBy { get; set; }
}


public class StatusResponse
{
    public bool Status { get; set; }
    public string Message { get; set; }
    public int Code { get; set; }

    internal StatusResponse(bool succeeded, string message, int code)
    {
        Status = succeeded;
        Message = message;
        Code = code;
    }
    public static StatusResponse Success(string message = "Request was Successful") =>
        new StatusResponse(succeeded: true, message, 200);

    public static StatusResponse Failure(string message = "Request Failed", int code = 400) =>
        new StatusResponse(succeeded: false, message, code);
}
