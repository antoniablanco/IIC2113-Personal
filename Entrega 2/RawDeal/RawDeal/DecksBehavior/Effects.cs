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
        if (CheckIfThePlayerCanReceiveDamage(controllerCurrentPlayer))
        {
            int numberOfcardToDraw = gameStructureInfo.View.AskHowManyCardsToDrawBecauseOfACardEffect(controllerCurrentPlayer.NameOfSuperStar(), maximumNumberOfcardToDraw);
            if (controllerCurrentPlayer.NumberOfCardIn("Arsenal") < numberOfcardToDraw)
                numberOfcardToDraw = controllerCurrentPlayer.NumberOfCardIn("Arsenal");
            StealCards(controllerCurrentPlayer, player, numberOfcardToDraw);
        }
    }
    
    public void StealCards(PlayerController controllerCurrentPlayer, Player player, int numberOfcardToDraw = 1)
    {
        gameStructureInfo.View.SayThatPlayerDrawCards(controllerCurrentPlayer.NameOfSuperStar(), numberOfcardToDraw);
        for (int i = 0; i < numberOfcardToDraw; i++)
        {   
            gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToHand(player);
        }
    }
    
    public void DiscardCardsFromHandToRingSide(PlayerController opponentPlayerController, PlayerController currentPlayerController, int cardsToDiscardCount)
    {
        for (int currentDamage = 0; currentDamage < cardsToDiscardCount; currentDamage++)
        {
            List<string> handFormatoString = opponentPlayerController.HandCardsButNotTheCardIsBeingPlayed(gameStructureInfo.LastPlayedCard).Item1;
        
            if (HasDamageToApply(handFormatoString.Count()))
                DiscardACardOfMyChoiceFromHandNotNotifying(opponentPlayerController,currentPlayerController, cardsToDiscardCount-currentDamage, handFormatoString);
        }
    }

    private void DiscardACardOfMyChoiceFromHandNotNotifying(PlayerController opponentPlayerController, PlayerController currentPlayerController, int cardsToDiscardCount, List<string> handFormatoString)
    {   
        int selectedCard = gameStructureInfo.View.AskPlayerToSelectACardToDiscard(handFormatoString, opponentPlayerController.NameOfSuperStar(), currentPlayerController.NameOfSuperStar(), cardsToDiscardCount);
        
        if (gameStructureInfo.PlayCard.HasSelectedAValidCard(selectedCard))
        {
            CardController discardCardController = opponentPlayerController.HandCardsButNotTheCardIsBeingPlayed(gameStructureInfo.LastPlayedCard).Item2[selectedCard];
            Player playerWhoDiscardCard = (opponentPlayerController == gameStructureInfo.ControllerCurrentPlayer)? gameStructureInfo.GetCurrentPlayer(): gameStructureInfo.GetOpponentPlayer();
            gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(playerWhoDiscardCard, discardCardController);
        }
    }
    
    public void DiscardCardFromHandNotifying(CardController playedCardController, PlayerController controllerCurrentPlayer, Player player)
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
    
    public void ProduceSeveralDamage(int totalDamage, PlayerController controllerOpponentPlayer, Player player)
    {
        if (HasDamageToApply(totalDamage))
        {
            gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(controllerOpponentPlayer.NameOfSuperStar(), totalDamage);
        
            for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
                InflictADamage(totalDamage, currentDamage, controllerOpponentPlayer, player);
        }
    }

    private bool InflictADamage(int totalDamage, int currentDamage, PlayerController controllerOpponentPlayer, Player player)
    {
        if (CheckIfThePlayerCanReceiveDamage(controllerOpponentPlayer))
            ShowOneFaceDownCard(currentDamage + 1, totalDamage, player);
        else
        {   
            gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(controllerOpponentPlayer);
            return false;
        }

        return true;
    }
    
    private bool CheckIfThePlayerCanReceiveDamage(PlayerController controllerPlayer)
    {
        return controllerPlayer.HasCardsInArsenal();
    }
    
    private string ShowOneFaceDownCard(int currentDamage, int totalDamage, Player player)
    {
        CardController flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
        string flippedCardString = flippedCardController.GetStringCardInfo();
        gameStructureInfo.View.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
        return flippedCardString;
    }
    
    public bool IsTheCardWeAreReversalOfMankindSuperStart(PlayerController playerController)
    {
        return IsTheSuperStarMankind(playerController);
    }

    public int GetDamageProducedByReversalCardWithNotEspecificDamage()
    {
        int totalDamage = gameStructureInfo.LastPlayedCard.GetDamageProducedByTheCard() + gameStructureInfo.BonusManager.AddBonus("JockeyingDamage");
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
        gameStructureInfo.View.SayThatPlayerDamagedHimself(controllerOpponentPlayer.NameOfSuperStar(), totalDamage);
        gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(controllerOpponentPlayer.NameOfSuperStar(), totalDamage);
        
        for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
        {   
            if (!InflictADamage(totalDamage, currentDamage, controllerOpponentPlayer, player))
                gameStructureInfo.View.SayThatPlayerLostDueToSelfDamage(controllerOpponentPlayer.NameOfSuperStar());
        }
    }

    public void GetBackDamage(PlayerController controllerPlayer, Player player, int recoveredDamage = 1)
    {
        List<string> ringSideAsString = controllerPlayer.StringCardsFrom("RingSide");
        
        recoveredDamage = LimitRecoveryToAvailableDamage(recoveredDamage, ringSideAsString);

        RecoverDamageFromRingSide(controllerPlayer, player, recoveredDamage, ringSideAsString);
    }

    private int LimitRecoveryToAvailableDamage(int recoveredDamage, List<string> ringSideAsString)
    {
        if (ringSideAsString.Count() < recoveredDamage)
            recoveredDamage = ringSideAsString.Count();
        
        return recoveredDamage;
    }

    private void RecoverDamageFromRingSide(PlayerController controllerPlayer, Player player, int recoveredDamage, List<string> ringSideAsString)
    {
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
        if (HasDamageToApply(totalDamage))
        {
            gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(controllerPlayer.NameOfSuperStar(), totalDamage);
            ApplyDamageToPlayer(player, totalDamage);
        }
    }

    private bool HasDamageToApply(int totalDamage)
    {
        return totalDamage > 0;
    }

    private void ApplyDamageToPlayer(Player player, int totalDamage)
    {
        for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
        {
            CardController flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
            string flippedCardString = flippedCardController.GetStringCardInfo();
        
            gameStructureInfo.View.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage+1, totalDamage);
        }
    }
    
    public void AddingChoosingCardFromRingSideToHand(PlayerController player)
    {
        List<string> ringSideAsString = player.StringCardsFrom("RingSide");
        int selectedCard = gameStructureInfo.View.AskPlayerToSelectCardsToPutInHisHand(player.NameOfSuperStar(), 1, ringSideAsString);
        
        CardController addedCardController = player.GetSpecificCardFrom("RingSide", selectedCard);
        Player playerWhoDiscardCard = (player == gameStructureInfo.ControllerCurrentPlayer)? gameStructureInfo.GetCurrentPlayer(): gameStructureInfo.GetOpponentPlayer();
        gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToHand(playerWhoDiscardCard, addedCardController);
    }
    
    public void AddingChoosingCardFromRingSideToArsenal(PlayerController playerController)
    {
        List<string> ringAreaAsString = gameStructureInfo.ControllerCurrentPlayer.StringCardsFrom("RingSide");
        int selectedCardIndex = gameStructureInfo.View.AskPlayerToSelectCardsToRecover(playerController.NameOfSuperStar(), 1, ringAreaAsString);
        CardController discardedCardController = gameStructureInfo.ControllerCurrentPlayer.GetSpecificCardFrom("RingSide", selectedCardIndex);
        gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToArsenal(gameStructureInfo.GetCurrentPlayer(), discardedCardController, "Start");
    }
    
    public void EndTurn()
    {
        gameStructureInfo.EndTurnManager.UpdateVariablesAtEndOfTurn();
    }
}