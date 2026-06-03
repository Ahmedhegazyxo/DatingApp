namespace Api.DTOs;

public class MatchDetailsDto
{
    public Guid MatchId { get; set; }
    public Guid AdjacentProfilePhotoId { get; set; }
    public string AdjacentFirstName { get; set; } = string.Empty;
    public string AdjacentLastName { get; set; } = string.Empty;
    public string? AdjacentBiography { get; set; } = string.Empty;
    public string? AdjacentEmail { get; set; } = string.Empty;
}