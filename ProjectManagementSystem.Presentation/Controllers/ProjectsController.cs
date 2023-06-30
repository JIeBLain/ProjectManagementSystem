using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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
        try
        {
            var projects = _service.ProjectService.GetAllProjects(trackChanges: false);
            return Ok(projects);
        }
        catch (Exception)
        {

            return StatusCode(500, "Internal server error");
        }
    }
}