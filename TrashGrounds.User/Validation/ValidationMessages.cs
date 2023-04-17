namespace TrashGrounds.User.Validation;

public static class ValidationMessages
{
    public const string EmptyNickname = "Nickname field cannot be empty";
    public const string TooShortNickname = "Nickname is too short";
    public const string TooLongNickname = "Nickname is too long";
    public const string NicknameContainsWrongSymbols = "Nickname contains wrong symbols";
    public const string NicknameAlreadyExists = "Nickname is already used";

    public const string EmptyEmail = "Email field cannot be empty";
    public const string IncorrectEmail = "Email is incorrect";
    public const string EmailAlreadyExists = "Email is already used";

    public const string EmptyPassword = "Password cannot be empty";
    public const string PasswordContainsWrongSymbols = "Password contains wrong symbols";
    public const string TooShortPassword = "Password is too short";
    public const string TooLongPassword = "Password is too long";
    public const string PasswordRequiresLowercase = "Password requires lowercase character";
    public const string PasswordRequiresUppercase = "Password requires uppercase character";
    public const string PasswordRequiresDigit = "Password requires digit";
    public const string PasswordRequiresNonAlphanumeric = "Password requires non alphanumeric character";

    public const string EmptyNewPassword = "New password cannot be empty";

    public const string EmptyNewStatus = "New status cannot be empty";
    public const string TooShortStatus = "New status is too short";
    public const string TooLongStatus = "New status is too long";
}