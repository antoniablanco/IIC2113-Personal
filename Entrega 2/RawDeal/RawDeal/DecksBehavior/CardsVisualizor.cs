using RawDeal.CardClass;
using RawDeal.PlayerClasses;
using RawDealView.Formatters;

namespace RawDeal.DecksBehavior;

public class CardsVisualizor
{
    public string GetStringCardInfo(CardController cardController)
    {   
        CardInfoImplementation cardInfoImplementation = cardController.CreateIViewableCardInfo();
        string formattedInfo = Formatter.CardToString(cardInfoImplementation);
        return formattedInfo;
    }

    public string GetStringPlayedInfo(CardController cardController, int numType = 0)
    {   
        PlayInfoImplementation playInfoImplementation = cardController.CreateIViewablePlayedInfo(numType);
        string formattedInfo = Formatter.PlayToString(playInfoImplementation);
        return formattedInfo;
    }
    
    public List<string> CreateStringCardList(List<CardController> cardsInSelectedSet)
    {
        return cardsInSelectedSet.Select(cardController => GetStringCardInfo(cardController)).ToList();
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
                stringList.Add(GetStringPlayedInfo(cardController, index));
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
                stringList.Add(GetStringPlayedInfo(cardController, index));
        }

        return stringList;
    }
    
    
}