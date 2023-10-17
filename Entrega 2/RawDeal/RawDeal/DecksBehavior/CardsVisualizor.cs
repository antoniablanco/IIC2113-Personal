using RawDeal.CardClass;
using RawDeal.PlayerClasses;
using RawDealView.Formatters;

namespace RawDeal.DecksBehavior;

public class CardsVisualizor
{
    public List<string> CreateStringCardList(List<CardController> cardsInSelectedSet)
    {
        return cardsInSelectedSet.Select(cardController => cardController.GetStringCardInfo()).ToList();
    }
    
    public List<String> GetStringCardsForSpecificType(List<Tuple<CardController, int>> cardsInSelectedSet)
    {
        List<string> stringList = new List<string>();

        foreach (var card in cardsInSelectedSet)
            stringList.Add(card.Item1.GetStringPlayedInfo(card.Item2));

        return stringList;
    }
}