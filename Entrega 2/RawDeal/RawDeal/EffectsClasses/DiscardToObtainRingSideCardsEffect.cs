using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class DiscardToObtainRingSideCardsEffect: EffectsUtils
{   
    private PlayerController currentPlayerController;
    private int maximumNumberOfCardsToDiscard;
    
    public DiscardToObtainRingSideCardsEffect(PlayerController currentPlayerController, 
        GameStructureInfo gameStructureInfo, int maximumNumberOfCardsToDiscard)
        : base(gameStructureInfo)
    {
        this.currentPlayerController = currentPlayerController;
        this.maximumNumberOfCardsToDiscard = maximumNumberOfCardsToDiscard;
        Apply();
    }

    private void Apply()
    {
        maximumNumberOfCardsToDiscard =
            Math.Min(maximumNumberOfCardsToDiscard, currentPlayerController.NumberOfCardIn("Hand"));
        if (IsPositive(maximumNumberOfCardsToDiscard))
            AskHowManyCardsWantsToChange();
    }

    private void AskHowManyCardsWantsToChange()
    {
        int numberOfCardsToDiscard = gameStructureInfo.View.AskHowManyCardsToDiscard(currentPlayerController.NameOfSuperStar(), maximumNumberOfCardsToDiscard);
        
        new DiscardCardsFromHandToRingSideEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardsToDiscard, gameStructureInfo);
            
        for (int i = 0; i < numberOfCardsToDiscard; i++)
            new AddingChoosingCardFromRingSideToHandEffectUtils(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo);
    }
}