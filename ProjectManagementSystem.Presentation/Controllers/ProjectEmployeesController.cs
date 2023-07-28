using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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

    [HttpGet("projects/{projectId:guid}/employees/{employeeId:guid}")]
    public IActionResult GetProjectEmployee(Guid projectId, Guid employeeId)
    {
        var projectEmployee = _service.ProjectEmployeeService.GetProjectEmployee(projectId, employeeId, false);
        return Ok(projectEmployee);
    }
}
