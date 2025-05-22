using Microsoft.AspNetCore.Mvc;
using Tiya.Database.Interfaces;
namespace Tiya.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;

    public HomeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IActionResult Index()
    {
        var employees = _employeeRepository.GetSome(2);
        return View(employees);
    }
}
