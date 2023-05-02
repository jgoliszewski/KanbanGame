namespace KanbanGame.Shared;

public class Team
{
    public int Id { get; set; }
    public List<Employee> Employees { get; set; }

    public enum TeamName
    {
        HighLevelAnalysis,
        FrontEnd,
        BackEnd,
    }
}
