namespace KanbanGame.Shared;

public class Card
{
    public int Id { get; set; }
    public int? RankId { get; set; }
    public KanbanTask? KanbanTask { get; set; }
    public Employee? Employee { get; set; }
    public string Column 
    { 
        get
        {
            if (Employee is not null && KanbanTask is null)
            {
                return Employee.SF_Column;
            }
            else if (KanbanTask is not null && Employee is null)
            {
                return KanbanTask.SF_Column;
            }
            return "";
        }
        set
        {
            if (Employee is not null && KanbanTask is null)
            {
                Employee.SF_Column = value;
            }
            else if (KanbanTask is not null && Employee is null)
            {
                KanbanTask.SF_Column = value;
            }
        } 
    }
}
