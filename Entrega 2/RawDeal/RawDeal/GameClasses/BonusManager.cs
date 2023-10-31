namespace RawDeal.GameClasses;

public class BonusManager
{
    private BonusStructureInfo bonusStructureInfo;

    public BonusManager(BonusStructureInfo bonusStructureInfo)
    {
        this.bonusStructureInfo = bonusStructureInfo;
    }

    public void ApplyBonusEffect(string typeName, int bonusValue, string bonusType)
    {
        SetBonus(bonusType, bonusValue);
        ActivateBonus(typeName);
    }

    private void SetBonus(string type, int bonus)
    {
        switch (type)
        {
            case "Fortitud":
                bonusStructureInfo.BonusFortitude = bonus;
                break;
            case "Damage":
                bonusStructureInfo.BonusDamage = bonus;
                break;
        }
    }

    private void ActivateBonus(string type)
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

    public int GetDamageBonus()
    {
        if (bonusStructureInfo.IsJockeyingForPositionBonusDamageActive==1)
            return bonusStructureInfo.BonusDamage;
        return 0;
    }

    public int GetFortitudBonus()
    {
        if (bonusStructureInfo.IsJockeyingForPositionBonusFortitudActive==1)
            return bonusStructureInfo.BonusFortitude;
        return 0;
    }

    public void AddingOneTurnJockeyingForPosition()
    {
        bonusStructureInfo.turnsLeftForBonusCounter += 1;
    }

    public void RemoveOneTurnFromJockeyingForPosition()
    {
        bonusStructureInfo.turnsLeftForBonusCounter -= 1;
    }

    public int GetTurnCounterForJokeyingForPosition()
    {
        return bonusStructureInfo.turnsLeftForBonusCounter;
    }

    public void SetTurnsLeftForBonusCounter(int turnsBeforeEffectExpires)
    {
        bonusStructureInfo.turnsLeftForBonusCounter = turnsBeforeEffectExpires;
    }
}