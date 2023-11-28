using RawDeal.GameClasses;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public class Mankind: SuperStar
{
    public Mankind(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        
    }

    public override void UseAutomaticSuperAbilityAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {
        if (gameStructureInfo.ControllerCurrentPlayer.GetNumberOfCardIn("Arsenal") > 0) 
            gameStructureInfo.ControllerCurrentPlayer.DrawCard();
    }
}