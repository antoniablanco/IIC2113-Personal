using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Reversal;

public class RollingTakedown : Card
{
    public RollingTakedown(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override bool CanReversalThisCard(CardController playedCardController)
    {
        const int maximumDamageProducedByPlayedCard = 7;
        return playedCardController.ContainsSubtype("Grapple") &&
               playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver") &&
               playedCardController.DealsTheMaximumDamage(maximumDamageProducedByPlayedCard);
    }

    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        var damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        var damage = gameStructureInfo.EffectsUtils.GetDamageProducedByReversalCardWithNotEspecificDamage();
        
        new ProduceDamageEffectUtils(damage, damagedPlayerController, gameStructureInfo.GetCurrentPlayer(),
            gameStructureInfo);
        gameStructureInfo.EffectsUtils.EndTurn();
    }
}