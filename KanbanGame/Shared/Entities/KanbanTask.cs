namespace KanbanGame.Shared;

public class KanbanTask
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public TaskStatus Status { get; set; }
    public Employee? Employee { get; set; }
    public string SF_Column
    {
        get => Status.ToString();
        set
        {
            Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), value);
        }
    } // for Syncfunction D&D

    public void AddAssignee(int employeeId, Employee employee)
    {
        this.Employee = employee;
    }
    public void UpdateTaskStatus()
    {
        if (Employee is null)
        {
            switch (Status)
            {
                case TaskStatus.AnalysisDoing:
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

    public enum TaskStatus
    {
        Backlog,
        AnalysisDoing,
        DevelopmentWaiting,
        DevelopmentDoing,
        TestWaiting,
        TestDoing,
        Delivered
    }

}