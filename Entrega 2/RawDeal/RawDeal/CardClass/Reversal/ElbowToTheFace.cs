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
        return playedCardController.VerifyIfPlayThisType("Maneuver") && playedCardController.DealsTheMaximumDamage(7);
    }
    
    public override void ReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        PlayerController damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        
        gameStructureInfo.CardEffects.ProduceDamage(int.Parse(Damage), damagedPlayerController,gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.CardEffects.EndTurn();
    }
}