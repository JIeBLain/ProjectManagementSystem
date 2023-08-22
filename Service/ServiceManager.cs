using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IProjectService> _projectService;
    private readonly Lazy<IEmployeeService> _employeeService;
    private readonly Lazy<IProjectEmployeeService> _projectEmployeeService;

    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, IDataShaper<EmployeeDto> employeeDataShaper, IDataShaper<ProjectDto> projectDataShaper)
    {
        _projectService = new Lazy<IProjectService>(() => new ProjectService(repositoryManager, logger, mapper, projectDataShaper));
        _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager, logger, mapper, employeeDataShaper));
        _projectEmployeeService = new Lazy<IProjectEmployeeService>(() => new ProjectEmployeeService(repositoryManager, logger, mapper));
    }

    public IProjectService ProjectService => _projectService.Value;
    public IEmployeeService EmployeeService => _employeeService.Value;
    public IProjectEmployeeService ProjectEmployeeService => _projectEmployeeService.Value;
}
