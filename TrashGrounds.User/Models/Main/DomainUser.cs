using TrashGrounds.User.Models.Main.Abstractions;

namespace TrashGrounds.User.Models.Main;

public class DomainUser : BaseEntity
{
    public required string Nickname { get; set; }
    
    public DateTime RegistrationDate { get; set; }
    
    public string? AvatarLink { get; set; }
    
    public string? Description { get; set; }

    public required Guid IdentityUserId { get; set; }

}


