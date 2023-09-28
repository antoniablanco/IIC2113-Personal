namespace RawDeal.SuperStarClass;
using RawDealView;

public class TheRock: SuperStar
{
    public TheRock(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingAutomaticSuperAbilityAtTheStartOfTheTurn(PlayerController currentPlayer, PlayerController opponentPlayer)
    {  
        if (CanUseSuperAbility(currentPlayer))
        {
            currentPlayer.TheSuperStarHasUsedHisSuperAbilityThisTurn();
            if (_view.DoesPlayerWantToUseHisAbility(Name))
            {
                AddingCardFromRingSideToArsenal(currentPlayer);
            }
        }
    }
    
    private void AddingCardFromRingSideToArsenal(PlayerController currentPlayer)
    {
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        List<string> ringAreaAsString = currentPlayer.StringCardsRingSide();
        int selectedCardIndex = _view.AskPlayerToSelectCardsToRecover(Name, 1, ringAreaAsString);
        CardController discardedCardController = currentPlayer.GetSpecificCardFromRingSide(selectedCardIndex);
        currentPlayer.TransferChoosinCardFromRingSideToArsenal(discardedCardController,  "Start");
    }
    
    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return currentPlayer.NumberOfCardsInRingSide() > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn();
    }
}