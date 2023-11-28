using RawDeal.GameClasses;

namespace RawDeal.SuperStarClasses;

public class SuperAbilityInformation
{
    public void UseStartOfTurnSuperAbility(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.ControllerCurrentPlayer.UseAutomaticSuperAbility();
    }

    public void UseSuperAbilityAction(GameStructureInfo gameStructureInfo)
    {   
        gameStructureInfo.BonusManager.AddOneTurnFromBonusCounter();
        gameStructureInfo.ControllerCurrentPlayer.UseElectiveSuperAbility();
    }

    public bool CanPlayerUseSuperStarAbility(GameStructureInfo gameStructureInfo) 
    {
        return gameStructureInfo.ControllerCurrentPlayer.CheckIfSuperStarCanUseSuperAbility(gameStructureInfo.ControllerCurrentPlayer);
    }
}