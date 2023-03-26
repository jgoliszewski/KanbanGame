namespace KanbanGame.Shared;

public class KanbanTask
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public TaskStatus Status { get; set; }
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    // pair programming 
    // todo: think about better solution
    public int? PpEmployeeId { get; set; }
    public Employee? PpEmployee { get; set; }

    public enum TaskStatus
    {
        Waiting,
        Doing,
        Done
    }

    public KanbanTask(int id, string title, string description, TaskStatus status)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = status;
    }
    public void AddAssignee(int employeeId, Employee employee)
    {
        this.EmployeeId = employeeId;
        this.Employee = employee;
    }
    public void AddPpAssignee(int ppEmployeeId, Employee ppEmployee)
    {
        this.PpEmployeeId = ppEmployeeId;
        this.PpEmployee = ppEmployee;
    }
}