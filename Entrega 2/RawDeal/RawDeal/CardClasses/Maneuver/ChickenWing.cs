using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Maneuver;

public class ChickenWing : Card
{
    public ChickenWing(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        const int numberOfDamageToRecover = 2;
        gameStructureInfo.DamageEffects.GetBackDamage(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.GetCurrentPlayer(), numberOfDamageToRecover);
    }
}