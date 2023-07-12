using Entities.Models;

namespace Contracts;

public interface IProjectEmployeeRepository
{
    void CreateProjectEmployee(Project project, Employee employee);
}