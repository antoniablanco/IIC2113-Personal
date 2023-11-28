using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Maneuver;

public class PressSlam : Card
{
    public PressSlam(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        new CollateralDamageEffectUtils(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo);

        const int numberOfCardToDiscard = 2;
        new HandToRingSideDiscardEffect(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo.ControllerOpponentPlayer, numberOfCardToDiscard, gameStructureInfo);
    }
}