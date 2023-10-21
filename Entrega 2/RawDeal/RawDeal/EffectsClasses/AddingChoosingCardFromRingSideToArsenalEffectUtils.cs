using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class AddingChoosingCardFromRingSideToArsenalEffectUtils: EffectsUtils
{
    private PlayerController controllerPlayer;
    
    public AddingChoosingCardFromRingSideToArsenalEffectUtils(PlayerController controllerPlayer, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this.controllerPlayer = controllerPlayer;
        Apply();
    }
    
    private void Apply()
    {
        var ringAreaAsString = gameStructureInfo.ControllerCurrentPlayer.StringCardsFrom("RingSide");
        var selectedCardIndex =
            gameStructureInfo.View.AskPlayerToSelectCardsToRecover(controllerPlayer.NameOfSuperStar(), 1,
                ringAreaAsString);
        var discardedCardController =
            gameStructureInfo.ControllerCurrentPlayer.GetSpecificCardFrom("RingSide", selectedCardIndex);
        gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToArsenal(gameStructureInfo.GetCurrentPlayer(),
            discardedCardController, "Start");
    }
}