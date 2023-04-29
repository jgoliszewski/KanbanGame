namespace KanbanGame.Shared;

public class Feature
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public List<KanbanTask> KanbanTasks {get; set;}
    public FeatureStatus Status { get; set; }

    private DeliveredPercentage DeliveredTaskPercentage {
        get
        {
            var tasksCount = KanbanTasks.Count;
            var deliveredTasksCount = KanbanTasks.FindAll(t => t.Status == KanbanTask.TaskStatus.Delivered).Count;
            return PercentageToStatus(deliveredTasksCount/tasksCount);
        }
    }
    public string SF_Column
    {
        get => DeliveredTaskPercentage.ToString();
    } // for Syncfunction D&D

    public enum FeatureStatus
    {
        Backlog,
        Doing,
        Delivered
    }
    
    public enum DeliveredPercentage
    {
        Zero_Twenty,
        Twenty_Forty,
        Forty_Sixty,
        Sixty_Eighty,
        Eighty_Hundred,
        Hundred,
    }

    private DeliveredPercentage PercentageToStatus(double p)
    {
        switch(p)
        {
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
            default:
                return DeliveredPercentage.Hundred;
        }
    }

}