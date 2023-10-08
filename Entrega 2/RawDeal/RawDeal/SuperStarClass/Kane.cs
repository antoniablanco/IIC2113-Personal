namespace RawDeal.SuperStarClass;
using RawDealView;

public class Kane: SuperStar 
{
    public Kane(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingAutomaticSuperAbilityAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {   
        CardController flippedCardController = gameStructureInfo.ControllerOpponentPlayer.TranferUnselectedCardFromArsenalToRingSide();
        string flippedCardString = VisualizeCards.GetStringCardInfo(flippedCardController);
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        _view.SayThatSuperstarWillTakeSomeDamage(gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), 1);
        _view.ShowCardOverturnByTakingDamage(flippedCardString, 1, 1);
    }
}