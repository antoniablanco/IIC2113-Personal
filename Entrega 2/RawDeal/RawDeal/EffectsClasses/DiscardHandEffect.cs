using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class DiscardHandEffect: EffectsUtils
{
    private PlayerController controllerPlayer;
    private Player player;
    
    public DiscardHandEffect(PlayerController controllerPlayer, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {   
        this.controllerPlayer = controllerPlayer;
        player = gameStructureInfo.ControllerOpponentPlayer == controllerPlayer ? 
            gameStructureInfo.GetOpponentPlayer() : gameStructureInfo.GetCurrentPlayer();
        DiscardHand();
    }

    private void DiscardHand()
    {
        gameStructureInfo.View.SayThatPlayerDiscardsHisHand(controllerPlayer.GetNameOfSuperStar());
        int numberOfCardsInHand = controllerPlayer.GetNumberOfCardIn("Hand");
        
        for (var currentIndex = 0; currentIndex < numberOfCardsInHand; currentIndex++)
        {   
            var discardCardController = controllerPlayer.RetrieveCardFromDeckAtPosition("Hand", 0);
            gameStructureInfo.CardMovement.TransferSelectedCardFromHandToRingSide(player,
                discardCardController);
        }
    }
}