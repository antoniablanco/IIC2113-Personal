namespace RawDeal.CardClass.Reversal;

public class KneeToTheGut: Card
{
    public KneeToTheGut(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, string typePlayed)
    {
        return playedCardController.VerifyIfContainSubtype("Strike");
    }
    
    public override void ReversalEffect(GameStructureInfo gameStructureInfo)
    {
        
    }
}