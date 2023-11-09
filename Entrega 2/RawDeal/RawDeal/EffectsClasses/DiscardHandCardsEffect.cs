using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class DiscardHandCardsEffect: EffectsUtils
{
    private PlayerController controllerPlayer;
    private Player player;
    
    public DiscardHandCardsEffect(PlayerController controllerPlayer, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {   
        this.controllerPlayer = controllerPlayer;
        player = gameStructureInfo.ControllerOpponentPlayer == controllerPlayer ? 
            gameStructureInfo.GetOpponentPlayer() : gameStructureInfo.GetCurrentPlayer();
        DiscardHand();
    }

    private void DiscardHand()
    {
        gameStructureInfo.View.SayThatPlayerDiscardsHisHand(controllerPlayer.NameOfSuperStar());
        int numberOfCardsInHand = controllerPlayer.NumberOfCardIn("Hand");
        
        for (var currentIndex = 0; currentIndex < numberOfCardsInHand; currentIndex++)
        {   
            var discardCardController = controllerPlayer.GetSpecificCardFrom("Hand", currentIndex);
            gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(player,
                discardCardController);
        }
    }
}