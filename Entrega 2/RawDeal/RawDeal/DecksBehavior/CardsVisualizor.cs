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

    public List<string> CreateStringPlayedCardListForReversalType(List<CardController> cardsInSelectedSet)
    {
        List<string> stringList = new List<string>();

        foreach (var cardController in cardsInSelectedSet)
        {
            stringList.AddRange(GetStringPlayedCardForReversalType(cardController));
        }

        return stringList;
    }

    private List<String> GetStringPlayedCardForReversalType(CardController cardController)
    {
        int[] indexes = Enumerable.Range(0, cardController.GetCardTypes().Count()).ToArray();

        return (from index in indexes where cardController.GetCardType(index) == "Reversal" 
            select cardController.GetStringPlayedInfo(index)).ToList();

    }
    
    public List<String> GetStringCardsForNotReversalType(List<Tuple<CardController, int>> cardsInSelectedSet)
    {
        List<string> stringList = new List<string>();

        foreach (var card in cardsInSelectedSet)
            stringList.Add(card.Item1.GetStringPlayedInfo(card.Item2));

        return stringList;
    }
}