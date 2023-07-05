﻿using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IProjectService
{
    IEnumerable<ProjectDto> GetAllProjects(bool trackChanges);
    ProjectDto GetProject(Guid id, bool trackChanges);
    IEnumerable<ProjectDto> GetProjectsByEmployee(Guid employeeId, bool trackChanges);
    ProjectDto GetProjectByEmployee(Guid employeeId, Guid projectId, bool trackChanges);
    ProjectDto CreateProject(ProjectForCreationDto project);
}