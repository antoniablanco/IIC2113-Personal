using RawDeal.PlayerClass;

namespace RawDeal.CardClass;

public class CardEffects
{   
    public GameStructureInfo gameStructureInfo= new GameStructureInfo();
    
    public void StealCard(PlayerController controllerCurrentPlayer, Player player, int numberOfcardToDraw = 1)
    {   
        gameStructureInfo.view.SayThatPlayerDrawCards(controllerCurrentPlayer.NameOfSuperStar(), numberOfcardToDraw);
        for (int i = 0; i < numberOfcardToDraw; i++)
        {
            gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToHand(player);
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
                    gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning();
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

    private void ShowOneFaceDownCard(int currentDamage, int totalDamage, Player player, PlayerController controllerOpponentPlayer)
    {
        CardController flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
        string flippedCardString = gameStructureInfo.VisualizeCards.GetStringCardInfo(flippedCardController);
        gameStructureInfo.view.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
    }

    public int GetDamageProducedByReversalCardWithNotEspecificDamage()
    {
        int totalDamage = gameStructureInfo.LastPlayedCard.GetDamageProducedByTheCard() + gameStructureInfo.bonusDamage*gameStructureInfo.IsJockeyingForPositionBonusDamage;
        if (gameStructureInfo.ControllerOpponentPlayer.IsTheSuperStarMankind() || gameStructureInfo.ControllerCurrentPlayer.IsTheSuperStarMankind())
            totalDamage -= 1;
        return totalDamage;
    }   
    
    public void EndTurn()
    {
        gameStructureInfo.GetSetGameVariables.UpdateVariablesAtEndOfTurn();
    }
}