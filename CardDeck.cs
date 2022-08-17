
public class CardDeck
{
    private List<Card> cards;

    public CardDeck()
    {
        cards = Card.Setup();
    }

    public List<Card> Draw(int numberOfCards)
    {
        if (CardCount == 0)
        {
            return new List<Card>();
        }

        var drawnCards = new List<Card>();

        var random = new Random();
        for (var index = 0; index < numberOfCards; index++)
        {
            var drawnCard = cards[random.Next(0, cards.Count)];
            cards.Remove(drawnCard);
            drawnCards.Add(drawnCard);
        }

        return drawnCards;
    }

    public int CardCount => cards.Count;
}
