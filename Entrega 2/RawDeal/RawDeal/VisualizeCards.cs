using RawDealView.Formatters;

namespace RawDeal;

public class VisualizeCards
{
    private CardInfoImplementation CrearIViewableCardInfo(Card card)
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

    private PlayInfoImplementation CrearIViewablePlayedInfo(Card card)
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
        return GetStringInfo(card, CrearIViewableCardInfo, Formatter.CardToString);
    }

    public string GetStringPlayedInfo(Card card)
    {
        return GetStringInfo(card, CrearIViewablePlayedInfo, Formatter.PlayToString);
    }


    private List<string> CreateStringInfoList(List<Card> cardsInSelectedSet, Func<Card, string> getInfoFunction)
    {
        List<string> stringCartas = new List<string>();

        foreach (var card in cardsInSelectedSet)
        {   
            stringCartas.Add(getInfoFunction(card));
        }

        return stringCartas;
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