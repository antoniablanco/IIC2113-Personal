using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView.Options;

namespace RawDeal.CardClasses.Reversal;

public class JockeyingForPosition : Card
{
    public JockeyingForPosition(string title, List<string> types, List<string> subtypes, string fortitude,
        string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totalDamage)
    {
        return playedCardController.GetCardTitle() == "Jockeying for Position" &&
               gameStructureInfo.BonusManager.CanReversal(gameStructureInfo, reverseBy, totalDamage);;
    }

    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        ApplyEffect(gameStructureInfo, gameStructureInfo.ControllerOpponentPlayer);
    }

    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        ApplyEffect(gameStructureInfo, gameStructureInfo.ControllerCurrentPlayer);
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetCurrentPlayer(),
            playedCardController);
    }

    private void ApplyEffect(GameStructureInfo gameStructureInfo, PlayerController playerController)
    {
        GetSelectedEffectChosenByPlayer(gameStructureInfo, playerController.NameOfSuperStar());
        gameStructureInfo.BonusManager.SetWhoActivateNextPlayedCardBonusEffect(playerController);
        var turnsBeforeEffectExpires = 2;
        gameStructureInfo.BonusManager.SetTurnsLeftForBonusCounter(turnsBeforeEffectExpires);
    }


    private void GetSelectedEffectChosenByPlayer(GameStructureInfo gameStructureInfo, string nameOfSuperStar)
    {
        var effectToPerform = gameStructureInfo.View.AskUserToSelectAnEffectForJockeyForPosition(nameOfSuperStar);
        switch (effectToPerform)
        {
            case SelectedEffect.NextGrappleIsPlus4D:
                gameStructureInfo.BonusManager.ApplyNextPlayedCardBonusEffect(BonusEnum.CardBonusName.JockeyingDamage, bonusValue:4,
                    BonusEnum.CardBonusType.Damage);
                break;
            case SelectedEffect.NextGrapplesReversalIsPlus8F:
                gameStructureInfo.BonusManager.ApplyNextPlayedCardBonusEffect(BonusEnum.CardBonusName.JockeyingFortitud, bonusValue:8, 
                    BonusEnum.CardBonusType.Fortitud);
                break;
        }
    }
}