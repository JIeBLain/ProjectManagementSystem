namespace Entities.Models;

public class ProjectEmployee
{
    public Guid ProjectId { get; set; }
    public Guid EmployeeId { get; set; }
    public Project? Project { get; set; }
    public Employee? Employee { get; set; }
    public Guid? ProjectManagerId { get; set; }
    public Employee? ProjectManager { get; set; }
}