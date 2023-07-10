using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

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

    [HttpGet("{id:guid}", Name = "EmployeeById")]
    public IActionResult GetEmployee(Guid id)
    {
        var employee = _service.EmployeeService.GetEmployee(id, trackChanges: false);
        return Ok(employee);
    }

    [HttpGet("{employeeId:guid}/projects")]
    public IActionResult GetProjectsByEmployee(Guid employeeId)
    {
        var projects = _service.ProjectService.GetProjectsByEmployee(employeeId, trackChanges: false);
        return Ok(projects);
    }

    [HttpGet("{employeeId:guid}/projects/{projectId:guid}", Name = "ProjectByEmployee")]
    public IActionResult GetProjectByEmployee(Guid employeeId, Guid projectId)
    {
        var project = _service.ProjectService.GetProjectByEmployee(employeeId, projectId, trackChanges: false);
        return Ok(project);
    }

    [HttpPost]
    public IActionResult CreateEmployee([FromBody] EmployeeForCreationDto employee)
    {
        if (employee is null)
            return BadRequest("EmployeeForCreationDto object is null");

        var createdEmployee = _service.EmployeeService.CreateEmployee(employee);
        return CreatedAtRoute("EmployeeById", new { id = createdEmployee.Id }, createdEmployee);
    }

    [HttpPost("{employeeId:guid}/projects")]
    public IActionResult CreateProjectForEmployee(Guid employeeId, ProjectForCreationDto project)
    {
        if (project is null)
            return BadRequest("ProjectForCreationDto object is null");

        var createdProject = _service.ProjectService.CreateProjectForEmployee(employeeId, project, trackChanges: false);
        return CreatedAtRoute("ProjectByEmployee", new { employeeId, projectId = createdProject.Id }, createdProject);
    }
}