using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class Facebuster: Card
{
    public Facebuster(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totalDamage)
    {
        return gameStructureInfo.LastCardBeingPlayedTitle == "Irish Whip" 
               && reverseBy == "Hand" 
               && playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver") &&
               gameStructureInfo.BonusManager.CanReversal(gameStructureInfo, reverseBy, totalDamage);;
    }

    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {  
        const int maximumNumberOfCardsToSteal = 2;
        new CardDrawEffect(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo).MayStealCards(maximumNumberOfCardsToSteal);
    }
}