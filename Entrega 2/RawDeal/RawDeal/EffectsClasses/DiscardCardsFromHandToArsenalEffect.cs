using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class DiscardCardsFromHandToArsenalEffect: EffectsUtils
{
    private PlayerController controllerPlayer;
    
    public DiscardCardsFromHandToArsenalEffect(PlayerController controllerPlayer, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this.controllerPlayer = controllerPlayer;
        Apply();
    }

    private void Apply()
    {
        var handCardsAsString = controllerPlayer.StringCardsFrom("Hand");
        var selectedCard =
            gameStructureInfo.View.AskPlayerToReturnOneCardFromHisHandToHisArsenal(controllerPlayer.NameOfSuperStar(),
                handCardsAsString);

        var discardedCardController = controllerPlayer.GetSpecificCardFrom("Hand", selectedCard);

        var player = gameStructureInfo.GetCurrentPlayer();
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToArsenal(player, discardedCardController, "Start");
    }
}