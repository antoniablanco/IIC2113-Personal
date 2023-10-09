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
    
    public void DiscardCard(CardController playedCardController, PlayerController controllerCurrentPlayer, Player player)
    {   
        gameStructureInfo.view.SayThatPlayerMustDiscardThisCard(controllerCurrentPlayer.NameOfSuperStar(), playedCardController.GetCardTitle());
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(player, playedCardController);
    }

    public void EndTurn()
    {
        gameStructureInfo.GetSetGameVariables.UpdateVariablesAtEndOfTurn();
    }
}