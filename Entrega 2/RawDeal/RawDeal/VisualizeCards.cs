using RawDealView.Formatters;

namespace RawDeal;

public class VisualizeCards
{
    private CardInfoImplementation CreateIViewableCardInfo(Card card)
    {
        var cardInfo = new CardInfoImplementation(
            card.Title,
            card.Fortitude,
            card.Damage,
            card.StunValue,
            card.Types,
            card.Subtypes,
            card.CardEffect);
        return cardInfo;
    }

    private PlayInfoImplementation CreateIViewablePlayedInfo(Card card)
    {   
        var cardInfo = new PlayInfoImplementation(
            card.Title,
            card.Fortitude,
            card.Damage,
            card.StunValue,
            card.Types,
            card.Subtypes,
            card.CardEffect);
        
        return cardInfo;
    }


    private string GetStringInfo<T>(Card card, Func<Card, T> createInfoFunc, Func<T, string> toStringFunc)
    {   
        T info = createInfoFunc(card);
        string formattedInfo = toStringFunc(info);
        return formattedInfo;
    }

    public string GetStringCardInfo(Card card)
    {
        return GetStringInfo(card, CreateIViewableCardInfo, Formatter.CardToString);
    }

    public string GetStringPlayedInfo(Card card)
    {
        return GetStringInfo(card, CreateIViewablePlayedInfo, Formatter.PlayToString);
    }


    private List<string> CreateStringInfoList(List<Card> cardsInSelectedSet, Func<Card, string> getInfoFunction)
    {
        return cardsInSelectedSet.Select(getInfoFunction).ToList();
    }

    public List<string> CreateStringCardList(List<Card> cardsInSelectedSet)
    {
        return CreateStringInfoList(cardsInSelectedSet, GetStringCardInfo);
    }

    public List<string> CreateStringPlayedCardList(List<Card> cardsInSelectedSet)
    {
        return CreateStringInfoList(cardsInSelectedSet, GetStringPlayedInfo);
    }
    
}