
using TrashGrounds.User.Infrastructure.Constants;

namespace TrashGrounds.User.Infrastructure.Exceptions;

public class AlreadyExistsException : ConflictException
{
    public AlreadyExistsException(string entityName) : base(ErrorCodes.AlreadyExistsError)
    {
        PlaceholderData.Add("EntityName", entityName);
    }
}