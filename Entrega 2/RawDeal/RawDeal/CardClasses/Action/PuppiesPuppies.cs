using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Action;

public class PuppiesPuppies: Card
{
    public PuppiesPuppies(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        const int numberOfDamageToRecover = 5;
        new GetBackDamageEffectUtils(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo, numberOfDamageToRecover);

        const int numberOfCardsToSteal = 2;
        
        new CardDrawEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo).StealCards(numberOfCardsToSteal);

        gameStructureInfo.EffectsUtils.DiscardActionCardToRingAreButNotSaying(playedCardController,
            gameStructureInfo.GetCurrentPlayer());
    }
}