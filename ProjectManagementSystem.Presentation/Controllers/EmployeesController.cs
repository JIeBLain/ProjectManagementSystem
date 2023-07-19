using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("{employeeId:guid}/projects/{projectId:guid}")]
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

    [HttpGet("collection/({ids})", Name = "EmployeeCollection")]
    public IActionResult GetEmployeeCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        var employees = _service.EmployeeService.GetByIds(ids, trackChanges: false);

        return Ok(employees);
    }

    [HttpPost("collection")]
    public IActionResult CreateEmployeeCollection([FromBody] IEnumerable<EmployeeForCreationDto> employeeCollection)
    {
        var result = _service.EmployeeService.CreateEmployeeCollection(employeeCollection);
        return CreatedAtRoute("EmployeeCollection", new { result.ids }, result.employees);
    }

    [HttpGet("withoutProject")]
    public IActionResult GetEmployeesWithoutProject()
    {
        var employees = _service.EmployeeService.GetEmployeesWithoutProject(trackChanges: false);
        return Ok(employees);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteEmployee(Guid id)
    {
        _service.EmployeeService.DeleteEmployee(id, trackChanges: false);
        return NoContent();
    }

    [HttpDelete("{employeeId:guid}/projects/{projectId:guid}")]
    public IActionResult DeleteProjectForEmployee(Guid employeeId, Guid projectId)
    {
        _service.ProjectService.DeleteProjectForEmployee(employeeId, projectId, trackChanges: false);
        return NoContent();
    }
}