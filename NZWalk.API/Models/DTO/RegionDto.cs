namespace NZWalk.API.Models.DTO
{
    // This DTO will be subset of Domain Model regions 
    // Property that you want to expose from Region domain model add here.
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }

    }
}
