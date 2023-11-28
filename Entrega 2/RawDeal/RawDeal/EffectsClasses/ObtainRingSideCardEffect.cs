using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class ObtainRingSideCardEffect: EffectsUtils
{   
    private PlayerController currentPlayerController;
    private int maximumNumberOfCardsToDiscard;
    
    public ObtainRingSideCardEffect(PlayerController currentPlayerController, 
        GameStructureInfo gameStructureInfo, int maximumNumberOfCardsToDiscard)
        : base(gameStructureInfo)
    {
        this.currentPlayerController = currentPlayerController;
        this.maximumNumberOfCardsToDiscard = maximumNumberOfCardsToDiscard;
        Apply();
    }

    private void Apply()
    {
        int numberOfCardInHand = currentPlayerController
            .GetHandCardsButNotTheCardIsBeingPlayed(gameStructureInfo.CardBeingPlayed).Item1.Count;
        maximumNumberOfCardsToDiscard = Math.Min(maximumNumberOfCardsToDiscard, numberOfCardInHand);

        if (IsPositive(maximumNumberOfCardsToDiscard))
            AskHowManyCardsWantsToChange();
    }

    private void AskHowManyCardsWantsToChange()
    {
        int numberOfCardsToDiscard = gameStructureInfo.View.AskHowManyCardsToDiscard(currentPlayerController.GetNameOfSuperStar(), 
            maximumNumberOfCardsToDiscard);
        
        new HandToRingSideDiscardEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardsToDiscard, gameStructureInfo);
            
        new RingToHandEffectUtils(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo, numberOfCardsToDiscard);
    }
}