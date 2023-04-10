
using TrashGrounds.User.Constants;

namespace TrashGrounds.User.Exceptions;

public class AlreadyExistsException : ConflictException
{
    public AlreadyExistsException(string entityName) : base(ErrorCodes.AlreadyExistsError)
    {
        PlaceholderData.Add("EntityName", entityName);
    }
}