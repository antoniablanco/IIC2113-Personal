using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView.Options;

namespace RawDeal.CardClass.Reversal;

public class JockeyingForPosition: Card
{
    public JockeyingForPosition(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, string typePlayed)
    {
        return playedCardController.GetCardTitle() == "Jockeying for Position";
    }
    
    public override void ReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        Effect(gameStructureInfo, gameStructureInfo.ControllerOpponentPlayer);
        gameStructureInfo.Effects.EndTurn();
    }
    
    public override void ActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        Effect(gameStructureInfo, gameStructureInfo.ControllerCurrentPlayer);
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetCurrentPlayer(), playedCardController);
    }

    private void Effect(GameStructureInfo gameStructureInfo, PlayerController playerController)
    {
        GetSelectedEffectChosenByPlayer(gameStructureInfo, playerController.NameOfSuperStar());
        gameStructureInfo.HowActivateJockeyingForPosition = playerController;
        int turnsBeforeEffectExpires = 2;
        gameStructureInfo.TurnCounterForJokeyingForPosition = turnsBeforeEffectExpires;
    }


    private void GetSelectedEffectChosenByPlayer(GameStructureInfo gameStructureInfo, string nameOfSuperStar)
    {   
        SelectedEffect effectToPerform = gameStructureInfo.view.AskUserToSelectAnEffectForJockeyForPosition(nameOfSuperStar);
        switch (effectToPerform)
        {
            case SelectedEffect.NextGrappleIsPlus4D:
                gameStructureInfo.IsJockeyingForPositionBonusDamage = 1;
                break;
            case SelectedEffect.NextGrapplesReversalIsPlus8F:
                gameStructureInfo.IsJockeyingForPositionBonusFortitud= 1;
                break;
        }
    }
    
}