using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class DiscardCardsFromHandToRingSideEffect: EffectsUtils
{
    private PlayerController opponentPlayerController;
    private PlayerController currentPlayerController;
    private int cardsToDiscardCount;
    private List<string> handFormatoString;
    
    public DiscardCardsFromHandToRingSideEffect(PlayerController opponentPlayerController,
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
                .HandCardsButNotTheCardIsBeingPlayed(gameStructureInfo.LastPlayedCard).Item1;

            if (IsPositive(handFormatoString.Count()))
                DiscardACardOfMyChoiceFromHandNotNotifying(cardsToDiscardCount - currentDamage);
        }
    }
    
    private void DiscardACardOfMyChoiceFromHandNotNotifying(int cardsRemainingToDiscard)
    {
        var selectedCard = gameStructureInfo.View.AskPlayerToSelectACardToDiscard(handFormatoString,
            opponentPlayerController.NameOfSuperStar(), currentPlayerController.NameOfSuperStar(), cardsRemainingToDiscard);

        if (gameStructureInfo.PlayCard.HasSelectedAValidCard(selectedCard))
        {
            var discardCardController = opponentPlayerController
                .HandCardsButNotTheCardIsBeingPlayed(gameStructureInfo.LastPlayedCard).Item2[selectedCard];
            var playerWhoDiscardCard = opponentPlayerController == gameStructureInfo.ControllerCurrentPlayer
                ? gameStructureInfo.GetCurrentPlayer()
                : gameStructureInfo.GetOpponentPlayer();
            gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(playerWhoDiscardCard,
                discardCardController);
        }
    }
}