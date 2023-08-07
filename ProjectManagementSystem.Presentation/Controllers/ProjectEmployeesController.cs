using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Presentation.ActionFilters;
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
    public async Task<IActionResult> GetAllProjectEmployees()
    {
        var projectEmployees = await _service.ProjectEmployeeService.GetAllProjectEmployeesAsync(false);
        return Ok(projectEmployees);
    }

    [HttpGet("projects/{projectId:guid}/employees/{employeeId:guid}", Name = "ProjectEmployee")]
    public async Task<IActionResult> GetProjectEmployee(Guid projectId, Guid employeeId)
    {
        var projectEmployee = await _service.ProjectEmployeeService.GetProjectEmployeeAsync(projectId, employeeId, false);
        return Ok(projectEmployee);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateProjectEmployee([FromBody] ProjectEmployeeForCreationDto projectEmployee)
    {
        var projectEmployeeDto = await _service.ProjectEmployeeService.CreateProjectEmployeeAsync(projectEmployee, false);
        return CreatedAtRoute("ProjectEmployee", new { projectId = projectEmployee.ProjectId, employeeId = projectEmployee.EmployeeId }, projectEmployeeDto);
    }
}
