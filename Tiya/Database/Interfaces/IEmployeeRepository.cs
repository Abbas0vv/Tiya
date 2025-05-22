using Microsoft.EntityFrameworkCore;
using Tiya.Database.DomainModels;
using Tiya.Database.ViewModels;
using Tiya.Extentions;

namespace Tiya.Database.Interfaces;

public interface IEmployeeRepository
{
    List<Employee> GetAll();
    List<Employee> GetSome(int value);
    Task<Employee> GetById(int? id);
    Task Insert(CreateEmployeeViewModel model);
    Task Update(int? id, UpdateEmployeeViewModel model);
    Task Remove(int? id);
}
