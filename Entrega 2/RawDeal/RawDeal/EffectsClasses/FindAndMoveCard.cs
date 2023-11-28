using RawDeal.CardClasses;
using RawDeal.Exceptions;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class FindAndMoveCard: EffectsUtils
{
    private string cardTitle;
    private PlayerController playerController;
    private Player player;
    
    
    public FindAndMoveCard(string cardTitle, PlayerController playerController, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this.cardTitle = cardTitle;
        this.playerController = playerController;
        
        player = gameStructureInfo.ControllerOpponentPlayer == playerController ? 
            gameStructureInfo.GetOpponentPlayer() : gameStructureInfo.GetCurrentPlayer();
        Apply();
    }

    private void Apply()
    {
        try { SearchCardInRingSide(); }
        catch (CardNotFoundException)
        {   
            gameStructureInfo.View.SayThatPlayerDidntFindTheCard(playerController.GetNameOfSuperStar());
            try { SearchCardInArsenal(); }
            catch (CardNotFoundException)
            {
                gameStructureInfo.View.SayThatPlayerDidntFindTheCard(playerController.GetNameOfSuperStar());
            }
        }
        
    }

    private void SearchCardInRingSide()
    {   
        gameStructureInfo.View.SayThatPlayerSearchesForTheTargetCardInHisRingside(
            playerController.GetNameOfSuperStar(), cardTitle);
        CardController card = playerController.GetCardInDeckByName("RingSide", cardTitle);
        gameStructureInfo.View.SayThatPlayerFoundTheCardAndPutItIntoHisHand(playerController.GetNameOfSuperStar());
        gameStructureInfo.CardMovement.TransferSelectedCardFromRingSideToHand(player, card);
    }

    private void SearchCardInArsenal()
    {   
        gameStructureInfo.View.SayThatPlayerSearchesForTheTargetCardInHisArsenal(
            playerController.GetNameOfSuperStar(), cardTitle);
        CardController card = playerController.GetCardInDeckByName("Arsenal", cardTitle);
        gameStructureInfo.View.SayThatPlayerFoundTheCardAndPutItIntoHisHand(playerController.GetNameOfSuperStar());
        gameStructureInfo.CardMovement.TransferSelectedCardFromArsenalToHand(player, card);
    }
}