using KanbanGame.Shared;

namespace KanbanGame.Client.Services;
public interface ICardService
{
    List<Card> Cards { get; set; }
    Dictionary<string, int> ColumnMaxCount { get; set; }
    Task GetCards();
    Task UpdateCard(int cardId, Card card);
    Task UpdateCardLocal(int cardId, Card card);
    Task UpdateColumn(string column);
}

