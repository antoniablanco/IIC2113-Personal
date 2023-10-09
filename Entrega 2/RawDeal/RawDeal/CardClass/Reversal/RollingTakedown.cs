using RawDeal.PlayerClass;

namespace RawDeal.CardClass.Reversal;

public class RollingTakedown: Card
{
    public RollingTakedown(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, string typePlayed)
    {
        return playedCardController.VerifyIfContainSubtype("Grapple") && playedCardController.DealsTheMaximumDamage(7);
    }
    
    public override void ReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        PlayerController damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        int damage = gameStructureInfo.CardEffects.GetDamageProducedByReversalCardWithNotEspecificDamage(damagedPlayerController);
        gameStructureInfo.CardEffects.Damage(damage, damagedPlayerController,gameStructureInfo.GetCurrentPlayer()); 
        gameStructureInfo.CardEffects.EndTurn();
    }
}