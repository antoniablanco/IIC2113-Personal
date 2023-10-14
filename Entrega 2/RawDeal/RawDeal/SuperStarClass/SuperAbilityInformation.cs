namespace RawDeal.SuperStarClass;

public class SuperAbilityInformation
{
    
    public void TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.ControllerCurrentPlayer.UsingAutomaticSuperAbility();
    }
    
    public void ActionUseSuperAbility(GameStructureInfo gameStructureInfo)
    {   
        gameStructureInfo.ContadorTurnosJokeyingForPosition += 1;
        gameStructureInfo.ControllerCurrentPlayer.UsingElectiveSuperAbility();
    }
    
    public bool PlayerCanUseSuperStarAbility(GameStructureInfo gameStructureInfo) 
    {
        return gameStructureInfo.ControllerCurrentPlayer.TheirSuperStarCanUseSuperAbility(gameStructureInfo.ControllerCurrentPlayer);
    }
}