using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Reversal;

public class ManagerInterferes : Card
{
    public ManagerInterferes(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int damageBonusForSuccessfulManeuver = 0)
    {
        return playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver");
    }

    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        new DrawCardEffect(gameStructureInfo.ControllerOpponentPlayer,gameStructureInfo.GetOpponentPlayer(), 
            gameStructureInfo).StealCards();

        var damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        var damageProduce =
            gameStructureInfo.PlayCard.ObtainDamageByCheckingIfTheCardBelongsToMankindSuperStar(int.Parse(Damage),
                damagedPlayerController);
        
        new ProduceDamageEffectUtils(damageProduce, damagedPlayerController, gameStructureInfo.GetCurrentPlayer(),
            gameStructureInfo);
        gameStructureInfo.EffectsUtils.EndTurn();
    }
}