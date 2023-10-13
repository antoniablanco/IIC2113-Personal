namespace RawDeal.CardClass.Action;

public class PuppiesPuppies: Card
{
    public PuppiesPuppies(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.CardEffects.GetBackDamage(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.GetCurrentPlayer(),5);
        gameStructureInfo.CardEffects.StealCards(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.GetCurrentPlayer(), 2);
        gameStructureInfo.CardEffects.DiscardActionCardToRingAreButNotSaying(playedCardController, gameStructureInfo.GetCurrentPlayer());
    }
}