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
        try
        {
            CardController card = playerController.FindCardCardFrom("RingSide", cardTitle);
            gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToHand(player, card);
        }
        catch (CardNotFoundException)
        {
            try
            {
                CardController card = playerController.FindCardCardFrom("Arsenal", cardTitle);
                gameStructureInfo.CardMovement.TransferChoosinCardArsenalToHand(player, card);
            }
            catch (CardNotFoundException) { }
        }
        
    }
}