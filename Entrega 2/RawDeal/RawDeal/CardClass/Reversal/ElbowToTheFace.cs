using RawDeal.GameClasses;
using RawDeal.PlayerClass;

namespace RawDeal.CardClass.Reversal;

public class ElbowToTheFace: Card
{
    public ElbowToTheFace(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, string typePlayed)
    {
        return playedCardController.VerifyIfTheCardIsOfThisType("Maneuver") && playedCardController.DealsTheMaximumDamage(7);
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