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
    
    public override bool CanReversalThisCard(CardController playedCardController)
    {
        return playedCardController.GetCardTitle() == "Jockeying for Position";
    }
    
    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        ApplyEffect(gameStructureInfo, gameStructureInfo.ControllerOpponentPlayer);
        gameStructureInfo.Effects.EndTurn();
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        ApplyEffect(gameStructureInfo, gameStructureInfo.ControllerCurrentPlayer);
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetCurrentPlayer(), playedCardController);
    }

    private void ApplyEffect(GameStructureInfo gameStructureInfo, PlayerController playerController)
    {
        GetSelectedEffectChosenByPlayer(gameStructureInfo, playerController.NameOfSuperStar());
        gameStructureInfo.WhoActivateJockeyingForPosition = playerController;
        int turnsBeforeEffectExpires = 2;
        gameStructureInfo.TurnCounterForJokeyingForPosition = turnsBeforeEffectExpires;
    }


    private void GetSelectedEffectChosenByPlayer(GameStructureInfo gameStructureInfo, string nameOfSuperStar)
    {   
        SelectedEffect effectToPerform = gameStructureInfo.View.AskUserToSelectAnEffectForJockeyForPosition(nameOfSuperStar);
        switch (effectToPerform)
        {
            case SelectedEffect.NextGrappleIsPlus4D:
                gameStructureInfo.IsJockeyingForPositionBonusDamageActive = 1;
                break;
            case SelectedEffect.NextGrapplesReversalIsPlus8F:
                gameStructureInfo.IsJockeyingForPositionBonusFortitudActive = 1;
                break;
        }
    }
    
}