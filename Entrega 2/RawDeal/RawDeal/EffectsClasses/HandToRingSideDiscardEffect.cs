using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class HandToRingSideDiscardEffect: EffectsUtils
{
    private PlayerController opponentPlayerController;
    private PlayerController currentPlayerController;
    private int cardsToDiscardCount;
    private List<string> handFormatoString;
    
    public HandToRingSideDiscardEffect(PlayerController opponentPlayerController,
        PlayerController currentPlayerController, int cardsToDiscardCount, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this.opponentPlayerController = opponentPlayerController;
        this.currentPlayerController = currentPlayerController;
        this.cardsToDiscardCount = cardsToDiscardCount;
        Apply();
    }

    private void Apply()
    {   
        for (var currentDamage = 0; currentDamage < cardsToDiscardCount; currentDamage++)
        {
            handFormatoString = opponentPlayerController
                .GetHandCardsButNotTheCardIsBeingPlayed(gameStructureInfo.CardBeingPlayed).Item1;

            if (IsPositive(handFormatoString.Count()))
                DiscardACardOfMyChoiceFromHandNotNotifying(cardsToDiscardCount - currentDamage);
        }
    }
    
    private void DiscardACardOfMyChoiceFromHandNotNotifying(int cardsRemainingToDiscard)
    {
        var selectedCard = gameStructureInfo.View.AskPlayerToSelectACardToDiscard(handFormatoString,
            opponentPlayerController.GetNameOfSuperStar(), currentPlayerController.GetNameOfSuperStar(), 
            cardsRemainingToDiscard);

        if (gameStructureInfo.CardPlay.HasSelectedAValidCard(selectedCard))
        {
            var discardCardController = opponentPlayerController
                .GetHandCardsButNotTheCardIsBeingPlayed(gameStructureInfo.CardBeingPlayed).Item2[selectedCard];
            var playerWhoDiscardCard = opponentPlayerController == gameStructureInfo.ControllerCurrentPlayer
                ? gameStructureInfo.GetCurrentPlayer()
                : gameStructureInfo.GetOpponentPlayer();
            gameStructureInfo.CardMovement.TransferSelectedCardFromHandToRingSide(playerWhoDiscardCard,
                discardCardController);
        }
    }
}