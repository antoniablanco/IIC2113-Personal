namespace RawDeal.SuperStarClases;
using RawDealView;

public class TheRock: SuperStar
{
    public TheRock(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingAutomaticSuperAbilityAtTheStartOfTheTurn(Player currentPlayer, Player opponentPlayer)
    {  
        if (CanUseSuperAbility(currentPlayer))
        {   
            currentPlayer.theHabilityHasBeenUsedThisTurn = true;
            if (_view.DoesPlayerWantToUseHisAbility(Name))
            {
                AddingCardFromRingSideToArsenal(currentPlayer);
            }
        }
    }
    
    private void AddingCardFromRingSideToArsenal(Player currentPlayer)
    {
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        List<string> ringAreaAsString = VisualizeCards.CreateStringCardList(currentPlayer.cardsRingSide);
        int selectedCardIndex = _view.AskPlayerToSelectCardsToRecover(Name, 1, ringAreaAsString);
        Card discardedCard = currentPlayer.cardsRingSide[selectedCardIndex];
        currentPlayer.CardTransferChoosingWhichOneToChange(discardedCard, currentPlayer.cardsRingSide, currentPlayer.cardsArsenal, "Start");
    }
    
    public override bool CanUseSuperAbility(Player currentPlayer)
    {
        return currentPlayer.cardsRingSide.Count > 0 && !currentPlayer.theHabilityHasBeenUsedThisTurn;
    }
}