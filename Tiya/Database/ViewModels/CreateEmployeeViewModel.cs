namespace Tiya.Database.ViewModels;

public class CreateEmployeeViewModel
{
    public string Name { get; set; }
    public string Position { get; set; }
    public string About { get; set; }
    public IFormFile File { get; set; }
}
