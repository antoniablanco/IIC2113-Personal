using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView.Options;

namespace RawDeal.EffectsClasses;

public class DrawOrForceToDiscardEffect: EffectsUtils
{   
    private int numberOfCardsToSteal;
    private int numberOfCardsToDiscard;
    private bool shouldAsk;
    
    public DrawOrForceToDiscardEffect(GameStructureInfo gameStructureInfo, int numberOfCardsToSteal, int maximumNumberOfCardsToDiscard, bool shouldAsk=false)

        : base(gameStructureInfo)
    {
        this.shouldAsk = shouldAsk;
        this.numberOfCardsToSteal = numberOfCardsToSteal;
        numberOfCardsToDiscard = maximumNumberOfCardsToDiscard;
        AskDrawOrForceToDiscar();
    }
    
    private void AskDrawOrForceToDiscar()
    {
        SelectedEffect selectedEffect = gameStructureInfo.View.AskUserToChooseBetweenDrawingOrForcingOpponentToDiscardCards(
            gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar());
        
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
            new DrawCardEffect(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetCurrentPlayer(), 
                gameStructureInfo).MayStealCards(numberOfCardsToSteal);
        else
            new DrawCardEffect(gameStructureInfo.ControllerCurrentPlayer,gameStructureInfo.GetCurrentPlayer(), 
                gameStructureInfo).StealCards(numberOfCardsToSteal);
    }

    private void ForceOpponentToDiscard()
    {
        new DiscardCardsFromHandToRingSideEffect(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo.ControllerOpponentPlayer, numberOfCardsToDiscard, gameStructureInfo);
    }
}