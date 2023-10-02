namespace DapperLearning.Models;

public class Race
{
    public Guid Id { get; set; }
    public string? RaceName { get; set; }
    public Guid FactionId { get; set; }
    public string? Description { get; set; }
    public string? Leader { get; set; }
    public string? SpecialAbilities { get; set; }
    public Faction? Faction { get; set; }
}