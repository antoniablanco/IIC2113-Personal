using RawDeal.PlayerClass;
using RawDealView;

namespace RawDeal.CardClass;


public class CardController
{
    private Card _card;
    private GameStructureInfo gameStructureInfo;

    public CardController(Card card, GameStructureInfo gameStructureInfo)
    {
        _card = card;
        this.gameStructureInfo = gameStructureInfo;
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
        return _card.Types.Any(t => t.Contains("Reversal"));
    }
    
    public bool HasAnyTypeDifferentOfReversal()
    {
        return _card.Types.Any(t => !t.Contains("Reversal"));
    }
    
    public int GetDamageProducedByTheCard()
    {
        return int.Parse(_card.Damage);
    }
    
    public string GetCardTitle()
    {
        return _card.Title;
    }
    
    public List<string> GetCardTypes()
    {
        return _card.Types;
    }

    public string GetCardType(int index)
    {
        return _card.Types[index];
    }
    
    public int GetCardFortitude()
    {
        return int.Parse(_card.Fortitude);
    }
    
    public int GetCardStunValue()
    {
        return int.Parse(_card.StunValue);
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
    
    public PlayInfoImplementation CreateIViewablePlayedInfo(int playedAs)
    {   
        var cardInfo = new PlayInfoImplementation(
            _card.Title,
            _card.Fortitude,
            _card.Damage,
            _card.StunValue,
            _card.Types,
            _card.Subtypes,
            _card.CardEffect,
            _card.Types[playedAs].ToUpper()  
            );
        
        return cardInfo;
    }

    public bool GetIfCardCanReversalPlayedCard()
    {
        return _card.CanReversalThisCard(gameStructureInfo.LastPlayedCard, gameStructureInfo.LastPlayedType);
    }

    public bool CanUseThisReversalCard(PlayerController controllerPlayer)
    {
        return ((GetCardFortitude() + gameStructureInfo.bonusFortitude*gameStructureInfo.IsJockeyingForPositionBonusFortitud <= controllerPlayer.FortitudRating()) && IsReversalType() && GetIfCardCanReversalPlayedCard());
    }
    
    public void ReversalEffect()
    {
        _card.ReversalEffect(gameStructureInfo);
    }
    
    public bool VerifyIfContainSubtype(string subType)
    {
        return _card.Subtypes.Contains(subType);
    }

    public bool VerifyIfPlayThisType(string type)
    {
        return gameStructureInfo.LastPlayedType == type;
    }

    public bool TheCardHadStunValue()
    {
        return int.Parse(_card.StunValue) > 0;
    }
    
    public bool DealsTheMaximumDamage(int maximumDamage)
    {
        return int.Parse(_card.Damage) + gameStructureInfo.bonusDamage*gameStructureInfo.IsJockeyingForPositionBonusDamage <= maximumDamage;
    }
    
    public void ActionEffect()
    {
        _card.ActionEffect(gameStructureInfo, this);
    }
}
