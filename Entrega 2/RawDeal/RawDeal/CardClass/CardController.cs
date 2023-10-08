using RawDeal.PlayerClass;
using RawDealView;

namespace RawDeal.CardClass;


public class CardController
{
    private Card _card;
    private View _view;

    public CardController(Card card, View view)
    {
        _card = card;
        _view = view;
    }
    
    public bool ContainsUniqueSubtype()
    {
        return _card.Subtypes.Any(t => t == "Unique");
    }
    
    public bool ContainsSetUpSubtype()
    {
        return _card.Subtypes.Any(t => t == "SetUp");
    }
    
    public bool ContainsHeelSubtype()
    {
        return _card.Subtypes.Any(t => t == "Heel");
    }
    
    public bool ContainsFaceSubtype()
    {
        return _card.Subtypes.Any(t => t == "Face");
    }

    public bool ContainsSuperStarLogo(string superStarLogo)
    {
        return _card.Subtypes.Any(t => t.Contains(superStarLogo));
    }

    public bool IsReversalType()
    {
        return _card.Types[0]=="Reversal";
    }
    
    public int GetDamageProducedByTheCard()
    {
        return int.Parse(_card.Damage);
    }
    
    public string GetCardTitle()
    {
        return _card.Title;
    }
    
    public int GetCardFortitude()
    {
        return int.Parse(_card.Fortitude);
    }
    
    public CardInfoImplementation CreateIViewableCardInfo()
    {
        var cardInfo = new CardInfoImplementation(
            _card.Title,
            _card.Fortitude,
            _card.Damage,
            _card.StunValue,
            _card.Types,
            _card.Subtypes,
            _card.CardEffect);
        return cardInfo;
    }
    
    public PlayInfoImplementation CreateIViewablePlayedInfo()
    {   
        var cardInfo = new PlayInfoImplementation(
            _card.Title,
            _card.Fortitude,
            _card.Damage,
            _card.StunValue,
            _card.Types,
            _card.Subtypes,
            _card.CardEffect);
        
        return cardInfo;
    }

    public void PlayManeuverCard()
    {
        
    }
    
}
