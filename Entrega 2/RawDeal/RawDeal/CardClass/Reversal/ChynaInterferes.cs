namespace RawDeal.CardClass.Reversal;

public class ChynaInterferes: Card
{
    public ChynaInterferes(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
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
        gameStructureInfo.CardEffects.StealCard(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.GetOpponentPlayer(), 2);
        gameStructureInfo.CardEffects.EndTurn();
    }
}