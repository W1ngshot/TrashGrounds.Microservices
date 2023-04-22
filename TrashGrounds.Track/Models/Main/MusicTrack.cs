using TrashGrounds.Track.Models.Main.Abstractions;

namespace TrashGrounds.Track.Models.Main;

public class MusicTrack : BaseEntity
{
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    public bool IsExplicit { get; set; }
    
    public DateTime UploadDate { get; set; }
    
    public int ListensCount { get; set; }
    
    public string? PictureLink { get; set; }
    
    public required string MusicLink { get; set; }
    
    public required Guid UserId { get; set; }
    
    public List<Genre> Genres { get; set; }
}