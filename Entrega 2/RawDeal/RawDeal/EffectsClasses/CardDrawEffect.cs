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
    
    public void MayStealCards(int maximumNumberOfcardToDraw)
    {   
        var numberOfcardToDraw = gameStructureInfo.View.AskHowManyCardsToDrawBecauseOfACardEffect(
            controllerPlayer.GetNameOfSuperStar(), maximumNumberOfcardToDraw);
        
        StealCards(numberOfcardToDraw);
    }

    public void StealCards(int numberOfcardToDraw = 1)
    {   
        numberOfcardToDraw = Math.Min(controllerPlayer.NumberOfCardIn("Arsenal"), numberOfcardToDraw);
        gameStructureInfo.View.SayThatPlayerDrawCards(controllerPlayer.GetNameOfSuperStar(), numberOfcardToDraw);
        if (IsPositive(numberOfcardToDraw))
        {
            for (var i = 0; i < numberOfcardToDraw; i++)
                gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToHand(player);
        }
    }
}