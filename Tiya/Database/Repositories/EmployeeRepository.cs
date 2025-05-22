using Microsoft.EntityFrameworkCore;
using Tiya.Database.DomainModels;
using Tiya.Database.Interfaces;
using Tiya.Database.ViewModels;
using Tiya.Helpers.Extentions;

namespace Tiya.Database.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly TiyaDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private const string FOLDER_NAME = "Uploads/Employee";

    public EmployeeRepository(TiyaDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public List<Employee> GetAll()
    {
        return _context.Employees.OrderBy(e => e.Id).ToList();
    }

    public List<Employee> GetSome(int value)
    {
        int totalCount = _context.Employees.Count();
        if (value >= totalCount) return GetAll();

        return _context.Employees.OrderBy(e => e.Id).Take(value).ToList();
    }

    public async Task<Employee> GetById(int? id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        return employee;
    }

    public async Task Insert(CreateEmployeeViewModel model)
    {
        var employee = new Employee()
        {
            Name = model.Name,
            About = model.About,
            Position = model.Position,
            ImageUrl = model.File.CreateFile(_environment.WebRootPath,FOLDER_NAME)
        };

        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int? id, UpdateEmployeeViewModel model)
    {
        var employee = await GetById(id);
        if (employee is null) return;

        employee.Name = model.Name;
        employee.About = model.About;
        employee.Position = model.Position;

        if (model.File is not null)
            employee.ImageUrl = model.File.UpdateFile(_environment.WebRootPath, FOLDER_NAME, employee.ImageUrl);

        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(int? id)
    {
        var employee = await GetById(id);
        if (employee is null) return;

        FileExtention.RemoveFile(Path.Combine(_environment.WebRootPath, FOLDER_NAME, employee.ImageUrl));
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }
}
