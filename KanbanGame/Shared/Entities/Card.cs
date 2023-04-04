namespace KanbanGame.Shared;

public class Card
{
    public string StatusString { get; set; }
    public int? ColumnId { get; set; }
    public int? InColumnId { get; set; }
    public KanbanTask? KanbanTask { get; set; }
    public Employee? Employee { get; set; }
}
