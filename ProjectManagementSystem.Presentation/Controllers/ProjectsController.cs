using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Presentation.ActionFilters;
using ProjectManagementSystem.Presentation.ModelBinder;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

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
    public async Task<IActionResult> GetProjects([FromQuery] ProjectParameters projectParameters)
    {
        var pagedResult = await _service.ProjectService.GetAllProjectsAsync(projectParameters, trackChanges: false);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.projects);
    }

    [HttpGet("{id:guid}", Name = nameof(GetProject))]
    public async Task<IActionResult> GetProject(Guid id)
    {
        var project = await _service.ProjectService.GetProjectAsync(id, trackChanges: false);
        return Ok(project);
    }

    [HttpGet("{projectId:guid}/employees")]
    public async Task<IActionResult> GetEmployeesByProject(Guid projectId, [FromQuery] EmployeeParameters employeeParameters)
    {
        var pagedResult = await _service.EmployeeService.GetEmployeesByProjectAsync(projectId, employeeParameters, trackChanges: false);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.employees);
    }

    [HttpGet("{projectId:guid}/employees/{employeeId:guid}", Name = nameof(GetEmployeeByProject))]
    public async Task<IActionResult> GetEmployeeByProject(Guid projectId, Guid employeeId)
    {
        var employee = await _service.EmployeeService.GetEmployeeByProjectAsync(projectId, employeeId, trackChanges: false);
        return Ok(employee);
    }

    [HttpGet("{projectId:guid}/projectManager", Name = nameof(GetProjectManager))]
    public async Task<IActionResult> GetProjectManager(Guid projectId)
    {
        var projectManager = await _service.EmployeeService.GetProjectManagerByProjectAsync(projectId, trackChanges: false);
        return Ok(projectManager);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateProject([FromBody] ProjectForCreationDto project)
    {
        var createdProject = await _service.ProjectService.CreateProjectAsync(project);
        return CreatedAtRoute(nameof(GetProject), new { id = createdProject.Id }, createdProject);
    }

    [HttpPost("{projectId:guid}/employees")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateEmployeeForProject(Guid projectId, [FromBody] EmployeeForCreationDto employee)
    {
        if (employee is null)
            return BadRequest("EmployeeForCreationDto object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var employeeToReturn = await _service.EmployeeService
            .CreateEmployeeForProjectAsync(projectId, employee, trackChanges: false);

        return CreatedAtRoute(nameof(GetEmployeeByProject),
            new { projectId, employeeId = employeeToReturn.Id }, employeeToReturn);
    }

    [HttpPost("{projectId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateProjectManagerForProject(Guid projectId, [FromBody] EmployeeForCreationDto projectManager)
    {
        var projectManagerToReturn = await _service.EmployeeService
            .CreateProjectManagerForProjectAsync(projectId, projectManager, trackChanges: false);

        return CreatedAtRoute(nameof(GetProjectManager),
            new { projectId, projectManager = projectManagerToReturn.Id }, projectManagerToReturn);
    }

    [HttpGet("collection/({ids})", Name = nameof(GetProjectCollection))]
    public async Task<IActionResult> GetProjectCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        var projects = await _service.ProjectService.GetByIdsAsync(ids, trackChanges: false);

        return Ok(projects);
    }

    [HttpPost("collection")]
    public async Task<IActionResult> CreateProjectCollection([FromBody] IEnumerable<ProjectForCreationDto> projectCollection)
    {
        var result = await _service.ProjectService.CreateProjectCollectionAsync(projectCollection);
        return CreatedAtRoute(nameof(GetProjectCollection), new { result.ids }, result.projects);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        await _service.ProjectService.DeleteProjectAsync(id, trackChanges: false);
        return NoContent();
    }

    [HttpDelete("{projectId:guid}/employees/{employeeId:guid}")]
    public async Task<IActionResult> DeleteEmployeeForProject(Guid projectId, Guid employeeId)
    {
        await _service.ProjectService.DeleteEmployeeFromProjectAsync(employeeId, projectId, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{projectId:guid}/employees/{employeeId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateEmployeeForProject(Guid projectId, Guid employeeId, [FromBody] EmployeeForUpdateDto employee)
    {
        await _service.EmployeeService.UpdateEmployeeForProjectAsync(projectId, employeeId,
            employee, projectTrackChanges: false, employeeTrackChanges: true);
        return NoContent();
    }

    [HttpPut("{projectId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateProject(Guid projectId, [FromBody] ProjectForUpdateDto project)
    {
        await _service.ProjectService.UpdateProjectAsync(projectId, project, trackChanges: true);
        return NoContent();
    }

    [HttpPatch("{projectId:guid}")]
    public async Task<IActionResult> PartiallyUpdateProject(Guid projectId,
    [FromBody] JsonPatchDocument<ProjectForUpdateDto> patchDocument)
    {
        if (patchDocument is null)
            return BadRequest("patchDocument object sent from client is null.");

        var result = await _service.ProjectService.GetProjectForPatchAsync(projectId, trackChanges: true);

        patchDocument.ApplyTo(result.projectToPatch, ModelState);

        TryValidateModel(result.projectToPatch);

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service.ProjectService.SaveChangesForPatchAsync(result.projectToPatch, result.projectEntity);
        return NoContent();
    }

    [HttpPatch("{projectId:guid}/employees/{employeeId:guid}")]
    public async Task<IActionResult> PartiallyUpdateEmployeeForProject(Guid projectId, Guid employeeId,
        [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDocument)
    {
        if (patchDocument is null)
            return BadRequest("patchDocument object sent from client is null.");

        var result = await _service.EmployeeService.GetEmployeeByProjectForPatchAsync(projectId, employeeId, projectTrackChanges: false, employeeTrackChanges: true);

        patchDocument.ApplyTo(result.employeeToPatch, ModelState);

        TryValidateModel(result.employeeToPatch);

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch, result.employeeEntity);
        return NoContent();
    }
}