using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class BackBodyDrop: Card
{
    public BackBodyDrop(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        const int maximumNumberOfCardsToSteal = 2;
        const int maximumNumberOfCardsToDiscard = 2;
        const bool shouldAsk = true;
        new DrawOrForceToDiscardEffect(gameStructureInfo, maximumNumberOfCardsToSteal, 
            maximumNumberOfCardsToDiscard, shouldAsk);
    }
    
    public override bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo)
    {
        return gameStructureInfo.LastPlayedCard.GetCardTitle() == "Irish Whip";
    }
}