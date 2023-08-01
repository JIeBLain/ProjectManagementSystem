﻿using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Presentation.ModelBinder;
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

    [HttpGet("{projectId:guid}/employees/{employeeId:guid}", Name = "EmployeeByProject")]
    public IActionResult GetEmployeeByProject(Guid projectId, Guid employeeId)
    {
        var employee = _service.EmployeeService.GetEmployeeByProject(projectId, employeeId, trackChanges: false);
        return Ok(employee);
    }

    [HttpGet("{projectId:guid}/projectManager", Name = "ProjectManagerByProject")]
    public IActionResult GetProjectManager(Guid projectId)
    {
        var projectManager = _service.EmployeeService.GetProjectManagerByProject(projectId, trackChanges: false);
        return Ok(projectManager);
    }

    [HttpPost]
    public IActionResult CreateProject([FromBody] ProjectForCreationDto project)
    {
        if (project is null)
            return BadRequest("ProjectForCreationDto object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var createdProject = _service.ProjectService.CreateProject(project);

        return CreatedAtRoute("ProjectById", new { id = createdProject.Id }, createdProject);
    }

    [HttpPost("{projectId:guid}/employees")]
    public IActionResult CreateEmployeeForProject(Guid projectId, [FromBody] EmployeeForCreationDto employee)
    {
        if (employee is null)
            return BadRequest("EmployeeForCreationDto object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var employeeToReturn = _service.EmployeeService
            .CreateEmployeeForProject(projectId, employee, trackChanges: false);

        return CreatedAtRoute("EmployeeByProject",
            new { projectId, employeeId = employeeToReturn.Id }, employeeToReturn);
    }

    [HttpPost("{projectId:guid}")]
    public IActionResult CreateProjectManagerForProject(Guid projectId, [FromBody] EmployeeForCreationDto projectManager)
    {
        if (projectManager is null)
            return BadRequest("EmployeeForCreationDto object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var projectManagerToReturn = _service.EmployeeService
            .CreateProjectManagerForProject(projectId, projectManager, trackChanges: false);

        return CreatedAtRoute("ProjectManagerByProject",
            new { projectId, projectManager = projectManagerToReturn.Id }, projectManagerToReturn);
    }

    [HttpGet("collection/({ids})", Name = "ProjectCollection")]
    public IActionResult GetProjectCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        var projects = _service.ProjectService.GetByIds(ids, trackChanges: false);

        return Ok(projects);
    }

    [HttpPost("collection")]
    public IActionResult CreateProjectCollection([FromBody] IEnumerable<ProjectForCreationDto> projectCollection)
    {
        var result = _service.ProjectService.CreateProjectCollection(projectCollection);
        return CreatedAtRoute("ProjectCollection", new { result.ids }, result.projects);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteProject(Guid id)
    {
        _service.ProjectService.DeleteProject(id, trackChanges: false);
        return NoContent();
    }

    [HttpDelete("{projectId:guid}/employees/{employeeId:guid}")]
    public IActionResult DeleteEmployeeForProject(Guid projectId, Guid employeeId)
    {
        _service.EmployeeService.DeleteProjectFromEmployee(projectId, employeeId, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{projectId:guid}/employees/{employeeId:guid}")]
    public IActionResult UpdateEmployeeForProject(Guid projectId, Guid employeeId, [FromBody] EmployeeForUpdateDto employee)
    {
        if (employee is null)
            return BadRequest("EmployeeForUpdateDto object is null");

        _service.EmployeeService.UpdateEmployeeForProject(projectId, employeeId,
            employee, projectTrackChanges: false, employeeTrackChanges: true);
        return NoContent();
    }

    [HttpPut("{projectId:guid}")]
    public IActionResult UpdateProject(Guid projectId, [FromBody] ProjectForUpdateDto project)
    {
        if (project is null)
            return BadRequest("ProjectForUpdateDto object is null");

        _service.ProjectService.UpdateProject(projectId, project, trackChanges: true);
        return NoContent();
    }

    [HttpPatch("{projectId:guid}")]
    public IActionResult PartiallyUpdateProject(Guid projectId,
    [FromBody] JsonPatchDocument<ProjectForUpdateDto> patchDocument)
    {
        if (patchDocument is null)
            return BadRequest("patchDocument object sent from client is null.");

        var result = _service.ProjectService.GetProjectForPatch(projectId, trackChanges: true);

        patchDocument.ApplyTo(result.projectToPatch);

        _service.ProjectService.SaveChangesForPatch(result.projectToPatch, result.projectEntity);
        return NoContent();
    }

    [HttpPatch("{projectId:guid}/employees/{employeeId:guid}")]
    public IActionResult PartiallyUpdateEmployeeForProject(Guid projectId, Guid employeeId,
        [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDocument)
    {
        if (patchDocument is null)
            return BadRequest("patchDocument object sent from client is null.");

        var result = _service.EmployeeService.GetEmployeeForPatch(projectId, employeeId, projectTrackChanges: false, employeeTrackChanges: true);

        patchDocument.ApplyTo(result.employeeToPatch);

        _service.EmployeeService.SaveChangesForPatch(result.employeeToPatch, result.employeeEntity);
        return NoContent();
    }
}