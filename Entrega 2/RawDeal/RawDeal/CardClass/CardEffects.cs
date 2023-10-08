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
}