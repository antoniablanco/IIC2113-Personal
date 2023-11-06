using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Reversal;

public class KneeToTheGut : Card
{
    public KneeToTheGut(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totaldamage)
    {
        const int maximumDamageProducedByPlayedCard = 7;
        return playedCardController.ContainsSubtype("Strike") &&
               playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver") &&
               totaldamage <= maximumDamageProducedByPlayedCard &&
               playedCardController.HasAnyTypeDifferentOfReversal();
               
    }

    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        var damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        var damage = gameStructureInfo.EffectsUtils.GetDamageProducedByReversalCardWithNotEspecificDamage();
        
        new ProduceDamageEffectUtils(damage, damagedPlayerController,
            gameStructureInfo);
        gameStructureInfo.EffectsUtils.EndTurn();
    }
}