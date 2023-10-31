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

    public void ApplyTurnBonusEffect(string typeName, int bonusValue)
    {
        switch (typeName)
        {
            case "IAmTheGame":
                bonusStructureInfo.IAmTheGameBonus += bonusValue;
                break;
            case "Haymaker":
                bonusStructureInfo.HaymakerBonus += bonusValue;
                break;
        }
    }
    
    public void ApplyNextPlayedCardBonusEffect(string typeName, int bonusValue, string bonusType)
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
                bonusStructureInfo.IsJockeyingForPositionBonusFortitudActive = true;
                break;
            case "JockeyingDamage":
                bonusStructureInfo.IsJockeyingForPositionBonusDamageActive = true;
                break;
            case "IrishWhip":
                bonusStructureInfo.IsIrishWhipBonusActive = true;
                break;
        }
    }
    
    public int GetNexPlayCardDamageBonus()
    {
        if (bonusStructureInfo.IsJockeyingForPositionBonusDamageActive || bonusStructureInfo.IsIrishWhipBonusActive)
            return bonusStructureInfo.BonusDamage;
        return 0;
    }

    public int GetTurnDamageBonus(CardController cardController)
    {
        int damage = 0;
        if (cardController.VerifyIfTheLastPlayedTypeIs("Maneuver"))
            damage += bonusStructureInfo.IAmTheGameBonus;
        return damage;
    }
    
    public int GetDamageForSuccessfulManeuver(CardController cardController)
    {   
        int damage = 0;
        if (cardController.VerifyIfTheLastPlayedTypeIs("Maneuver") && cardController.ContainsSubtype("Strike"))
            damage += bonusStructureInfo.HaymakerBonus;
        return damage;
    }

    public int GetFortitudBonus()
    {
        if (bonusStructureInfo.IsJockeyingForPositionBonusFortitudActive)
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
    
    public void SetTurnsLeftForBonusCounter(int turnsBeforeEffectExpires)
    {
        bonusStructureInfo.turnsLeftForBonusCounter = turnsBeforeEffectExpires;
    }
    
    public int GetTurnCounterForBonus()
    {
        return bonusStructureInfo.turnsLeftForBonusCounter;
    }
    
    public void SetWhoActivateNextPlayedCardBonusEffect(PlayerController playerController)
    {
        bonusStructureInfo.WhoActivateNextPlayedCardBonusEffect = playerController;
    }
    
    public PlayerController GetWhoActivateNextPlayedCardBonusEffect()
    {
        return bonusStructureInfo.WhoActivateNextPlayedCardBonusEffect;
    }
    
    public void CheckIfBonusesShouldBeActive(PlayerController controllerCurrentPlayer, CardController cardController)
    {
        if (CheckNextPlayCardBonusConditions(controllerCurrentPlayer, cardController))
            DeactivateNextPlayedCardBonusEffect();
    }

    private bool CheckNextPlayCardBonusConditions(PlayerController controllerCurrentPlayer, CardController cardController)
    {
        return CheckSpecificBonusConditions(cardController) ||
               GetTurnCounterForBonus() <= 0 ||
               (GetWhoActivateNextPlayedCardBonusEffect() != controllerCurrentPlayer &&
                GetWhoActivateNextPlayedCardBonusEffect() != null);
    }

    private bool CheckSpecificBonusConditions(CardController cardController)
    {   if (bonusStructureInfo.IsJockeyingForPositionBonusDamageActive ||
            bonusStructureInfo.IsJockeyingForPositionBonusFortitudActive)
            return !cardController.ContainsSubtype("Grapple");
        if (bonusStructureInfo.IsIrishWhipBonusActive)
            return !cardController.ContainsSubtype("Strike");
        return false;
    }

    private void DeactivateNextPlayedCardBonusEffect()
    {
        DeactivateBonus("JockeyingFortitud");
        DeactivateBonus("JockeyingDamage");
        DeactivateBonus("IrishWhip");
    }

    public void DeactivateTurnBonus()
    {
        bonusStructureInfo.IAmTheGameBonus = 0;
        bonusStructureInfo.HaymakerBonus = 0;
    }
    
    private void DeactivateBonus(string type)
    {
        switch (type)
        {
            case "JockeyingFortitud":
                bonusStructureInfo.IsJockeyingForPositionBonusFortitudActive = false;
                break;
            case "JockeyingDamage":
                bonusStructureInfo.IsJockeyingForPositionBonusDamageActive = false;
                break;
            case "IrishWhip":
                bonusStructureInfo.IsIrishWhipBonusActive = false;
                break;
        }
    }
    
}