using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class RingToArsenalEffectUtils: EffectsUtils
{
    private PlayerController controllerPlayer;
    private int numberOfCardToRecover;
    
    public RingToArsenalEffectUtils(PlayerController controllerPlayer, 
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
        {
            DiscardCard(numberOfCardToRecover-currentNumberOfCard);
        }
            
    }

    private void DiscardCard(int currentNumberOfCard)
    {
        var ringAreaAsString = gameStructureInfo.ControllerCurrentPlayer.StringCardsFrom("RingSide");
        var selectedCardIndex =
            gameStructureInfo.View.AskPlayerToSelectCardsToRecover(controllerPlayer.GetNameOfSuperStar(), currentNumberOfCard,
                ringAreaAsString);
        var discardedCardController =
            gameStructureInfo.ControllerCurrentPlayer.GetSpecificCardFrom("RingSide", selectedCardIndex);
        gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToStartOfArsenal(gameStructureInfo.GetCurrentPlayer(),
            discardedCardController);
    }
    
}