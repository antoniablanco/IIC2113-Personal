using RawDeal.GameClasses;

namespace RawDeal.SuperStarClasses;

public class SuperAbilityInformation
{
    public void TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.ControllerCurrentPlayer.UseAutomaticSuperAbility();
    }

    public void UseSuperAbilityAction(GameStructureInfo gameStructureInfo)
    {   
        gameStructureInfo.BonusManager.AddOneTurnFromBonusCounter();
        gameStructureInfo.ControllerCurrentPlayer.UseElectiveSuperAbility();
    }

    public bool PlayerCanUseSuperStarAbility(GameStructureInfo gameStructureInfo) 
    {
        return gameStructureInfo.ControllerCurrentPlayer.TheirSuperStarCanUseSuperAbility(gameStructureInfo.ControllerCurrentPlayer);
    }
}