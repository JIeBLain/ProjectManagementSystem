using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Presentation.ActionFilters;
using ProjectManagementSystem.Presentation.ModelBinder;
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
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _service.EmployeeService.GetAllEmployeesAsync(trackChanges: false);
        return Ok(employees);
    }

    [HttpGet("{id:guid}", Name = "EmployeeById")]
    public async Task<IActionResult> GetEmployee(Guid id)
    {
        var employee = await _service.EmployeeService.GetEmployeeAsync(id, trackChanges: false);
        return Ok(employee);
    }

    [HttpGet("{employeeId:guid}/projects")]
    public async Task<IActionResult> GetProjectsByEmployee(Guid employeeId)
    {
        var projects = await _service.ProjectService.GetProjectsByEmployeeAsync(employeeId, trackChanges: false);
        return Ok(projects);
    }

    [HttpGet("{employeeId:guid}/projects/{projectId:guid}")]
    public async Task<IActionResult> GetProjectByEmployee(Guid employeeId, Guid projectId)
    {
        var project = await _service.ProjectService.GetProjectByEmployeeAsync(employeeId, projectId, trackChanges: false);
        return Ok(project);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeForCreationDto employee)
    {
        var createdEmployee = await _service.EmployeeService.CreateEmployeeAsync(employee);
        return CreatedAtRoute("EmployeeById", new { id = createdEmployee.Id }, createdEmployee);
    }

    [HttpGet("collection/({ids})", Name = "EmployeeCollection")]
    public async Task<IActionResult> GetEmployeeCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        var employees = await _service.EmployeeService.GetByIdsAsync(ids, trackChanges: false);

        return Ok(employees);
    }

    [HttpPost("collection")]
    public async Task<IActionResult> CreateEmployeeCollection([FromBody] IEnumerable<EmployeeForCreationDto> employeeCollection)
    {
        var result = await _service.EmployeeService.CreateEmployeeCollectionAsync(employeeCollection);
        return CreatedAtRoute("EmployeeCollection", new { result.ids }, result.employees);
    }

    [HttpGet("withoutProject")]
    public async Task<IActionResult> GetEmployeesWithoutProject()
    {
        var employees = await _service.EmployeeService.GetEmployeesWithoutProjectAsync(trackChanges: false);
        return Ok(employees);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        await _service.EmployeeService.DeleteEmployeeAsync(id, trackChanges: false);
        return NoContent();
    }

    [HttpDelete("{employeeId:guid}/projects/{projectId:guid}")]
    public async Task<IActionResult> DeleteProjectForEmployee(Guid employeeId, Guid projectId)
    {
        await _service.ProjectService.DeleteEmployeeFromProjectAsync(employeeId, projectId, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{employeeId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateEmployee(Guid employeeId, [FromBody] EmployeeForUpdateDto employee)
    {
        await _service.EmployeeService.UpdateEmployeeAsync(employeeId, employee, trackChanges: true);
        return NoContent();
    }

    [HttpPatch("{employeeId:guid}")]
    public async Task<IActionResult> PartiallyUpdateEmployee(Guid employeeId,
        [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDocument)
    {
        if (patchDocument is null)
            return BadRequest("patchDocument object sent from client is null.");

        var result = await _service.EmployeeService.GetEmployeeForPatchAsync(employeeId, trackChanges: true);

        patchDocument.ApplyTo(result.employeeToPatch, ModelState);

        TryValidateModel(result.employeeToPatch);

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch, result.employeeEntity);
        return NoContent();
    }
}