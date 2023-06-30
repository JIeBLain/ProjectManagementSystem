﻿using Entities.Models;

namespace Contracts;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetAllEmployees(bool trackChanges);
    Employee GetEmployee(Guid employeeId, bool trackChanges);
}