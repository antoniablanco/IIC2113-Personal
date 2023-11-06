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
            gameStructureInfo.View.SayThatPlayerDidntFindTheCard(playerController.NameOfSuperStar());
            try { SearchCardInArsenal(); }
            catch (CardNotFoundException)
            {
                gameStructureInfo.View.SayThatPlayerDidntFindTheCard(playerController.NameOfSuperStar());
            }
        }
        
    }

    private void SearchCardInRingSide()
    {   
        cardTitle = cardTitle == "The People’s Elbow"? "The People's Elbow": cardTitle;
        gameStructureInfo.View.SayThatPlayerSearchesForTheTargetCardInHisRingside(
            playerController.NameOfSuperStar(), cardTitle);
        CardController card = playerController.FindCardCardFrom("RingSide", cardTitle);
        gameStructureInfo.View.SayThatPlayerFoundTheCardAndPutItIntoHisHand(playerController.NameOfSuperStar());
        gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToHand(player, card);
    }

    private void SearchCardInArsenal()
    {   
        cardTitle = cardTitle == "The People’s Elbow"? "The People's Elbow": cardTitle;
        gameStructureInfo.View.SayThatPlayerSearchesForTheTargetCardInHisArsenal(
            playerController.NameOfSuperStar(), cardTitle);
        CardController card = playerController.FindCardCardFrom("Arsenal", cardTitle);
        gameStructureInfo.View.SayThatPlayerFoundTheCardAndPutItIntoHisHand(playerController.NameOfSuperStar());
        gameStructureInfo.CardMovement.TransferChoosinCardArsenalToHand(player, card);
    }
}