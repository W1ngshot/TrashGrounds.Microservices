namespace TrashGrounds.User.Validation;

public static class ValidationMessages
{
    public const string EmptyUsername = "Username field cannot be empty";
    public const string TooShortUsername = "Username is too short";
    public const string TooLongUsername = "Username is too long";
    public const string UsernameContainsWrongSymbols = "Username contains imadmissible symbols";

    public const string EmptyEmail = "Email field cannot be empty";
    public const string IncorrectEmail = "Email is incorrect";
    public const string EmailAlreadyExists = "Email is already used";

    public const string EmptyPassword = "Password cannot be empty";
    public const string PasswordContainsWrongSymbols = "Password contains inadmissible symbols";
    public const string TooShortPassword = "Password is too short";
    public const string TooLongPassword = "Password is too long";
}