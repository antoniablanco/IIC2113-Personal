namespace RawDeal.GameClasses;

public class BonusManager
{   
    
    private GameStructureInfo gameStructureInfo = new GameStructureInfo();

    public BonusManager(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }
    
    public void ActivateJockeyingForPositionBonusFortitud()
    {
        gameStructureInfo.IsJockeyingForPositionBonusFortitudActive = 1;
    }
    
    public void ActivateJockeyingForPositionBonusDamage()
    {
        gameStructureInfo.IsJockeyingForPositionBonusDamageActive = 1;
    }
    
    public void DesactivateJockeyingForPositionBonusFortitud()
    {
        gameStructureInfo.IsJockeyingForPositionBonusFortitudActive = 0;
    }
    
    public void DesactivateJockeyingForPositionBonusDamage()
    {
        gameStructureInfo.IsJockeyingForPositionBonusDamageActive = 0;
    }

    public int AddBonusFortitud()
    {
        return gameStructureInfo.BonusFortitude * gameStructureInfo.IsJockeyingForPositionBonusFortitudActive;
    }

    public int AddBonusDamage()
    {
        return gameStructureInfo.BonusDamage * gameStructureInfo.IsJockeyingForPositionBonusDamageActive;
    }
    
    public void AddingOneTurnJockeyingForPosition()
    {
        gameStructureInfo.TurnCounterForJokeyingForPosition += 1;
    }
    
    public void RemoveOneTurnFromJockeyingForPosition()
    {
        gameStructureInfo.TurnCounterForJokeyingForPosition -= 1;
    }

}