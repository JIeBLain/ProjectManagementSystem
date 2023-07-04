using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace ProjectManagementSystem.Presentation.Controllers;

[Route("api/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private IServiceManager _service;

    public EmployeesController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetEmployees()
    {
        var employees = _service.EmployeeService.GetAllEmployees(trackChanges: false);
        return Ok(employees);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetEmployee(Guid id)
    {
        var employee = _service.EmployeeService.GetEmployee(id, trackChanges: false);
        return Ok(employee);
    }

    [HttpGet("{employeeId}/projects")]
    public IActionResult GetProjectsByEmployee(Guid employeeId)
    {
        var projects = _service.ProjectService.GetProjectsByEmployee(employeeId, trackChanges: false);
        return Ok(projects);
    }

    [HttpGet("{employeeId}/projects/{projectId}")]
    public IActionResult GetProjectByEmployee(Guid employeeId, Guid projectId)
    {
        var project = _service.ProjectService.GetProjectByEmployee(employeeId, projectId, trackChanges: false);
        return Ok(project);
    }
}