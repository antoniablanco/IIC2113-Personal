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
    
    // CASO BORDE A REVISAR, QUE PASA SI LA ULTIMA CARTA DEL JUGADOR a ES irish whip Y LA PRIMERA DEL JUGADOR B LA NECESITA
    public override bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo)
    {
        return gameStructureInfo.LastPlayedCard.GetCardTitle() == "Irish Whip";
    }
}