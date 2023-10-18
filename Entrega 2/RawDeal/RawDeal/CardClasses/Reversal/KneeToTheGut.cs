using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Reversal;

public class KneeToTheGut : Card
{
    public KneeToTheGut(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override bool CanReversalThisCard(CardController playedCardController)
    {
        const int maximumDamageProducedByPlayedCard = 7;
        return playedCardController.ContainsSubtype("Strike") &&
               playedCardController.DealsTheMaximumDamage(maximumDamageProducedByPlayedCard) &&
               playedCardController.HasAnyTypeDifferentOfReversal();
    }

    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        var damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        var damage = gameStructureInfo.Effects.GetDamageProducedByReversalCardWithNotEspecificDamage();
        gameStructureInfo.DamageEffects.ProduceSeveralDamage(damage, damagedPlayerController,
            gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.Effects.EndTurn();
    }
}