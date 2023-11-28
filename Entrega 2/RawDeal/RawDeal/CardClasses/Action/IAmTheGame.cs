using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class IAmTheGame: Card
{
    public IAmTheGame(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        const int maximumNumberOfCardsToSteal = 2;
        const int maximumNumberOfCardsToDiscard = 2;
        new DrawOrForceToDiscardEffect(gameStructureInfo, maximumNumberOfCardsToSteal, 
            maximumNumberOfCardsToDiscard);

        gameStructureInfo.BonusManager.ApplyTurnBonusEffect(BonusEnum.CardBonusName.IAmTheGame, 3);
        
        gameStructureInfo.EffectsUtils.DiscardActionCardToRingAreButNotSaying(playedCardController,
            gameStructureInfo.GetCurrentPlayer());
    }
    
}