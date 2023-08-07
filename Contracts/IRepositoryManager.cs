namespace Contracts;

public interface IRepositoryManager
{
    IProjectRepository Project { get; }
    IEmployeeRepository Employee { get; }
    IProjectEmployeeRepository ProjectEmployee { get; }
    Task SaveAsync();
}