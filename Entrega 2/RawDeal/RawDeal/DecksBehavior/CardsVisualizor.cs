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
        List<string> stringList = new List<string>();
        foreach (var index in indexes)
        {   
            if (cardController.GetCardType(index) == "Reversal")
                stringList.Add(cardController.GetStringPlayedInfo(index));
        }

        return stringList;

    }
    
    public List<string> CreateStringPlayedCardListForNotReversalType(List<CardController> cardsInSelectedSet, PlayerController controllerCurrentPlayer)
    {
        List<string> stringList = new List<string>();

        foreach (var cardController in cardsInSelectedSet)
        {
            stringList.AddRange(GetStringPlayedCardForNotReversalType(cardController, controllerCurrentPlayer));
        }

        return stringList;
    }

    private List<String> GetStringPlayedCardForNotReversalType(CardController cardController, PlayerController controllerCurrentPlayer)
    {
        int[] indexes = Enumerable.Range(0, cardController.GetCardTypes().Count()).ToArray();
        List<string> stringList = new List<string>();
        foreach (var index in indexes)
        {   
            if (cardController.GetCardType(index) != "Reversal" && cardController.GetCardFortitude(cardController.GetCardType(index)) <= controllerCurrentPlayer.FortitudRating())
                stringList.Add(cardController.GetStringPlayedInfo(index));
        }

        return stringList;
    }
    
    
}