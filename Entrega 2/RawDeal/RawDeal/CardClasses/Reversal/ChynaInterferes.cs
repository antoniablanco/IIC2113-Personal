using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Reversal;

public class ChynaInterferes : Card
{
    public ChynaInterferes(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override bool CanReversalThisCard(CardController playedCardController)
    {
        return playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver");
    }

    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        const int numberOfCardsToSteal = 2;
        gameStructureInfo.Effects.StealCards(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo.GetOpponentPlayer(), numberOfCardsToSteal);

        var damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        var damageProduce =
            gameStructureInfo.PlayCard.ObtainDamageByCheckingIfTheCardBelongsToMankindSuperStar(int.Parse(Damage),
                damagedPlayerController);

        gameStructureInfo.DamageEffects.ProduceSeveralDamage(damageProduce, damagedPlayerController,
            gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.Effects.EndTurn();
    }
}