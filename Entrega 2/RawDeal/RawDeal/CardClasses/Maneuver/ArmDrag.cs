using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Maneuver;

public class ArmDrag : Card
{
    public ArmDrag(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        const int numberOfCardToDiscard = 1;
        new HandToRingSideDiscardEffect(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardToDiscard, gameStructureInfo);
    }
}