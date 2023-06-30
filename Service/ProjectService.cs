using Contracts;
using Service.Contracts;

namespace Service;

internal sealed class ProjectService : IProjectService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;

    public ProjectService(IRepositoryManager repository, ILoggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }
}