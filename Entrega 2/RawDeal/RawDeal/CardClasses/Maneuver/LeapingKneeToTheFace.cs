using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class LeapingKneeToTheFace: Card
{
    public LeapingKneeToTheFace(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CheckIfCardCanBeReverted()
    {
        return false;
    }
    
    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        const int numberOfCardToDiscard = 1;
        new DiscardCardsFromHandToRingSideEffect(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo.ControllerOpponentPlayer, numberOfCardToDiscard, gameStructureInfo);
    }
    
    public override bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo)
    {
        return gameStructureInfo.CardBeingPlayed.GetCardTitle() == "Irish Whip" &&
               gameStructureInfo.BonusManager.GetWhoActivateNextPlayedCardBonusEffect() ==
               gameStructureInfo.ControllerCurrentPlayer && gameStructureInfo.BonusManager.GetTurnCounterForBonus() > 0;
    }
}