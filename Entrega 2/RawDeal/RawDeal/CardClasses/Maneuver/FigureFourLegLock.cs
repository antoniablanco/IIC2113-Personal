using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Maneuver;

public class FigureFourLegLock : Card
{
    public FigureFourLegLock(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        const int numberOfCardToDiscard = 1;
        gameStructureInfo.EffectsUtils.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo.ControllerOpponentPlayer, numberOfCardToDiscard);
    }
}