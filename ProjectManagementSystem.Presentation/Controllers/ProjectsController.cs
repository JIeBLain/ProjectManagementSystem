using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ProjectManagementSystem.Presentation.Controllers;

[Route("api/projects")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IServiceManager _service;

    public ProjectsController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetProjects()
    {
        var projects = _service.ProjectService.GetAllProjects(trackChanges: false);
        return Ok(projects);
    }

    [HttpGet("{id:guid}", Name = "ProjectById")]
    public IActionResult GetProject(Guid id)
    {
        var project = _service.ProjectService.GetProject(id, trackChanges: false);
        return Ok(project);
    }

    [HttpGet("{projectId:guid}/employees")]
    public IActionResult GetEmployeesByProject(Guid projectId)
    {
        var employees = _service.EmployeeService.GetEmployeesByProject(projectId, trackChanges: false);
        return Ok(employees);
    }

    [HttpGet("{projectId:guid}/employees/{employeeId:guid}")]
    public IActionResult GetEmployeeByProject(Guid projectId, Guid employeeId)
    {
        var employee = _service.EmployeeService.GetEmployeeByProject(projectId, employeeId, trackChanges: false);
        return Ok(employee);
    }

    [HttpGet("{projectId:guid}/projectManager")]
    public IActionResult GetProjectManager(Guid projectId)
    {
        var projectManager = _service.EmployeeService.GetProjectManager(projectId, trackChanges: false);
        return Ok(projectManager);
    }

    [HttpPost]
    public IActionResult CreateProject([FromBody] ProjectForCreationDto project)
    {
        if (project is null)
            return BadRequest("ProjectForCreationDto object is null");

        var createdProject = _service.ProjectService.CreateProject(project);

        return CreatedAtRoute("ProjectById", new { id = createdProject.Id }, createdProject);
    }
}