using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class IrishWhip: Card
{
    public IrishWhip(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }

    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo,
        string reverseBy)
    {
        return playedCardController.GetCardTitle() == "Irish Whip";
    }
    
    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        ApplyEffect(gameStructureInfo, gameStructureInfo.ControllerOpponentPlayer);
        gameStructureInfo.EffectsUtils.EndTurn();
    }

    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        ApplyEffect(gameStructureInfo, gameStructureInfo.ControllerCurrentPlayer);
        gameStructureInfo.EffectsUtils.DiscardActionCardToRingAreButNotSaying(playedCardController,
            gameStructureInfo.GetCurrentPlayer());
    }
    
    private void ApplyEffect(GameStructureInfo gameStructureInfo, PlayerController playerController)
    {   
        gameStructureInfo.BonusManager.ApplyNextPlayedCardBonusEffect("IrishWhip", bonusValue:5, "Damage");
        gameStructureInfo.BonusManager.SetWhoActivateNextPlayedCardBonusEffect(playerController);
        var turnsBeforeEffectExpires = 2;
        gameStructureInfo.BonusManager.SetTurnsLeftForBonusCounter(turnsBeforeEffectExpires);
    }
}