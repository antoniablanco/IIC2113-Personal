namespace RawDeal.SuperStarClases;
using RawDealView;

public class StoneCold: SuperStar
{
    public StoneCold(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingElectiveSuperAbility(PlayerController currentPlayer, PlayerController opponentPlayer)
    {
        currentPlayer.TheSuperStarHasUsedHisSuperAbilityThisTurn();
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        
        AddingCardFromArsenalToHand(currentPlayer);
        DiscardingCardsFromHandToArsenal(currentPlayer);
    }

    private void AddingCardFromArsenalToHand(PlayerController currentPlayer)
    {
        _view.SayThatPlayerDrawCards(Name, 1);
        currentPlayer.TranferUnselectedCardFromArsenalToHand();
    }

    private void DiscardingCardsFromHandToArsenal(PlayerController currentPlayer)
    {
        List<string> handCardsAsString = currentPlayer.StringCardsHand();
        int selectedCard = _view.AskPlayerToReturnOneCardFromHisHandToHisArsenal(Name, handCardsAsString);
        
        Card discardedCard = currentPlayer.GetSpecificCardFromHand(selectedCard);
        currentPlayer.TransferChoosinCardFromHandToArsenal(discardedCard, "Start");
    }
    
    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return (currentPlayer.NumberOfCardsInTheArsenal() > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn());
    }
}