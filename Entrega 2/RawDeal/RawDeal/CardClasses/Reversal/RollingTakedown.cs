using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.CardClass.Reversal;

public class RollingTakedown: Card
{
    public RollingTakedown(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController)
    {
        const int maximumDamageProducedByPlayedCard = 7;
        return playedCardController.ContainsSubtype("Grapple") && playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver") && playedCardController.DealsTheMaximumDamage(maximumDamageProducedByPlayedCard);
    }
    
    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        PlayerController damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        int damage = gameStructureInfo.Effects.GetDamageProducedByReversalCardWithNotEspecificDamage();
        gameStructureInfo.Effects.ProduceDamage(damage, damagedPlayerController,gameStructureInfo.GetCurrentPlayer()); 
        gameStructureInfo.Effects.EndTurn();
    }
}