namespace TddWebApi.Services.Users.Exceptions;
public class GetUsersException: Exception
{
    public GetUsersException()
    {
        
    }

    public GetUsersException(string message): base(message)
    {
        
    }
}