using RawDeal.CardClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView.Options;

namespace RawDeal.EffectsClasses;

public class ChooseRingSideOrArsenalToSelectCardEffect: EffectsUtils
{   
    private PlayerController playerController;
    private Player player;
    
    public ChooseRingSideOrArsenalToSelectCardEffect(PlayerController playerController, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this.playerController = playerController;
        
        player = gameStructureInfo.ControllerOpponentPlayer == playerController ? 
            gameStructureInfo.GetOpponentPlayer() : gameStructureInfo.GetCurrentPlayer();
        Apply();
    }

    private void Apply()
    {   
        string deck = GetSelectedEffectChosenByPlayer(playerController.NameOfSuperStar());
        List<String> optionCards = playerController.StringCardsFrom(deck);
        
        const int numberOfCardsToSelect = 1;
        int indexOfCard = gameStructureInfo.View.AskPlayerToSelectCardsToPutInHisHand(playerController.NameOfSuperStar(), 
            numberOfCardsToSelect, optionCards);
        
        var addedCardController = playerController.GetSpecificCardFrom(deck, indexOfCard);
        MoveCardsTo(deck, addedCardController);
    }
    
    private  string GetSelectedEffectChosenByPlayer(string nameOfSuperStar)
    {
        SelectedEffect effectToPerform = gameStructureInfo.View.AskUserToChooseBetweenTakingACardFromYourArsenalOrRingside( nameOfSuperStar);
        switch (effectToPerform)
        {
            case SelectedEffect.TakeCardFromArsenal:
                return "Arsenal";
            case SelectedEffect.TakeCardFromRingside:
                return "Ringside";
            default:
                return "Error";
        }
    }

    private void MoveCardsTo(string deck, CardController cardController)
    {
        switch (deck)
        {
            case "Arsenal":
                gameStructureInfo.CardMovement.TransferChoosinCardFromArsenalToHand(player, cardController);
                break;
            case "Ringside":
                gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToHand(player, cardController);
                break;
        }
    }
}