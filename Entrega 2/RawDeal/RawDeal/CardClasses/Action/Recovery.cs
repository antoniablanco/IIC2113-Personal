using RawDeal.GameClasses;

namespace RawDeal.CardClass.Action;

public class Recovery: Card
{
    public Recovery(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        const int numberOfDamageToRecover = 2;
        gameStructureInfo.Effects.GetBackDamage(gameStructureInfo.ControllerCurrentPlayer,gameStructureInfo.GetCurrentPlayer(), numberOfDamageToRecover);
        
        const int numberOfCardsToSteal = 1;
        gameStructureInfo.Effects.StealCards(gameStructureInfo.ControllerCurrentPlayer,gameStructureInfo.GetCurrentPlayer(), numberOfCardsToSteal);
        
        gameStructureInfo.Effects.DiscardActionCardToRingAreButNotSaying(playedCardController, gameStructureInfo.GetCurrentPlayer());
    }
}