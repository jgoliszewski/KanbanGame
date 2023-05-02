namespace KanbanGame.Shared;

public class Card
{
    public int Id { get; set; }
    public int? RankId { get; set; }
    public KanbanTask? KanbanTask { get; set; }
    public Employee? Employee { get; set; }
    public Feature? Feature { get; set; }
    public Team.TeamName Team
    {
        get
        {
            if (Employee is not null)
            {
                return Employee.Team;
            }
            else if (KanbanTask is not null)
            {
                return KanbanTask.Team;
            }
            else if (Feature is not null)
            {
                return Feature.Team;
            }
            return KanbanGame.Shared.Team.TeamName.HighLevelAnalysis;
        }
        set
        {
            if (Employee is not null)
            {
                Employee.Team = value;
            }
            else if (KanbanTask is not null)
            {
                KanbanTask.Team = value;
            }
            else if (Feature is not null)
            {
                Feature.Team = value;
            }
        }
    }
    public string Column
    {
        get
        {
            if (Employee is not null)
            {
                return Employee.SF_Column;
            }
            else if (KanbanTask is not null)
            {
                return KanbanTask.SF_Column;
            }
            else if (Feature is not null)
            {
                return Feature.SF_Column;
            }
            return "";
        }
        set
        {
            if (Employee is not null)
            {
                Employee.SF_Column = value;
            }
            else if (KanbanTask is not null)
            {
                KanbanTask.SF_Column = value;
            }
            else if (Feature is not null)
            {
                Feature.SF_Column = value;
            }
        }
    }
}
