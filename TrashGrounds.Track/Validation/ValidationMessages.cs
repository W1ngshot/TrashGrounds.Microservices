namespace TrashGrounds.Track.Validation;

public static class ValidationMessages
{
    public const string EmptyTitle = "Title field cannot be empty";
    public const string TooShortTitle = "Title is too short";
    public const string TooLongTitle = "Title is too long";
    public const string TitleContainsWrongSymbols = "Title contains wrong symbols";

    public const string TooShortDescription = "Description is too short";
    public const string TooLongDescription = "Description is too long";
    public const string DescriptionContainsWrongSymbols = "Description contains wrong symbols";

    public const string EmptyGenresList = "Genres cannot be empty";
}