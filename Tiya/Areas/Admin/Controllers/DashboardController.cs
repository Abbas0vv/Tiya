using Microsoft.AspNetCore.Mvc;
using Tiya.Database.Interfaces;
using Tiya.Database.ViewModels;

namespace Tiya.Areas.Admin.Controllers;

[Area(nameof(Admin))]
public class DashboardController : Controller
{
    private readonly IEmployeeRepository _repository;

    public DashboardController(IEmployeeRepository repository)
    {
        _repository = repository;
    }


    [HttpGet]
    public IActionResult Index()
    {
        var employees = _repository.GetAll();
        return View(employees);
    }

    [HttpGet]
    public IActionResult NotFound()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeViewModel model)
    {
        if(!ModelState.IsValid)return View(model);
        await _repository.Insert(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int? id)
    {
        if (id is null) return RedirectToAction(nameof(NotFound));
        var employee = await _repository.GetById(id);
        if (employee is null) return RedirectToAction(nameof(NotFound));
        var model = new UpdateEmployeeViewModel()
        {
            Name = employee.Name,
            Position = employee.Position,
            About = employee.About,
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int? id, UpdateEmployeeViewModel model)
    {
        if (id is null) return RedirectToAction(nameof(NotFound));
        var employee = await _repository.GetById(id);
        if (employee is null) return RedirectToAction(nameof(NotFound));
        if (!ModelState.IsValid) return View(model);
        await _repository.Update(id, model);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return RedirectToAction(nameof(NotFound));
        await _repository.Remove(id);
        return RedirectToAction(nameof(Index));
    }
}

