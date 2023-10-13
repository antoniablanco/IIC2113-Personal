namespace RawDeal.CardClass.Action;

public class Recovery: Card
{
    public Recovery(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.CardEffects.GetBackDamage(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.GetCurrentPlayer(),2);
        gameStructureInfo.CardEffects.StealCards(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.GetCurrentPlayer(), 1);
        gameStructureInfo.CardEffects.DiscardActionCardToRingAreButNotSaying(playedCardController, gameStructureInfo.GetCurrentPlayer());
    }
}