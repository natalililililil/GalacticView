namespace Shared.DataTransferObjects
{
    public record PlanetDto
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? FullPlanetInfo { get; init; }
    }
}
