namespace KanbanGame.Shared;

public class Feature
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int? EstimatedMinEarnings { get; set; }
    public int? EstimatedMaxEarnings { get; set; }
    public List<KanbanTask> KanbanTasks { get; set; }
    public Employee? Assignee { get; set; }
    public FeatureStatus Status { get; set; }
    public Team.TeamName Team = KanbanGame.Shared.Team.TeamName.HighLevelAnalysis;

    public double DeliveredTaskPercentage
    {
        get
        {
            double tasksCount = KanbanTasks.Count;
            double deliveredTasksCount = KanbanTasks
                .FindAll(t => t.Status == KanbanTask.TaskStatus.Delivered)
                .Count;
            return (deliveredTasksCount / tasksCount);
        }
    }
    public DeliveredPercentage DeliveredTaskPercentageStatus
    {
        get
        {
            double tasksCount = KanbanTasks.Count;
            double deliveredTasksCount = KanbanTasks
                .FindAll(t => t.Status == KanbanTask.TaskStatus.Delivered)
                .Count;
            var p = deliveredTasksCount / tasksCount;
            return PercentageToStatus(p);
        }
    }
    public string SF_PercentageColumn
    {
        get => DeliveredTaskPercentageStatus.ToString();
    } // for Syncfunction D&D
    public string SF_Column
    {
        get => Status.ToString();
        set { Status = (FeatureStatus)Enum.Parse(typeof(FeatureStatus), value); }
    }

    public enum FeatureStatus
    {
        Backlog,
        Doing1,
        Doing2,
        Delivered
    }

    public enum DeliveredPercentage
    {
        None,
        Zero_Twenty,
        Twenty_Forty,
        Forty_Sixty,
        Sixty_Eighty,
        Eighty_Hundred,
        Hundred,
    }

    public void NextFeatureStatus()
    {
        switch (Status)
        {
            case FeatureStatus.Doing1:
                Status = FeatureStatus.Doing2;
                break;
            case FeatureStatus.Doing2:
                Status = FeatureStatus.Delivered;
                break;
        }
    }

    private DeliveredPercentage PercentageToStatus(double p)
    {
        switch (p)
        {
            case 0:
                if (Status == FeatureStatus.Delivered)
                    return DeliveredPercentage.Zero_Twenty;
                else
                    return DeliveredPercentage.None;
            case < 0.2:
                return DeliveredPercentage.Zero_Twenty;
            case < 0.4:
                return DeliveredPercentage.Twenty_Forty;
            case < 0.6:
                return DeliveredPercentage.Forty_Sixty;
            case < 0.8:
                return DeliveredPercentage.Sixty_Eighty;
            case < 1:
                return DeliveredPercentage.Eighty_Hundred;
            case 1:
                return DeliveredPercentage.Hundred;
            default:
                return DeliveredPercentage.None;
        }

    }
}
