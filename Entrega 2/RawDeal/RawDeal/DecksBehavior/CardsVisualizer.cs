using RawDeal.CardClasses;

namespace RawDeal.DecksBehavior;

public class CardsVisualizer
{
    public List<string> CreateStringCardList(List<CardController> cardsInSelectedSet)
    {
        return cardsInSelectedSet.Select(cardController => cardController.GetStringCardInfo()).ToList();
    }

    public List<string> GetStringCardsForSpecificType(List<Tuple<CardController, int>> cardsInSelectedSet)
    {
        var stringList = new List<string>();

        foreach (var card in cardsInSelectedSet)
            stringList.Add(card.Item1.GetStringPlayedInfo(card.Item2));

        return stringList;
    }
}