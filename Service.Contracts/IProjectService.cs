﻿using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IProjectService
{
    IEnumerable<ProjectDto> GetAllProjects(bool trackChanges);
}