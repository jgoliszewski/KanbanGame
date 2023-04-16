using KanbanGame.Shared;

namespace KanbanGame.Client.Services;
public interface ICardService
{
    List<Card> Cards { get; set; }
    Task GetCards();
    Task UpdateCard(int cardId, Card card);
    Task UpdateCards();
}

