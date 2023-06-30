using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class ProjectService : IProjectService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public ProjectService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public IEnumerable<ProjectDto> GetAllProjects(bool trackChanges)
    {
        try
        {
            var projects = _repository.Project.GetAllProjects(trackChanges);
            var projectsDto = _mapper.Map<IEnumerable<ProjectDto>>(projects);
            return projectsDto;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong in the {nameof(GetAllProjects)} service method {ex}");
            throw;
        }
    }
}