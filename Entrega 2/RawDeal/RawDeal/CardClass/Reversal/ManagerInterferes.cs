using RawDeal.PlayerClass;

namespace RawDeal.CardClass.Reversal;

public class ManagerInterferes: Card
{
    public ManagerInterferes(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, string typePlayed)
    {
        return playedCardController.VerifyIfPlayThisType("Maneuver");
    }
    
    public override void ReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        gameStructureInfo.CardEffects.StealCard(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.GetOpponentPlayer());
        PlayerController damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        gameStructureInfo.CardEffects.ProduceDamage(int.Parse(Damage), damagedPlayerController,gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.CardEffects.EndTurn();
    }
}