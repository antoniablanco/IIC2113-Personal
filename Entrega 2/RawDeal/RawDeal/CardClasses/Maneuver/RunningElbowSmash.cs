using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Maneuver;

public class RunningElbowSmash : Card
{
    public RunningElbowSmash(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        new ColateralDamageEffectUtils(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetCurrentPlayer(), 
            gameStructureInfo);
    }
}