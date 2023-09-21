using RawDealView.Formatters;

namespace RawDeal;

public class VisualizeCards
{
    private CardInfoImplementation CreateIViewableCardInfo(CardController cardController)
    {
        return cardController.CreateIViewableCardInfo();
    }

    private PlayInfoImplementation CreateIViewablePlayedInfo(CardController cardController)
    {
        return cardController.CreateIViewablePlayedInfo();
    }


    private string GetStringInfo<T>(CardController cardController, Func<CardController, T> createInfoFunc, Func<T, string> toStringFunc)
    {   
        T info = createInfoFunc(cardController);
        string formattedInfo = toStringFunc(info);
        return formattedInfo;
    }

    public string GetStringCardInfo(CardController cardController)
    {
        return GetStringInfo(cardController, CreateIViewableCardInfo, Formatter.CardToString);
    }

    public string GetStringPlayedInfo(CardController cardController)
    {
        return GetStringInfo(cardController, CreateIViewablePlayedInfo, Formatter.PlayToString);
    }


    private List<string> CreateStringInfoList(List<CardController> cardsInSelectedSet, Func<CardController, string> getInfoFunction)
    {
        return cardsInSelectedSet.Select(getInfoFunction).ToList();
    }

    public List<string> CreateStringCardList(List<CardController> cardsInSelectedSet)
    {
        return CreateStringInfoList(cardsInSelectedSet, GetStringCardInfo);
    }

    public List<string> CreateStringPlayedCardList(List<CardController> cardsInSelectedSet)
    {
        return CreateStringInfoList(cardsInSelectedSet, GetStringPlayedInfo);
    }
    
}