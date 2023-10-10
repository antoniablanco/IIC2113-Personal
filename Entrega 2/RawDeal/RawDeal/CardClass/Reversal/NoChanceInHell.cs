namespace RawDeal.CardClass.Reversal;

public class NoChanceInHell: Card
{
    public NoChanceInHell(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, string typePlayed)
    {
        return playedCardController.VerifyIfTheCardIsOfThisType("Action");
    }
    
    public override void ReversalEffect(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.CardEffects.EndTurn();
    }
}