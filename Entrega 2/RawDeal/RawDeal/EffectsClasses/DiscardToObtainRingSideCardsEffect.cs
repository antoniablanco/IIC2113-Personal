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
        int numberOfCardInHand = currentPlayerController.HandCardsButNotTheCardIsBeingPlayed(gameStructureInfo.CardBeingPlayed).Item1.Count();
        maximumNumberOfCardsToDiscard = Math.Min(maximumNumberOfCardsToDiscard, numberOfCardInHand);

        if (IsPositive(maximumNumberOfCardsToDiscard))
            AskHowManyCardsWantsToChange();
    }

    private void AskHowManyCardsWantsToChange()
    {
        int numberOfCardsToDiscard = gameStructureInfo.View.AskHowManyCardsToDiscard(currentPlayerController.NameOfSuperStar(), 
            maximumNumberOfCardsToDiscard);
        
        new DiscardCardsFromHandToRingSideEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardsToDiscard, gameStructureInfo);
            
        new AddingChoosingCardFromRingSideToHandEffectUtils(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo, numberOfCardsToDiscard);
    }
}