namespace KanbanGame.Shared;

public class KanbanTask
{
    public int Id { get; set; }
    public string Title { get; set; } = "Title";
    public string? Description { get; set; }
    public int Age { get; set; } = 0;
    public TaskType Type { get; set; }
    public Employee? Assignee { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.None;
    public Team.TeamName Team { get; set; } = Shared.Team.TeamName.None;
    public string SF_Column
    {
        get => Status.ToString();
        set { Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), value); }
    } // for Syncfunction D&D

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

    public enum TaskStatus
    {
        None,
        ReadyForDevelopment,
        Backlog,
        AnalysisDoing,
        DevelopmentWaiting,
        DevelopmentDoing,
        TestWaiting,
        TestDoing,
        Delivered
    }

    public enum TaskType
    {
        FrontEnd,
        BackEnd
    }
}
