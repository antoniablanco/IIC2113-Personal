using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView.Options;

namespace RawDeal.EffectsClasses;

public class DrawOrForceToDiscardEffect: EffectsUtils
{   
    private int numberOfCardsToSteal;
    private int numberOfCardsToDiscard;
    private bool shouldAsk;
    
    public DrawOrForceToDiscardEffect(GameStructureInfo gameStructureInfo, int numberOfCardsToSteal, 
        int maximumNumberOfCardsToDiscard, bool shouldAsk=false)

        : base(gameStructureInfo)
    {
        this.shouldAsk = shouldAsk;
        this.numberOfCardsToSteal = numberOfCardsToSteal;
        numberOfCardsToDiscard = maximumNumberOfCardsToDiscard;
        AskDrawOrForceToDiscard();
    }
    
    private void AskDrawOrForceToDiscard()
    {
        SelectedEffect selectedEffect = gameStructureInfo.View.AskUserToChooseBetweenDrawingOrForcingOpponentToDiscardCards(
            gameStructureInfo.ControllerCurrentPlayer.GetNameOfSuperStar());
        
        switch (selectedEffect)
        {
            case SelectedEffect.DrawCards:
                DrawCards();
                break;
            case SelectedEffect.ForceOpponentToDiscard:
                ForceOpponentToDiscard();
                break;
        }
    }

    private void DrawCards()
    {
        if (shouldAsk)
            new CardDrawEffect(gameStructureInfo.ControllerCurrentPlayer, 
                gameStructureInfo).MayStealCards(numberOfCardsToSteal);
        else
            new CardDrawEffect(gameStructureInfo.ControllerCurrentPlayer, 
                gameStructureInfo).StealCards(numberOfCardsToSteal);
    }

    private void ForceOpponentToDiscard()
    {
        new HandToRingSideDiscardEffect(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo.ControllerOpponentPlayer, numberOfCardsToDiscard, gameStructureInfo);
    }
}