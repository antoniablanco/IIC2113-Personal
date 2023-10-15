using RawDeal.GameClasses;

namespace RawDeal.SuperStarClasses;

public class SuperAbilityInformation
{
    
    public void TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.ControllerCurrentPlayer.UsingAutomaticSuperAbility();
    }
    
    public void ActionUseSuperAbility(GameStructureInfo gameStructureInfo)
    {   
        gameStructureInfo.GetSetGameVariables.AddingOneTurnJockeyingForPosition();
        gameStructureInfo.ControllerCurrentPlayer.UsingElectiveSuperAbility();
    }
    
    public bool PlayerCanUseSuperStarAbility(GameStructureInfo gameStructureInfo) 
    {
        return gameStructureInfo.ControllerCurrentPlayer.TheirSuperStarCanUseSuperAbility(gameStructureInfo.ControllerCurrentPlayer);
    }
}