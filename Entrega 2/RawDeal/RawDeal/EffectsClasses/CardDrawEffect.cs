using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class CardDrawEffect: EffectsUtils
{
    private PlayerController controllerPlayer;
    private Player player;
    
    public CardDrawEffect(PlayerController controllerPlayer, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this.controllerPlayer = controllerPlayer;
        player = gameStructureInfo.ControllerOpponentPlayer == controllerPlayer ? 
            gameStructureInfo.GetOpponentPlayer() : gameStructureInfo.GetCurrentPlayer();
    }
    
    public void MayStealCards(int maximumNumberOfCardToDraw)
    {   
        var numberOfCardToDraw = gameStructureInfo.View.AskHowManyCardsToDrawBecauseOfACardEffect(
            controllerPlayer.GetNameOfSuperStar(), maximumNumberOfCardToDraw);
        
        StealCards(numberOfCardToDraw);
    }

    public void StealCards(int numberOfCardToDraw = 1)
    {   
        numberOfCardToDraw = Math.Min(controllerPlayer.GetNumberOfCardIn("Arsenal"), numberOfCardToDraw);
        gameStructureInfo.View.SayThatPlayerDrawCards(controllerPlayer.GetNameOfSuperStar(), numberOfCardToDraw);
        if (IsPositive(numberOfCardToDraw))
        {
            for (var i = 0; i < numberOfCardToDraw; i++)
                gameStructureInfo.CardMovement.TransferUnselectedCardFromArsenalToHand(player);
        }
    }
}