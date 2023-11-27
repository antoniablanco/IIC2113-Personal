using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class DrawCardEffect: EffectsUtils
{
    private PlayerController controllerPlayer;
    private Player player;
    
    public DrawCardEffect(PlayerController controllerPlayer, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this.controllerPlayer = controllerPlayer;
        player = gameStructureInfo.ControllerOpponentPlayer == controllerPlayer ? 
            gameStructureInfo.GetOpponentPlayer() : gameStructureInfo.GetCurrentPlayer();
    }
    
    public void MayStealCards(int maximumNumberOfcardToDraw)
    {   
        var numberOfcardToDraw = gameStructureInfo.View.AskHowManyCardsToDrawBecauseOfACardEffect(
            controllerPlayer.NameOfSuperStar(), maximumNumberOfcardToDraw);
        
        StealCards(numberOfcardToDraw);
    }

    public void StealCards(int numberOfcardToDraw = 1)
    {   
        numberOfcardToDraw = Math.Min(controllerPlayer.NumberOfCardIn("Arsenal"), numberOfcardToDraw);
        gameStructureInfo.View.SayThatPlayerDrawCards(controllerPlayer.NameOfSuperStar(), numberOfcardToDraw);
        if (IsPositive(numberOfcardToDraw))
        {
            for (var i = 0; i < numberOfcardToDraw; i++)
                gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToHand(player);
        }
    }
}