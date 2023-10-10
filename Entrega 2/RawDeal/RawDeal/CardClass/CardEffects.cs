using RawDeal.PlayerClass;

namespace RawDeal.CardClass;

public class CardEffects
{   
    public GameStructureInfo gameStructureInfo= new GameStructureInfo();
    
    public void StealCard(PlayerController controllerCurrentPlayer, Player player, int numberOfcardToDraw = 1)
    {
        if (controllerCurrentPlayer.HasCardsInArsenal())
        {
            gameStructureInfo.view.SayThatPlayerDrawCards(controllerCurrentPlayer.NameOfSuperStar(), numberOfcardToDraw);
            for (int i = 0; i < numberOfcardToDraw; i++)
            {
                gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToHand(player);
            }
        }
    }
    
    public void DiscardCardsFromHand(PlayerController playerController, int cardsToDiscardCount, Player player)
    {
        for (int courrentDamage = 0; courrentDamage < cardsToDiscardCount; courrentDamage++)
            DiscardingCardFromHandToRingSide(playerController, cardsToDiscardCount-courrentDamage, player);
    }

    private void DiscardingCardFromHandToRingSide(PlayerController playerController, int cardsToDiscardCount, Player player)
    {
        List<string> handFormatoString = playerController.StringCardsHand();
        int selectedCard =gameStructureInfo.view.AskPlayerToSelectACardToDiscard(handFormatoString, playerController.NameOfSuperStar(), playerController.NameOfSuperStar(), cardsToDiscardCount);
            
        if (selectedCard != -1)
        {
            CardController discardCardController = playerController.GetSpecificCardFromHand(selectedCard);
            gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(player, discardCardController);
        }
    }
    
    public void DiscardCard(CardController playedCardController, PlayerController controllerCurrentPlayer, Player player)
    {   
        gameStructureInfo.view.SayThatPlayerMustDiscardThisCard(controllerCurrentPlayer.NameOfSuperStar(), playedCardController.GetCardTitle());
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(player, playedCardController);
    }

    public void ProduceDamage(int totalDamage, PlayerController controllerOpponentPlayer, Player player)
    {
        if (totalDamage > 0)
        {
            gameStructureInfo.view.SayThatSuperstarWillTakeSomeDamage(controllerOpponentPlayer.NameOfSuperStar(), totalDamage);
            for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
            {   
                if (CheckCanReceiveDamage(controllerOpponentPlayer))
                    ShowOneFaceDownCard(currentDamage + 1, totalDamage, player, controllerOpponentPlayer);
                else
                    gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(controllerOpponentPlayer);
            }
        }
    }

    public bool IsTheCardWeAreReversalMankindType(PlayerController playerController)
    {
        return playerController.IsTheSuperStarMankind();
    }
    
    private bool CheckCanReceiveDamage(PlayerController controllerOpponentPlayer)
    {
        return controllerOpponentPlayer.AreThereCardsLeftInTheArsenal();
    }

    private string ShowOneFaceDownCard(int currentDamage, int totalDamage, Player player, PlayerController controllerOpponentPlayer)
    {
        CardController flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
        string flippedCardString = gameStructureInfo.VisualizeCards.GetStringCardInfo(flippedCardController);
        gameStructureInfo.view.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
        return flippedCardString;
    }

    public int GetDamageProducedByReversalCardWithNotEspecificDamage()
    {
        int totalDamage = gameStructureInfo.LastPlayedCard.GetDamageProducedByTheCard() + gameStructureInfo.bonusDamage*gameStructureInfo.IsJockeyingForPositionBonusDamage;
        if (gameStructureInfo.ControllerOpponentPlayer.IsTheSuperStarMankind() || gameStructureInfo.ControllerCurrentPlayer.IsTheSuperStarMankind())
            totalDamage -= 1;
        return totalDamage;
    }
    
    public void DiscardCardsFromHandToRingSide(PlayerController playerController, int cardsToDiscardCount)
    {
        for (int courrentDamage = 0; courrentDamage < cardsToDiscardCount; courrentDamage++)
            DiscardOneCardOfMyChoiceFromHand(playerController, cardsToDiscardCount-courrentDamage);
    }
    
    public void AddingCardFromRingSideToHand(PlayerController player)
    {
        List<string> ringSideAsString = player.StringCardsRingSide();
        int selectedCard = gameStructureInfo.view.AskPlayerToSelectCardsToPutInHisHand(player.NameOfSuperStar(), 1, ringSideAsString);
        
        CardController addedCardController = player.GetSpecificCardFromRingSide(selectedCard);
        Player playerHowDiscardCard = (player == gameStructureInfo.ControllerCurrentPlayer)? gameStructureInfo.GetCurrentPlayer(): gameStructureInfo.GetOpponentPlayer();
        gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToHand(playerHowDiscardCard, addedCardController);
    }
    
    public void DiscardOneCardOfMyChoiceFromHand(PlayerController playerController, int cardsToDiscardCount)
    {
        List<string> handFormatoString = playerController.StringCardsHand();
        if (handFormatoString.Count() > 0)
        {
            int selectedCard = gameStructureInfo.view.AskPlayerToSelectACardToDiscard(handFormatoString, playerController.NameOfSuperStar(), playerController.NameOfSuperStar(), cardsToDiscardCount);
            
            if (selectedCard != -1)
            {
                CardController discardCardController = playerController.GetSpecificCardFromHand(selectedCard);
                Player playerWhoDiscardCard = (playerController == gameStructureInfo.ControllerCurrentPlayer)? gameStructureInfo.GetCurrentPlayer(): gameStructureInfo.GetOpponentPlayer();
            
                gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(playerWhoDiscardCard, discardCardController);
            }
        }
    }

    public void DiscardOpponentCardOfMyChoiceFromHand(PlayerController opponentPlayerController, PlayerController currentPlayerController, int cardsToDiscardCount)
    {
        List<string> handFormatoString = opponentPlayerController.StringCardsHand();
        if (handFormatoString.Count() > 0)
        {
            int selectedCard = gameStructureInfo.view.AskPlayerToSelectACardToDiscard(handFormatoString, opponentPlayerController.NameOfSuperStar(), currentPlayerController.NameOfSuperStar(), cardsToDiscardCount);
            
            if (selectedCard != -1)
            {
                CardController discardCardController = opponentPlayerController.GetSpecificCardFromHand(selectedCard);
                Player playerWhoDiscardCard = (opponentPlayerController == gameStructureInfo.ControllerCurrentPlayer)? gameStructureInfo.GetCurrentPlayer(): gameStructureInfo.GetOpponentPlayer();
            
                gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(playerWhoDiscardCard, discardCardController);
            }
        } 
    }

    public void ColateralDamage(PlayerController controllerOpponentPlayer, Player player, int totalDamage = 1)
    {
        if (totalDamage > 0)
        {   
            gameStructureInfo.view.SayThatPlayerDamagedHimself(controllerOpponentPlayer.NameOfSuperStar(), totalDamage);
            gameStructureInfo.view.SayThatSuperstarWillTakeSomeDamage(controllerOpponentPlayer.NameOfSuperStar(), totalDamage);
            for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
            {
                if (CheckCanReceiveDamage(controllerOpponentPlayer))
                {
                    string flippedCardString = ShowOneFaceDownCard(currentDamage + 1, totalDamage, player, controllerOpponentPlayer);
                    gameStructureInfo.view.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
                }
                else
                {
                    gameStructureInfo.view.SayThatPlayerLostDueToSelfDamage(controllerOpponentPlayer.NameOfSuperStar());
                    gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(controllerOpponentPlayer);
                }
            }
        }
    }

    public void GetBackDamage(PlayerController controllerPlayer, Player player, int recoveredDamage = 1)
    {
        List<string> ringAreaAsString = controllerPlayer.StringCardsRingSide();
        int selectedCardIndex = gameStructureInfo.view.AskPlayerToSelectCardsToRecover(controllerPlayer.NameOfSuperStar(), recoveredDamage, ringAreaAsString);
        CardController discardedCardController = controllerPlayer.GetSpecificCardFromRingSide(selectedCardIndex);
        
        gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToArsenal(player, discardedCardController, "Start");
    }

    public void takeDamage(PlayerController controllerPlayer, Player player, int totalDamage)
    {
        if (totalDamage > 0)
        {   
            gameStructureInfo.view.SayThatSuperstarWillTakeSomeDamage(controllerPlayer.NameOfSuperStar(), totalDamage);
            /*
            for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
            {
                if (CheckCanReceiveDamage(controllerPlayer))
                {
                    //CardController flippedCardController =
                        gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
                    //string flippedCardString = gameStructureInfo.VisualizeCards.GetStringCardInfo(flippedCardController);
                    //gameStructureInfo.view.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
                    
                }
                else
                {
                    gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(controllerPlayer);
                }
            }
            */
            CardController flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
            string flippedCardString = gameStructureInfo.VisualizeCards.GetStringCardInfo(flippedCardController);
        
            gameStructureInfo.view.ShowCardOverturnByTakingDamage(flippedCardString, 1, totalDamage);
            
        }
    }
    
    public void EndTurn()
    {
        gameStructureInfo.GetSetGameVariables.UpdateVariablesAtEndOfTurn();
    }
}