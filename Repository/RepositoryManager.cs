using Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IProjectRepository> _projectRepository;
    private readonly Lazy<IEmployeeRepository> _employeeRepository;
    private readonly Lazy<IProjectEmployeeRepository> _projectEmployeeRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _projectRepository = new Lazy<IProjectRepository>(() => new ProjectRepository(repositoryContext));
        _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryContext));
        _projectEmployeeRepository = new Lazy<IProjectEmployeeRepository>(() => new ProjectEmployeeRepository(repositoryContext));
    }

    public IProjectRepository Project => _projectRepository.Value;

    public IEmployeeRepository Employee => _employeeRepository.Value;

    public IProjectEmployeeRepository ProjectEmployee => _projectEmployeeRepository.Value;

    public async Task SaveAsync()
    {
        await _repositoryContext.SaveChangesAsync();
    }
}