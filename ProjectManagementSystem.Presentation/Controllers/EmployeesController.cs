﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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

    [HttpGet("{id:guid}")]
    public IActionResult GetEmployee(Guid id)
    {
        var employee = _service.EmployeeService.GetEmployee(id, trackChanges: false);
        return Ok(employee);
    }
}