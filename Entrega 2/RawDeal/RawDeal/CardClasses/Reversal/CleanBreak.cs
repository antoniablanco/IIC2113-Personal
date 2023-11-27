using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Reversal;

public class CleanBreak : Card
{
    public CleanBreak(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totalDamage)
    {
        return playedCardController.GetCardTitle() == "Jockeying for Position" &&
               gameStructureInfo.BonusManager.CanReversal(gameStructureInfo, reverseBy, totalDamage);
    }

    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        const int numberOfCardToDiscard = 4;
        new DiscardCardsFromHandToRingSideEffect(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardToDiscard, gameStructureInfo);

        const int numberOfCardsToSteal = 1;
        
        new DrawCardEffect(gameStructureInfo.ControllerOpponentPlayer, 
            gameStructureInfo).StealCards(numberOfCardsToSteal);
    }
}