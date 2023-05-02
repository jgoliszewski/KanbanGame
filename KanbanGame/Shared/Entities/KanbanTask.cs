namespace KanbanGame.Shared;

public class KanbanTask
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public TaskStatus Status { get; set; }
    public Employee? Assignee { get; set; }
    public Team.TeamName Team { get; set; }
    public string SF_Column
    {
        get => Status.ToString();
        set { Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), value); }
    } // for Syncfunction D&D

    public void AddAssignee(Employee employee)
    {
        this.Assignee = employee;
    }

    public void NextTaskStatus()
    {
        switch (Status)
        {
            case TaskStatus.AnalysisDoing:
                Status = TaskStatus.DevelopmentWaiting;
                break;
            case TaskStatus.DevelopmentDoing:
                Status = TaskStatus.TestWaiting;
                break;
            case TaskStatus.TestDoing:
                Status = TaskStatus.Delivered;
                break;
        }
    }

    public void MoveToWaiting()
    {
        switch (Status)
        {
            case TaskStatus.AnalysisDoing:
                Status = TaskStatus.Backlog;
                break;
            case TaskStatus.DevelopmentDoing:
                Status = TaskStatus.DevelopmentWaiting;
                break;
            case TaskStatus.TestDoing:
                Status = TaskStatus.TestWaiting;
                break;
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
