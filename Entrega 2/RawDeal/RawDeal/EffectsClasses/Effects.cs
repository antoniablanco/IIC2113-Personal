using RawDeal.CardClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class Effects
{
    private readonly GameStructureInfo gameStructureInfo = new();

    public Effects(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }

    public void MayStealCards(PlayerController controllerCurrentPlayer, Player player, int maximumNumberOfcardToDraw)
    {
        if (CheckIfThePlayerCanStealCards(controllerCurrentPlayer))
        {
            var numberOfcardToDraw = gameStructureInfo.View.AskHowManyCardsToDrawBecauseOfACardEffect(
                controllerCurrentPlayer.NameOfSuperStar(), maximumNumberOfcardToDraw);
            if (controllerCurrentPlayer.NumberOfCardIn("Arsenal") < numberOfcardToDraw)
                numberOfcardToDraw = controllerCurrentPlayer.NumberOfCardIn("Arsenal");
            StealCards(controllerCurrentPlayer, player, numberOfcardToDraw);
        }
    }

    public void StealCards(PlayerController controllerCurrentPlayer, Player player, int numberOfcardToDraw = 1)
    {
        gameStructureInfo.View.SayThatPlayerDrawCards(controllerCurrentPlayer.NameOfSuperStar(), numberOfcardToDraw);
        for (var i = 0; i < numberOfcardToDraw; i++)
            gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToHand(player);
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

    public bool IsTheCardWeAreReversalOfMankindSuperStart(PlayerController playerController)
    {
        return IsTheSuperStarMankind(playerController);
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

    private bool IsTheSuperStarMankind(PlayerController playerController)
    {
        return playerController.NameOfSuperStar() == "MANKIND";
    }
    
    private bool HasCardsToDiscard(int numberofCards)
    {
        return numberofCards > 0;
    }
    
    private bool CheckIfThePlayerCanStealCards(PlayerController controllerPlayer)
    {
        return controllerPlayer.HasCardsInArsenal();
    }
    
    public void AddingChoosingCardFromRingSideToHand(PlayerController player)
    {
        var ringSideAsString = player.StringCardsFrom("RingSide");
        var selectedCard =
            gameStructureInfo.View.AskPlayerToSelectCardsToPutInHisHand(player.NameOfSuperStar(), 1, ringSideAsString);

        var addedCardController = player.GetSpecificCardFrom("RingSide", selectedCard);
        var playerWhoDiscardCard = player == gameStructureInfo.ControllerCurrentPlayer
            ? gameStructureInfo.GetCurrentPlayer()
            : gameStructureInfo.GetOpponentPlayer();
        gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToHand(playerWhoDiscardCard, addedCardController);
    }

    public void AddingChoosingCardFromRingSideToArsenal(PlayerController playerController)
    {
        var ringAreaAsString = gameStructureInfo.ControllerCurrentPlayer.StringCardsFrom("RingSide");
        var selectedCardIndex =
            gameStructureInfo.View.AskPlayerToSelectCardsToRecover(playerController.NameOfSuperStar(), 1,
                ringAreaAsString);
        var discardedCardController =
            gameStructureInfo.ControllerCurrentPlayer.GetSpecificCardFrom("RingSide", selectedCardIndex);
        gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToArsenal(gameStructureInfo.GetCurrentPlayer(),
            discardedCardController, "Start");
    }

    public void EndTurn()
    {
        gameStructureInfo.EndTurnManager.UpdateVariablesAtEndOfTurn();
    }
}