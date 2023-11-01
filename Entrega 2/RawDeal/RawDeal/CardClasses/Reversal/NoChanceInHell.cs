using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Reversal;

public class NoChanceInHell : Card
{
    public NoChanceInHell(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totaldamage, int damageBonusForSuccessfulManeuver = 0)
    {
        return playedCardController.VerifyIfTheLastPlayedTypeIs("Action");
    }

    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.EffectsUtils.EndTurn();
    }
}