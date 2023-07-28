namespace Service.Contracts;

public interface IServiceManager
{
    IProjectService ProjectService { get; }
    IEmployeeService EmployeeService { get; }
    IProjectEmployeeService ProjectEmployeeService { get; }
}