using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Maneuver;

public class Reverse : Card
{
    public Reverse(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        const int maximumNumberOfCardsToSteal = 2;
        new DrawCardEffect(gameStructureInfo.ControllerCurrentPlayer,gameStructureInfo.GetCurrentPlayer(), 
            gameStructureInfo).MayStealCards(maximumNumberOfCardsToSteal);
    }
}