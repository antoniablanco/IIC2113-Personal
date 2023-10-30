using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class DrawCardEffect: EffectsUtils
{
    private PlayerController controllerPlayer;
    private Player player;
    
    public DrawCardEffect(PlayerController controllerPlayer, Player player, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this.controllerPlayer = controllerPlayer;
        this.player = player;
    }
    
    public void MayStealCards(int maximumNumberOfcardToDraw)
    {
        if (CheckIfThePlayerHasCardInArsenal(controllerPlayer))
        {
            var numberOfcardToDraw = gameStructureInfo.View.AskHowManyCardsToDrawBecauseOfACardEffect(
                controllerPlayer.NameOfSuperStar(), maximumNumberOfcardToDraw);
            if (controllerPlayer.NumberOfCardIn("Arsenal") < numberOfcardToDraw)
                numberOfcardToDraw = controllerPlayer.NumberOfCardIn("Arsenal");
            StealCards(numberOfcardToDraw);
        }
    }

    public void StealCards(int numberOfcardToDraw = 1)
    {
        gameStructureInfo.View.SayThatPlayerDrawCards(controllerPlayer.NameOfSuperStar(), numberOfcardToDraw);
        for (var i = 0; i < numberOfcardToDraw; i++)
            gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToHand(player);
    }
}