using RawDeal.CardClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class EffectsUtils
{
    protected readonly GameStructureInfo gameStructureInfo = new();
    
    public EffectsUtils(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }


    public void DiscardCardsFromHandToRingSide(PlayerController opponentPlayerController,
        PlayerController currentPlayerController, int cardsToDiscardCount)
    {
        for (var currentDamage = 0; currentDamage < cardsToDiscardCount; currentDamage++)
        {
            var handFormatoString = opponentPlayerController
                .HandCardsButNotTheCardIsBeingPlayed(gameStructureInfo.LastPlayedCard).Item1;

            if (HasCardsToDiscard(handFormatoString.Count()))
                DiscardACardOfMyChoiceFromHandNotNotifying(opponentPlayerController, currentPlayerController,
                    cardsToDiscardCount - currentDamage, handFormatoString);
        }
    }

    private void DiscardACardOfMyChoiceFromHandNotNotifying(PlayerController opponentPlayerController,
        PlayerController currentPlayerController, int cardsToDiscardCount, List<string> handFormatoString)
    {
        var selectedCard = gameStructureInfo.View.AskPlayerToSelectACardToDiscard(handFormatoString,
            opponentPlayerController.NameOfSuperStar(), currentPlayerController.NameOfSuperStar(), cardsToDiscardCount);

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

    public void DiscardCardFromHandNotifying(CardController playedCardController,
        PlayerController controllerCurrentPlayer, Player player)
    {
        gameStructureInfo.View.SayThatPlayerMustDiscardThisCard(controllerCurrentPlayer.NameOfSuperStar(),
            playedCardController.GetCardTitle());
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(player, playedCardController);
    }

    public void DiscardActionCardToRingAreButNotSaying(CardController playedCardController, Player player)
    {
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(player, playedCardController);
    }

    public void DiscardingCardsFromHandToArsenal(PlayerController playerController)
    {
        var handCardsAsString = gameStructureInfo.ControllerCurrentPlayer.StringCardsFrom("Hand");
        var selectedCard =
            gameStructureInfo.View.AskPlayerToReturnOneCardFromHisHandToHisArsenal(playerController.NameOfSuperStar(),
                handCardsAsString);

        var discardedCardController =
            gameStructureInfo.ControllerCurrentPlayer.GetSpecificCardFrom("Hand", selectedCard);

        var player = gameStructureInfo.GetCurrentPlayer();
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToArsenal(player, discardedCardController, "Start");
    }
    
    public bool IsTheSuperStarMankind(PlayerController playerController)
    {
        return playerController.NameOfSuperStar() == "MANKIND";
    }

    public int GetDamageProducedByReversalCardWithNotEspecificDamage()
    {
        var totalDamage = gameStructureInfo.LastPlayedCard.GetDamageProducedByTheCard() +
                          gameStructureInfo.BonusManager.AddBonus("JockeyingDamage");
        if (IsTheSuperStarMankind(gameStructureInfo.ControllerOpponentPlayer) ||
            IsTheSuperStarMankind(gameStructureInfo.ControllerCurrentPlayer))
            totalDamage -= 1;
        return totalDamage;
    }
    
    private bool HasCardsToDiscard(int numberofCards)
    {
        return numberofCards > 0;
    }
    
    protected bool HasDamageToApply(int totalDamage)
    {
        return totalDamage > 0;
    }
    
    protected bool CheckIfThePlayerHasCardInArsenal(PlayerController controllerPlayer)
    {
        return controllerPlayer.HasCardsInArsenal();
    }
    
    public void EndTurn()
    {
        gameStructureInfo.EndTurnManager.UpdateVariablesAtEndOfTurn();
    }
    
    
}