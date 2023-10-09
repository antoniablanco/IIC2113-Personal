using RawDeal.PlayerClass;

namespace RawDeal.CardClass;

public class CardEffects
{   
    public GameStructureInfo gameStructureInfo= new GameStructureInfo();
    
    public void StealCard()
    {   
        gameStructureInfo.view.SayThatPlayerDrawCards(gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar(), 1);
        Player player = gameStructureInfo.GetCurrentPlayer();
        gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToHand(player);
    }
    
    public void DiscardCard(CardController playedCardController)
    {   
        gameStructureInfo.view.SayThatPlayerMustDiscardThisCard(gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar(), playedCardController.GetCardTitle());
        Player player = gameStructureInfo.GetCurrentPlayer();
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(player, playedCardController);
    }
    
    public void CauseDamageActionPlayCard(int totalDamage)
    {
        for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
        {
            if (CheckCanReceiveDamage())
                ShowOneFaceDownCard(currentDamage + 1, totalDamage);
            else
                gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning();
        }
    }

    private bool CheckCanReceiveDamage()
    {
        return gameStructureInfo.ControllerOpponentPlayer.AreThereCardsLeftInTheArsenal();
    }

    private void ShowOneFaceDownCard(int currentDamage, int totalDamage)
    {
        Player player = gameStructureInfo.GetOpponentPlayer();
        CardController flippedCardController =
            gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
        string flippedCardString = gameStructureInfo.VisualizeCards.GetStringCardInfo(flippedCardController);
        gameStructureInfo.view.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
    }

    public void EndTurn()
    {
        gameStructureInfo.GetSetGameVariables.UpdateVariablesAtEndOfTurn();
    }
}