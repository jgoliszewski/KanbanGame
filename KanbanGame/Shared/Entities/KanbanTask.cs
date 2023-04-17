namespace KanbanGame.Shared;

public class KanbanTask
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public TaskStatus Status { get; set; }
    public string StatusString
    {
        get => Status.ToString();
        set
        {
            Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), value);
        }
    } // for Syncfunction
    public Employee? Employee { get; set; }

    // pair programming 
    // todo: think about better solution
    public int? PpEmployeeId { get; set; }
    public Employee? PpEmployee { get; set; }

    public enum TaskStatus
    {
        Backlog,
        Analysis,
        DevelopmentWaiting,
        DevelopmentDoing,
        TestWaiting,
        TestDoing,
        Delivered
    }
    public void AddAssignee(int employeeId, Employee employee)
    {
        this.Employee = employee;
    }
    public void AddPpAssignee(int ppEmployeeId, Employee ppEmployee)
    {
        this.PpEmployee = ppEmployee;
    }
    public void UpdateTaskStatus()
    {
        if (Employee is null)
        {
            switch (Status)
            {
                case TaskStatus.Analysis:
                    Status = TaskStatus.Backlog;
                    break;
                case TaskStatus.DevelopmentDoing:
                    Status = TaskStatus.DevelopmentWaiting;
                    break;
                case TaskStatus.TestWaiting:
                    Status = TaskStatus.TestWaiting;
                    break;
            }
        }
    }

}