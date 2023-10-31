using System.Diagnostics;
using RawDeal.CardClasses;
using RawDeal.PlayerClasses;

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
    
    public void DeactivateNextPlayedCardBonusEffect()
    {
        DeactivateBonus("JockeyingFortitud");
        DeactivateBonus("JockeyingDamage");
    }

    private void DeactivateBonus(string type)
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

    public void AddingOneTurnFromBonusCounter()
    {
        bonusStructureInfo.turnsLeftForBonusCounter += 1;
    }

    public void RemoveOneTurnFromBonusCounter()
    {
        bonusStructureInfo.turnsLeftForBonusCounter -= 1;
    }

    private int GetTurnCounterForBonus()
    {
        return bonusStructureInfo.turnsLeftForBonusCounter;
    }

    public void SetTurnsLeftForBonusCounter(int turnsBeforeEffectExpires)
    {
        bonusStructureInfo.turnsLeftForBonusCounter = turnsBeforeEffectExpires;
    }

    private PlayerController GetWhoActivateNextPlayedCardBonusEffect()
    {
        return bonusStructureInfo.WhoActivateNextPlayedCardBonusEffect;
    }
    
    public void SetWhoActivateNextPlayedCardBonusEffect(PlayerController playerController)
    {
        bonusStructureInfo.WhoActivateNextPlayedCardBonusEffect = playerController;
    }
    
    public bool CheckNextPlayCardBonusConditions(PlayerController controllerCurrentPlayer, CardController cardController)
    {
        return CheckSpecificBonusConditions(cardController) ||
               GetTurnCounterForBonus() <= 0 ||
               (GetWhoActivateNextPlayedCardBonusEffect() != controllerCurrentPlayer &&
                GetWhoActivateNextPlayedCardBonusEffect() != null);
    }

    private bool CheckSpecificBonusConditions(CardController cardController)
    {   if (bonusStructureInfo.IsJockeyingForPositionBonusDamageActive==1 ||
            bonusStructureInfo.IsJockeyingForPositionBonusFortitudActive==1)
            return !cardController.ContainsSubtype("Grapple");
        return false;
    }

}