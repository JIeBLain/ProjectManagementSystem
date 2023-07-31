using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ProjectManagementSystem.Presentation.Controllers;

[Route("api/projectEmployees")]
[ApiController]
public class ProjectEmployeesController : ControllerBase
{
    private IServiceManager _service;

    public ProjectEmployeesController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllProjectEmployees()
    {
        var projectEmployees = _service.ProjectEmployeeService.GetAllProjectEmployees(false);
        return Ok(projectEmployees);
    }

    [HttpGet("projects/{projectId:guid}/employees/{employeeId:guid}", Name = "ProjectEmployee")]
    public IActionResult GetProjectEmployee(Guid projectId, Guid employeeId)
    {
        var projectEmployee = _service.ProjectEmployeeService.GetProjectEmployee(projectId, employeeId, false);
        return Ok(projectEmployee);
    }

    [HttpPost]
    public IActionResult CreateProjectEmployee([FromBody] ProjectEmployeeForCreationDto projectEmployee)
    {
        if (projectEmployee is null)
            return BadRequest("ProjectEmployeeForCreation object is null");

        var projectEmployeeDto = _service.ProjectEmployeeService.CreateProjectEmployee(projectEmployee, false);
        return CreatedAtRoute("ProjectEmployee", new { projectId = projectEmployee.ProjectId, employeeId = projectEmployee.EmployeeId }, projectEmployeeDto);
    }
}
