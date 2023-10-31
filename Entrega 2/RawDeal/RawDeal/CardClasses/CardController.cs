using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView.Formatters;

namespace RawDeal.CardClasses;

public class CardController
{
    private readonly Card _card;
    private readonly GameStructureInfo gameStructureInfo;

    public CardController(Card card, GameStructureInfo gameStructureInfo)
    {
        _card = card;
        this.gameStructureInfo = gameStructureInfo;
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
        return _card.Damage == "#" ? 0 : int.Parse(_card.Damage);
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

    public bool ContainsSubtype(string subType)
    {
        return _card.Subtypes.Contains(subType);
    }

    public int GetIndexForType(string type)
    {
        var numType = 0;
        var indexes = Enumerable.Range(0, GetCardTypes().Count()).ToArray();
        foreach (var index in indexes)
            if (GetCardType(index) == type)
                numType = index;
        return numType;
    }

    public int GetCardFortitude(string type)
    {
        return _card.GetFortitude(type);
    }

    public int GetCardStunValue()
    {
        return int.Parse(_card.StunValue);
    }

    public bool TheCardHadStunValue()
    {
        return int.Parse(_card.StunValue) > 0;
    }

    public string GetStringCardInfo()
    {
        return Formatter.CardToString(_card);
    }

    public string GetStringPlayedInfo(int playedAs)
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

        return Formatter.PlayToString(cardInfo);
    }

    public bool CanThisCardBePlayed()
    {   
        return _card.CheckIfCardCanBePlayed(gameStructureInfo);
    }

    public bool GetIfCardCanReversalPlayedCard(string reverseBy)
    {
        return _card.CanReversalThisCard(gameStructureInfo.CardBeingPlayed, gameStructureInfo, reverseBy) &&
               gameStructureInfo.CardBeingPlayed.CanThisCardBeReversal();
    }

    private bool CanThisCardBeReversal()
    {   
        return _card.CheckIfCardCanBeReverted();
    }

    public bool CanUseThisReversalCard(PlayerController controllerPlayer, string reverseBy)
    {
        return GetCardFortitude(GetCardTypes()[0]) + gameStructureInfo.BonusManager.GetFortitudBonus() <=
            controllerPlayer.FortitudRating() && IsReversalType() && GetIfCardCanReversalPlayedCard(reverseBy);
    }

    public bool VerifyIfTheLastPlayedTypeIs(string type)
    {
        return gameStructureInfo.CardBeingPlayedType == type;
    }

    public bool DealsTheMaximumDamage(int maximumDamage)
    {
        var damage = GetDamageProducedByTheCard() + gameStructureInfo.BonusManager.GetNexPlayCardDamageBonus() + gameStructureInfo.BonusManager.GetTurnDamageBonus(this);
        var totalDamage =
            gameStructureInfo.PlayCard.ObtainDamageByCheckingIfTheCardBelongsToMankindSuperStar(damage,
                gameStructureInfo.ControllerOpponentPlayer);

        return totalDamage <= maximumDamage;
    }

    public void ApplyReversalEffect()
    {
        _card.ApplyReversalEffect(gameStructureInfo);
    }

    public void ApplyActionEffect()
    {
        _card.ApplyActionEffect(gameStructureInfo, this);
    }

    public void ApplyManeuverEffect(CardController playedCardController)
    {
        _card.ApplyManeuverEffect(gameStructureInfo, playedCardController);
    }
}