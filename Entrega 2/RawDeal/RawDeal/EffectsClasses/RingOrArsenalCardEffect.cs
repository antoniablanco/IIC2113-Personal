using RawDeal.CardClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView.Options;

namespace RawDeal.EffectsClasses;

public class RingOrArsenalCardEffect: EffectsUtils
{   
    private PlayerController playerController;
    private Player player;
    
    public RingOrArsenalCardEffect(PlayerController playerController, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this.playerController = playerController;
        
        player = gameStructureInfo.ControllerOpponentPlayer == playerController ? 
            gameStructureInfo.GetOpponentPlayer() : gameStructureInfo.GetCurrentPlayer();
        Apply();
    }

    private void Apply()
    {   
        string deck = GetSelectedEffect(playerController.GetNameOfSuperStar());
        List<String> optionCards = playerController.GetStringCardsFrom(deck);
        
        const int numberOfCardsToSelect = 1;
        int indexOfCard = gameStructureInfo.View.AskPlayerToSelectCardsToPutInHisHand(playerController.GetNameOfSuperStar(), 
            numberOfCardsToSelect, optionCards);
        
        var addedCardController = playerController.RetrieveCardFromDeckAtPosition(deck, indexOfCard);
        MoveCardsTo(deck, addedCardController);
    }
    
    private  string GetSelectedEffect(string nameOfSuperStar)
    {
        SelectedEffect effectToPerform = gameStructureInfo.View.AskUserToChooseBetweenTakingACardFromYourArsenalOrRingside( nameOfSuperStar);
        switch (effectToPerform)
        {
            case SelectedEffect.TakeCardFromArsenal:
                return "Arsenal";
            case SelectedEffect.TakeCardFromRingside:
                return "RingSide";
            default:
                return "Error";
        }
    }

    private void MoveCardsTo(string deck, CardController cardController)
    {
        switch (deck)
        {
            case "Arsenal":
                gameStructureInfo.CardMovement.TransferSelectedCardFromArsenalToHand(player, cardController);
                break;
            case "RingSide":
                gameStructureInfo.CardMovement.TransferSelectedCardFromRingSideToHand(player, cardController);
                break;
        }
    }
}