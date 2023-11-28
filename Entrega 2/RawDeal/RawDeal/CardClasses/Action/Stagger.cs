using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class Stagger: Card
{
    public Stagger(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetCurrentPlayer(),
            playedCardController);
        
        gameStructureInfo.BonusManager.ApplyNextPlayedCardBonusEffect(BonusEnum.CardBonusName.Stagger, bonusValue:0, 
            BonusEnum.CardBonusType.Reversal);
        
        gameStructureInfo.BonusManager.SetWhoActivateNextPlayedCardBonusEffect(gameStructureInfo.ControllerCurrentPlayer);
        var turnsBeforeEffectExpires = 2;
        gameStructureInfo.BonusManager.SetTurnsLeftForBonusCounter(turnsBeforeEffectExpires);
    }
    
    public override bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo, string type = "Maneuver")
    { 
        return gameStructureInfo.CardBeingPlayedType == "Maneuver" 
               && gameStructureInfo.GetSetGameVariables.GetRoundsInTurn() > 1;
    }
}