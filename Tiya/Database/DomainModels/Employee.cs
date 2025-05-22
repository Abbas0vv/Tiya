namespace Tiya.Database.DomainModels;

public class Employee : BaseEntity
{
    public string Name { get; set; }
    public string Position { get; set; }
    public string About { get; set; }
    public string ImageUrl { get; set; }
}
