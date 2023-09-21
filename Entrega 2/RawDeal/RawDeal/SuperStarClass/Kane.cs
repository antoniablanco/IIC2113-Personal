namespace RawDeal.SuperStarClases;
using RawDealView;

public class Kane: SuperStar 
{
    public Kane(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingAutomaticSuperAbilityAtTheStartOfTheTurn(PlayerController currentPlayer, PlayerController opponentPlayer)
    {   
        Card flippedCard = opponentPlayer.TranferUnselectedCardFromArsenalToRingSide();
        string flippedCardString = VisualizeCards.GetStringCardInfo(flippedCard);
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        _view.SayThatSuperstarWillTakeSomeDamage(opponentPlayer.NameOfSuperStar(), 1);
        _view.ShowCardOverturnByTakingDamage(flippedCardString, 1, 1);
    }
}