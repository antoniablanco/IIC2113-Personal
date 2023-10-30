namespace RawDeal.GameClasses;

public class BonusManager
{
    private BonusStructureInfo bonusStructureInfo;

    public BonusManager(BonusStructureInfo bonusStructureInfo)
    {
        this.bonusStructureInfo = bonusStructureInfo;
    }

    public void ActivateBonus(string type)
    {
        switch (type)
        {
            case "JockeyingFortitud":
                bonusStructureInfo.IsJockeyingForPositionBonusFortitudActive = 1;
                break;
            case "JockeyingDamage":
                bonusStructureInfo.IsJockeyingForPositionBonusDamageActive = 1;
                break;
        }
    }

    public void DeactivateBonus(string type)
    {
        switch (type)
        {
            case "JockeyingFortitud":
                bonusStructureInfo.IsJockeyingForPositionBonusFortitudActive = 0;
                break;
            case "JockeyingDamage":
                bonusStructureInfo.IsJockeyingForPositionBonusDamageActive = 0;
                break;
        }
    }

    public int AddBonus(string type)
    {
        return type switch 
        {
            "JockeyingFortitud" => bonusStructureInfo.BonusFortitude * bonusStructureInfo.IsJockeyingForPositionBonusFortitudActive,
            "JockeyingDamage" => bonusStructureInfo.BonusDamage * bonusStructureInfo.IsJockeyingForPositionBonusDamageActive,
            _ => 0
        };
    }

    public void AddingOneTurnJockeyingForPosition()
    {
        bonusStructureInfo.TurnCounterForJokeyingForPosition += 1;
    }

    public void RemoveOneTurnFromJockeyingForPosition()
    {
        bonusStructureInfo.TurnCounterForJokeyingForPosition -= 1;
    }

    public int GetTurnCounterForJokeyingForPosition()
    {
        return bonusStructureInfo.TurnCounterForJokeyingForPosition;
    }
}