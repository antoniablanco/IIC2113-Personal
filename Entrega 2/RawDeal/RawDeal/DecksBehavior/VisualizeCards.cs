using RawDeal.CardClass;
using RawDeal.PlayerClass;
using RawDealView.Formatters;

namespace RawDeal.DecksBehavior;

public class VisualizeCards
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

    public List<string> CreateStringPlayedCardList(List<CardController> cardsInSelectedSet)
    {
        List<string> stringList = new List<string>();

        foreach (var cardController in cardsInSelectedSet)
        {
            stringList.AddRange(GetStringPlayedCardForType(cardController));
        }

        return stringList;
    }

    private List<String> GetStringPlayedCardForType(CardController cardController)
    {
        int[] indexes = Enumerable.Range(0, cardController.GetCardTypes().Count()).ToArray();
        return indexes.Select(index => GetStringPlayedInfo(cardController, index)).ToList();
    }
}