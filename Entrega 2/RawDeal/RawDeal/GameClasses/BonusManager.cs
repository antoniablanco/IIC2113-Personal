namespace RawDeal.GameClasses;

public class BonusManager
{
    private GameStructureInfo gameStructureInfo = new GameStructureInfo();

    public BonusManager(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }

    public void ActivateBonus(string type)
    {
        switch (type)
        {
            case "JockeyingFortitud":
                gameStructureInfo.IsJockeyingForPositionBonusFortitudActive = 1;
                break;
            case "JockeyingDamage":
                gameStructureInfo.IsJockeyingForPositionBonusDamageActive = 1;
                break;
        }
    }

    public void DeactivateBonus(string type)
    {
        switch (type)
        {
            case "JockeyingFortitud":
                gameStructureInfo.IsJockeyingForPositionBonusFortitudActive = 0;
                break;
            case "JockeyingDamage":
                gameStructureInfo.IsJockeyingForPositionBonusDamageActive = 0;
                break;
        }
    }

    public int AddBonus(string type)
    {
        return type switch 
        {
            "JockeyingFortitud" => gameStructureInfo.BonusFortitude * gameStructureInfo.IsJockeyingForPositionBonusFortitudActive,
            "JockeyingDamage" => gameStructureInfo.BonusDamage * gameStructureInfo.IsJockeyingForPositionBonusDamageActive,
            _ => 0
        };
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