using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Maneuver;

public class Bulldog : Card
{
    public Bulldog(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        const int numberOfCardToDiscardCurrentPlayer = 1;
        new DiscardCardsFromHandToRingSideEffect(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardToDiscardCurrentPlayer, gameStructureInfo);

        const int numberOfCardToDiscardOpponentPlayer = 1;
        new DiscardCardsFromHandToRingSideEffect(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardToDiscardOpponentPlayer, gameStructureInfo);
    }
}