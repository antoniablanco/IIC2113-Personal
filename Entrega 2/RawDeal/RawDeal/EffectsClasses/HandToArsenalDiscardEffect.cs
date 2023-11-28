using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class HandToArsenalDiscardEffect: EffectsUtils
{
    private PlayerController controllerPlayer;
    
    public HandToArsenalDiscardEffect(PlayerController controllerPlayer, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this.controllerPlayer = controllerPlayer;
        Apply();
    }

    private void Apply()
    {
        var handCardsAsString = controllerPlayer.GetStringCardsFrom("Hand");
        var selectedCard =
            gameStructureInfo.View.AskPlayerToReturnOneCardFromHisHandToHisArsenal(controllerPlayer.GetNameOfSuperStar(),
                handCardsAsString);

        var discardedCardController = controllerPlayer.RetrieveCardFromDeckAtPosition("Hand", selectedCard);

        var player = gameStructureInfo.GetCurrentPlayer();
        gameStructureInfo.CardMovement.TransferSelectedCardFromHandToStartOfArsenal(player, discardedCardController);
    }
}