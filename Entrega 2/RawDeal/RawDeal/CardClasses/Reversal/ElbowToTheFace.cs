using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.CardClass.Reversal;

public class ElbowToTheFace: Card
{
    public ElbowToTheFace(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController)
    {
        const int maximumDamageProducedByPlayedCard = 7;
        return playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver") && playedCardController.DealsTheMaximumDamage(maximumDamageProducedByPlayedCard);
    }
    
    public override void ReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        PlayerController damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        int damageProduce = int.Parse(Damage);
        if (gameStructureInfo.Effects.IsTheCardWeAreReversalOfMankindSuperStart(gameStructureInfo.ControllerCurrentPlayer))
            damageProduce -= 1;
        gameStructureInfo.Effects.ProduceDamage(damageProduce, damagedPlayerController,gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.Effects.EndTurn();
    }
}