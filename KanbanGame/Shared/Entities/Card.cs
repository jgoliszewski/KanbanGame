namespace KanbanGame.Shared;

public class Card
{
    public int Id { get; set; }
    public string Column { get; set; }
    public int? InColumnId { get; set; }
    public KanbanTask? KanbanTask { get; set; }
    public Employee? Employee { get; set; }
}
