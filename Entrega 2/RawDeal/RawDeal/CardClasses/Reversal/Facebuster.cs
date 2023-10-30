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
    
    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, string reverseBy)
    {
        return gameStructureInfo.LastPlayedCard.GetCardTitle() == "Irish Whip" && reverseBy == "Hand";
    }

    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        const int maximumNumberOfCardsToSteal = 2;
        new DrawCardEffect(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetOpponentPlayer(),
            gameStructureInfo).MayStealCards(maximumNumberOfCardsToSteal);
        gameStructureInfo.EffectsUtils.EndTurn();
    }
}