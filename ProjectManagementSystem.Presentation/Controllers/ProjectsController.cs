﻿using Microsoft.AspNetCore.Mvc;
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
        var projects = _service.ProjectService.GetAllProjects(trackChanges: false);
        return Ok(projects);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetProject(Guid id)
    {
        var project = _service.ProjectService.GetProject(id, trackChanges: false);
        return Ok(project);
    }
}