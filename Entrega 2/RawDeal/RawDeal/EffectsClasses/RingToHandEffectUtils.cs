using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class RingToHandEffectUtils: EffectsUtils
{
    private PlayerController controllerPlayer;
    private int numberOfCardToRecover;
    
    public RingToHandEffectUtils(PlayerController controllerPlayer, 
        GameStructureInfo gameStructureInfo, int numberOfCardToRecover=1)
        : base(gameStructureInfo)
    {
        this.controllerPlayer = controllerPlayer;
        this.numberOfCardToRecover = numberOfCardToRecover;
        Apply();
    }
    
    private void Apply()
    {   
        for (int currentNumberOfCard = 0; currentNumberOfCard < numberOfCardToRecover; currentNumberOfCard++)
            DiscardCard(numberOfCardToRecover-currentNumberOfCard);
        
    }

    private void DiscardCard(int currentNumberOfCard)
    {
        var ringSideAsString = controllerPlayer.StringCardsFrom("RingSide");
        var selectedCard = gameStructureInfo.View.AskPlayerToSelectCardsToPutInHisHand(
            controllerPlayer.GetNameOfSuperStar(), currentNumberOfCard, ringSideAsString);

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