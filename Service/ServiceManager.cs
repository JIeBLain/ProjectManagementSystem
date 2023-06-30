using Contracts;
using Service.Contracts;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IProjectService> _projectService;
    private readonly Lazy<IEmployeeService> _employeeService;

    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger)
    {
        _projectService = new Lazy<IProjectService>(() => new ProjectService(repositoryManager, logger));
        _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager, logger));
    }

    public IProjectService ProjectService => _projectService.Value;
    public IEmployeeService EmployeeService => _employeeService.Value;
}
