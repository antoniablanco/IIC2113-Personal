using RawDeal.CardClass;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class Effects
{
    private GameStructureInfo gameStructureInfo= new GameStructureInfo();
    
    public Effects(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }

    public void MayStealCards(PlayerController controllerCurrentPlayer, Player player, int maximumNumberOfcardToDraw)
    {
        int numberOfcardToDraw = gameStructureInfo.View.AskHowManyCardsToDrawBecauseOfACardEffect(controllerCurrentPlayer.NameOfSuperStar(), maximumNumberOfcardToDraw);
        StealCards(controllerCurrentPlayer, player, numberOfcardToDraw);
    }
    
    public void StealCards(PlayerController controllerCurrentPlayer, Player player, int numberOfcardToDraw = 1)
    {
        if (controllerCurrentPlayer.HasCardsInArsenal())
        {   
            if (controllerCurrentPlayer.NumberOfCardIn("Arsenal") < numberOfcardToDraw)
                numberOfcardToDraw = controllerCurrentPlayer.NumberOfCardIn("Arsenal");
            gameStructureInfo.View.SayThatPlayerDrawCards(controllerCurrentPlayer.NameOfSuperStar(), numberOfcardToDraw);
            for (int i = 0; i < numberOfcardToDraw; i++)
            {   
                gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToHand(player);
            }
        }
    }
    
    public void DiscardCardsFromHandToRingSide(PlayerController opponentPlayerController, PlayerController currentPlayerController, int cardsToDiscardCount)
    {
        for (int courrentDamage = 0; courrentDamage < cardsToDiscardCount; courrentDamage++)
            DiscardCardOfMyChoiceFromHand(opponentPlayerController,currentPlayerController, cardsToDiscardCount-courrentDamage);
    }
    
    public void DiscardCardOfMyChoiceFromHand(PlayerController opponentPlayerController, PlayerController currentPlayerController, int cardsToDiscardCount)
    {   
        List<string> handFormatoString = opponentPlayerController.HandCardsButNotTheCardIsBeingPlayed(gameStructureInfo.LastPlayedCard).Item1;
        if (handFormatoString.Count() > 0)
        {
            int selectedCard = gameStructureInfo.View.AskPlayerToSelectACardToDiscard(handFormatoString, opponentPlayerController.NameOfSuperStar(), currentPlayerController.NameOfSuperStar(), cardsToDiscardCount);
            
            if (selectedCard != -1)
            {
                CardController discardCardController = opponentPlayerController.HandCardsButNotTheCardIsBeingPlayed(gameStructureInfo.LastPlayedCard).Item2[selectedCard];
                Player playerWhoDiscardCard = (opponentPlayerController == gameStructureInfo.ControllerCurrentPlayer)? gameStructureInfo.GetCurrentPlayer(): gameStructureInfo.GetOpponentPlayer();
                gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(playerWhoDiscardCard, discardCardController);
            }
        } 
    }
    
    public void DiscardActionCardWithNoEfect(CardController playedCardController, PlayerController controllerCurrentPlayer, Player player)
    {   
        gameStructureInfo.View.SayThatPlayerMustDiscardThisCard(controllerCurrentPlayer.NameOfSuperStar(), playedCardController.GetCardTitle());
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(player, playedCardController);
    }
    
    public void DiscardActionCardToRingAreButNotSaying(CardController playedCardController, Player player)
    {   
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(player, playedCardController);
    }
    
    public void DiscardingCardsFromHandToArsenal(PlayerController playerController)
    {
        List<string> handCardsAsString = gameStructureInfo.ControllerCurrentPlayer.StringCardsFrom("Hand");
        int selectedCard = gameStructureInfo.View.AskPlayerToReturnOneCardFromHisHandToHisArsenal(playerController.NameOfSuperStar(), handCardsAsString);
        
        CardController discardedCardController = gameStructureInfo.ControllerCurrentPlayer.GetSpecificCardFrom("Hand", selectedCard);
        
        Player player = gameStructureInfo.GetCurrentPlayer();
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToArsenal(player, discardedCardController, "Start");
    }
    
    public void ProduceDamage(int totalDamage, PlayerController controllerOpponentPlayer, Player player)
    {
        if (totalDamage <= 0) return;
        
        gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(controllerOpponentPlayer.NameOfSuperStar(), totalDamage);
        for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
        {   
            if (CheckIfThePlayerCanReceiveDamage(controllerOpponentPlayer))
                ShowOneFaceDownCard(currentDamage + 1, totalDamage, player);
            else
                gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(controllerOpponentPlayer);
        }
    }

    public bool IsTheCardWeAreReversalOfMankindSuperStart(PlayerController playerController)
    {
        return IsTheSuperStarMankind(playerController);
    }
    
    private bool CheckIfThePlayerCanReceiveDamage(PlayerController controllerOpponentPlayer)
    {
        return controllerOpponentPlayer.HasCardsInArsenal();
    }

    private string ShowOneFaceDownCard(int currentDamage, int totalDamage, Player player)
    {
        CardController flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
        string flippedCardString = flippedCardController.GetStringCardInfo();
        gameStructureInfo.View.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
        return flippedCardString;
    }

    public int GetDamageProducedByReversalCardWithNotEspecificDamage()
    {
        int totalDamage = gameStructureInfo.LastPlayedCard.GetDamageProducedByTheCard() + gameStructureInfo.BonusDamage*gameStructureInfo.IsJockeyingForPositionBonusDamageActive;
        if (IsTheSuperStarMankind(gameStructureInfo.ControllerOpponentPlayer) || IsTheSuperStarMankind(gameStructureInfo.ControllerCurrentPlayer))
            totalDamage -= 1;
        return totalDamage;
    }

    private bool IsTheSuperStarMankind(PlayerController playerController)
    {
        return playerController.NameOfSuperStar() == "MANKIND";
    }
    
    public void ColateralDamage(PlayerController controllerOpponentPlayer, Player player, int totalDamage = 1)
    {
        if (totalDamage <= 0) return;
        
        gameStructureInfo.View.SayThatPlayerDamagedHimself(controllerOpponentPlayer.NameOfSuperStar(), totalDamage);
        gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(controllerOpponentPlayer.NameOfSuperStar(), totalDamage);
        for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
        {   
            if (CheckIfThePlayerCanReceiveDamage(controllerOpponentPlayer))
            {
                ShowOneFaceDownCard(currentDamage + 1, totalDamage, player);
            }
            else
            {
                gameStructureInfo.View.SayThatPlayerLostDueToSelfDamage(controllerOpponentPlayer.NameOfSuperStar());
                gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(controllerOpponentPlayer);
            }
        }
    }

    public void GetBackDamage(PlayerController controllerPlayer, Player player, int recoveredDamage = 1)
    {
        List<string> ringSideAsString = controllerPlayer.StringCardsFrom("RingSide");
        if (ringSideAsString.Count() < recoveredDamage)
            recoveredDamage = ringSideAsString.Count();
        
        for (int currentDamage = 0; currentDamage < recoveredDamage; currentDamage++)
        {   
            ringSideAsString = controllerPlayer.StringCardsFrom("RingSide");
            int selectedCardIndex = gameStructureInfo.View.AskPlayerToSelectCardsToRecover(controllerPlayer.NameOfSuperStar(), recoveredDamage-currentDamage, ringSideAsString);
            CardController discardedCardController = controllerPlayer.GetSpecificCardFrom("RingSide", selectedCardIndex);
            
            gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToArsenal(player, discardedCardController, "Start");
        }
        
    }
    
    public void TakeDamage(PlayerController controllerPlayer, Player player, int totalDamage)
    {
        if (totalDamage <= 0) return;
        gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(controllerPlayer.NameOfSuperStar(), totalDamage);
            
        for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
        {
            CardController flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
            //string flippedCardString = gameStructureInfo.CardsVisualizor.GetStringCardInfo(flippedCardController);
            string flippedCardString = flippedCardController.GetStringCardInfo();
        
            gameStructureInfo.View.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage+1, totalDamage);
        }
    }
    
    public void AddingCardFromRingSideToHand(PlayerController player)
    {
        List<string> ringSideAsString = player.StringCardsFrom("RingSide");
        int selectedCard = gameStructureInfo.View.AskPlayerToSelectCardsToPutInHisHand(player.NameOfSuperStar(), 1, ringSideAsString);
        
        CardController addedCardController = player.GetSpecificCardFrom("RingSide", selectedCard);
        Player playerHowDiscardCard = (player == gameStructureInfo.ControllerCurrentPlayer)? gameStructureInfo.GetCurrentPlayer(): gameStructureInfo.GetOpponentPlayer();
        gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToHand(playerHowDiscardCard, addedCardController);
    }
    
    public void AddingCardFromRingSideToArsenal(PlayerController playerController)
    {
        List<string> ringAreaAsString = gameStructureInfo.ControllerCurrentPlayer.StringCardsFrom("RingSide");
        int selectedCardIndex = gameStructureInfo.View.AskPlayerToSelectCardsToRecover(playerController.NameOfSuperStar(), 1, ringAreaAsString);
        CardController discardedCardController = gameStructureInfo.ControllerCurrentPlayer.GetSpecificCardFrom("RingSide", selectedCardIndex);
        gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToArsenal(gameStructureInfo.GetCurrentPlayer(), discardedCardController, "Start");
    }
    
    public void EndTurn()
    {
        gameStructureInfo.GetSetGameVariables.UpdateVariablesAtEndOfTurn();
    }
}