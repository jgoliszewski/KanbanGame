using KanbanGame.Shared;

namespace KanbanGame.Client.Services;
public interface ICardService
{
    List<Card> Cards { get; set; }
    Task<List<Card>> GetCards();
    Task GetCardsByColumnId();
    Task<Employee?> GetCardById(int employeeId);
}

