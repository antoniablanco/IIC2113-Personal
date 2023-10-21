using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class AddingChoosingCardFromRingSideToHandEffectUtils: EffectsUtils
{
    private PlayerController controllerPlayer;
    
    public AddingChoosingCardFromRingSideToHandEffectUtils(PlayerController controllerPlayer, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this.controllerPlayer = controllerPlayer;
        Apply();
    }
    
    private void Apply()
    {
        var ringSideAsString = controllerPlayer.StringCardsFrom("RingSide");
        var selectedCard =
            gameStructureInfo.View.AskPlayerToSelectCardsToPutInHisHand(controllerPlayer.NameOfSuperStar(), 1, ringSideAsString);

        var addedCardController = controllerPlayer.GetSpecificCardFrom("RingSide", selectedCard);
        var playerWhoDiscardCard = GetPlayerWhoDiscard();
        gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToHand(playerWhoDiscardCard, addedCardController);
    }

    private Player GetPlayerWhoDiscard()
    {
        return controllerPlayer == gameStructureInfo.ControllerCurrentPlayer
            ? gameStructureInfo.GetCurrentPlayer()
            : gameStructureInfo.GetOpponentPlayer();
    }
}