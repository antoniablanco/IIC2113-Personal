using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Maneuver;

public class StandingSideHeadlock : Card
{
    public StandingSideHeadlock(string title, List<string> types, List<string> subtypes, string fortitude,
        string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        const int numberOfCardsToSteal = 1;
        gameStructureInfo.Effects.StealCards(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo.GetOpponentPlayer());
    }
}