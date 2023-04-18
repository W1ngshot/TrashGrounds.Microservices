using TrashGrounds.Track.Models.Main.Abstractions;

namespace TrashGrounds.Track.Models.Main;

public class Genre : BaseEntity
{
    public required string Name { get; set; }
    
    public List<Track> Tracks { get; set; }
}