using RawDeal.GameClasses;

namespace RawDeal.SuperStarClasses;

public class SuperAbilityInformation
{
    public void TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.ControllerCurrentPlayer.UsingAutomaticSuperAbility();
    }

    public void UseSuperAbilityAction(GameStructureInfo gameStructureInfo)
    {   
        gameStructureInfo.BonusManager.AddingOneTurnJockeyingForPosition();
        gameStructureInfo.ControllerCurrentPlayer.UsingElectiveSuperAbility();
    }

    public bool PlayerCanUseSuperStarAbility(GameStructureInfo gameStructureInfo) 
    {
        return gameStructureInfo.ControllerCurrentPlayer.TheirSuperStarCanUseSuperAbility(gameStructureInfo.ControllerCurrentPlayer);
    }
}