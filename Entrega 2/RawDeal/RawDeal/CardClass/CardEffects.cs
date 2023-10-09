using RawDeal.PlayerClass;

namespace RawDeal.CardClass;

public class CardEffects
{   
    public GameStructureInfo gameStructureInfo= new GameStructureInfo();
    public bool isUserReversalDeckCard = false;
    
    public void StealCard(PlayerController controllerCurrentPlayer, Player player)
    {   
        gameStructureInfo.view.SayThatPlayerDrawCards(controllerCurrentPlayer.NameOfSuperStar(), 1);
        gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToHand(player);
    }
    
    public void DiscardCard(CardController playedCardController, PlayerController controllerCurrentPlayer, Player player)
    {   
        gameStructureInfo.view.SayThatPlayerMustDiscardThisCard(controllerCurrentPlayer.NameOfSuperStar(), playedCardController.GetCardTitle());
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(player, playedCardController);
    }
    
    public void CauseDamageActionPlayCard(int totalDamage, PlayerController controllerOpponentPlayer, Player player)
    {
        isUserReversalDeckCard = false;
        for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
        {   
            Console.WriteLine(gameStructureInfo.IsTheGameStillPlaying);
            if (CheckCanReceiveDamage(controllerOpponentPlayer) && !isUserReversalDeckCard)
                ShowOneFaceDownCard(currentDamage + 1, totalDamage, player, controllerOpponentPlayer);
            else if (!CheckCanReceiveDamage(controllerOpponentPlayer))
                gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning();
        }
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
        DeckReversal(flippedCardController, controllerOpponentPlayer);
    }

    private void DeckReversal(CardController flippedCardController, PlayerController controllerOpponentPlayer)
    {
        isUserReversalDeckCard = flippedCardController.CanUseThisReversalCard(controllerOpponentPlayer);
        if (isUserReversalDeckCard)
        {
            gameStructureInfo.view.SayThatCardWasReversedByDeck(controllerOpponentPlayer.NameOfSuperStar());
            EndTurn();
        }
    }

    public void EndTurn()
    {
        gameStructureInfo.GetSetGameVariables.UpdateVariablesAtEndOfTurn();
    }
}