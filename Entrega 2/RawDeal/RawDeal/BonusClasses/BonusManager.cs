using System.Diagnostics;
using RawDeal.CardClasses;
using RawDeal.Exceptions;
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
            case "Superkick":
                bonusStructureInfo.SuperkickBonus = bonusValue;
                break;
            case "PowerofDarknessDamage":
                bonusStructureInfo.PowerofDarknessDamageBonus += bonusValue;
                break;
            case "PowerofDarknessFortitud":
                bonusStructureInfo.PowerofDarknessFortitudBonus += bonusValue;
                break;
            case "DontYouNeverEVER":
                bonusStructureInfo.DontYouNeverEVERBonus += bonusValue;
                break;
            case "UndertakerSitsUpDamage":
                bonusStructureInfo.UndertakerSitsUpDamageBonus += bonusValue;
                break;
            case "UndertakerSitsUpFortitud":
                bonusStructureInfo.UndertakerSitsUpFortitudBonus += bonusValue;
                break;
            case "KanesReturnDamage":
                bonusStructureInfo.KanesReturnDamageBonus += bonusValue;
                break;
            case "KanesReturnFortitud":
                bonusStructureInfo.KanesReturnFortitudBonus += bonusValue;
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
            case "Reversal":
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
            case "Clothesline":
                bonusStructureInfo.ClotheslineBonusActive = true;
                break;
            case "AtomicDrop":
                bonusStructureInfo.AtomicDropBonusActive = true;
                break;
            case "SnapMare":
                bonusStructureInfo.SnapMareBonusActive = true;
                break;
            case "GetCrowdSupport":
                bonusStructureInfo.GetCrowdSupportBonusActive = true;
                break;
            case "OpenUpaCanOfWhoopAss":
                bonusStructureInfo.OpenUpaCanOfWhoopAssBonusActive = true;
                break;
            case "SmackdownHotel":
                bonusStructureInfo.SmackdownHotelBonusActive = true;
                break;
            case "Diversion":
                bonusStructureInfo.DiversionBonusActive = true;
                break;
            case "Stagger":
                bonusStructureInfo.StaggerBonusActive = true;
                break;
            case "Ayatollah":
                bonusStructureInfo.AyatollahBonusActive = true;
                break;
        }
    }
    
    public int GetNexPlayCardDamageBonus()
    {   
        bool isAnyNextPlayCardBonusActive = bonusStructureInfo.IsJockeyingForPositionBonusDamageActive
                                            || bonusStructureInfo.IsIrishWhipBonusActive
                                            || bonusStructureInfo.ClotheslineBonusActive
                                            || bonusStructureInfo.AtomicDropBonusActive
                                            || bonusStructureInfo.SnapMareBonusActive
                                            || bonusStructureInfo.GetCrowdSupportBonusActive
                                            || bonusStructureInfo.OpenUpaCanOfWhoopAssBonusActive
                                            || bonusStructureInfo.SmackdownHotelBonusActive;
        
        return isAnyNextPlayCardBonusActive ? bonusStructureInfo.BonusDamage : 0;
    }

    public int GetTurnDamageBonus(CardController cardController)
    {   
        int damage = 0;
        if (cardController.ContainType("Maneuver"))
        {
            damage += bonusStructureInfo.IAmTheGameBonus + bonusStructureInfo.PowerofDarknessDamageBonus +
                      bonusStructureInfo.UndertakerSitsUpDamageBonus + bonusStructureInfo.KanesReturnDamageBonus +
                      bonusStructureInfo.DontYouNeverEVERBonus;
        }
        if (cardController.ContainType("Maneuver") && cardController.ContainsSubtype("Strike"))
            damage += bonusStructureInfo.HaymakerBonus;
        
        return damage;
    }
    
    
    public int EternalDamage(CardController cardController, PlayerController controllerCurrentPlayer)
    {   
        int damage = 0;
        if (DoesHasMrSockoInArsenal(controllerCurrentPlayer) && cardController.ContainType("Maneuver"))
            damage += bonusStructureInfo.MrSockoBonus;
        return damage;
    }
    

    private bool DoesHasMrSockoInArsenal(PlayerController controllerCurrentPlayer)
    {
        try
        {   
            controllerCurrentPlayer.FindCardCardFrom("RingArea", "Mr. Socko");
            return true;
        } catch (CardNotFoundException e) { return false;} 
    }
    
    
    public int GetDamageForSuccessfulManeuver(CardController cardController, int lastDamageComited)
    {   
        int damage = 0;
        if (cardController.ContainType("Maneuver") && lastDamageComited >= 5 && cardController.GetCardTitle()=="Superkick")
            damage += bonusStructureInfo.SuperkickBonus;
        return damage;
    }

    public int GetFortitudBonus(string lastTypePlayed)
    {
        int bonus = 0;
        if (bonusStructureInfo.IsJockeyingForPositionBonusFortitudActive || bonusStructureInfo.GetCrowdSupportBonusActive 
                                                                         || bonusStructureInfo.OpenUpaCanOfWhoopAssBonusActive )
            bonus += bonusStructureInfo.BonusFortitude;
        if (lastTypePlayed == "Maneuver")
            bonus += bonusStructureInfo.PowerofDarknessFortitudBonus + bonusStructureInfo.UndertakerSitsUpFortitudBonus
                + bonusStructureInfo.KanesReturnFortitudBonus;
        return bonus;
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
    
    public void CheckIfBonusesShouldBeActive(PlayerController controllerCurrentPlayer, CardController cardController, string type)
    {
        if (CheckNextPlayCardBonusConditions(controllerCurrentPlayer, cardController, type))
            DeactivateNextPlayedCardBonusEffect();
    }

    private bool CheckNextPlayCardBonusConditions(PlayerController controllerCurrentPlayer, CardController cardController, string type)
    {   
        return CheckSpecificBonusConditions(cardController, type) ||
               GetTurnCounterForBonus() <= 0 ||
               (GetWhoActivateNextPlayedCardBonusEffect() != controllerCurrentPlayer &&
                GetWhoActivateNextPlayedCardBonusEffect() != null);
    }

    private bool CheckSpecificBonusConditions(CardController cardController, string type)
    {
        if (bonusStructureInfo.IsJockeyingForPositionBonusDamageActive ||
            bonusStructureInfo.IsJockeyingForPositionBonusFortitudActive)
            return !cardController.ContainsSubtype("Grapple");
        
        if (bonusStructureInfo.IsIrishWhipBonusActive)
            return !cardController.ContainsSubtype("Strike");

        if (bonusStructureInfo.ClotheslineBonusActive || bonusStructureInfo.AtomicDropBonusActive 
                                                      || bonusStructureInfo.GetCrowdSupportBonusActive 
                                                      || bonusStructureInfo.OpenUpaCanOfWhoopAssBonusActive 
                                                      || bonusStructureInfo.SmackdownHotelBonusActive)
            return !(type == "Maneuver");

        if (bonusStructureInfo.SnapMareBonusActive)
            return !(type == "Maneuver" && cardController.ContainsSubtype("Strike"));
        
        return false;
    }
    
    private void DeactivateNextPlayedCardBonusEffect()
    {   
        DeactivateBonus("JockeyingFortitud");
        DeactivateBonus("JockeyingDamage");
        DeactivateBonus("IrishWhip");
        DeactivateBonus("Clothesline");
        DeactivateBonus("AtomicDrop");
        DeactivateBonus("SnapMare");
        DeactivateBonus("GetCrowdSupport");
        DeactivateBonus("OpenUpaCanOfWhoopAss");
        DeactivateBonus("SmackdownHotel");
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
            case "Clothesline":
                bonusStructureInfo.ClotheslineBonusActive = false;
                break;
            case "AtomicDrop":
                bonusStructureInfo.AtomicDropBonusActive = false;
                break;
            case "SnapMare":
                bonusStructureInfo.SnapMareBonusActive = false;
                break;
            case "GetCrowdSupport":
                bonusStructureInfo.GetCrowdSupportBonusActive = false;
                break;
            case "OpenUpaCanOfWhoopAss":
                bonusStructureInfo.OpenUpaCanOfWhoopAssBonusActive = false;
                break;
            case "SmackdownHotel":
                bonusStructureInfo.SmackdownHotelBonusActive = false;
                break;
            case "Diversion":
                bonusStructureInfo.DiversionBonusActive = false;
                break;
            case "Stagger":
                bonusStructureInfo.StaggerBonusActive = false;
                break;
            case "Ayatollah":
                bonusStructureInfo.AyatollahBonusActive = false;
                break;
        }
    }
    
    public void CheckIfReversalBonusShouldBeActive(PlayerController controllerCurrentPlayer)
    {
        if (GetTurnCounterForBonus() <= 0 ||
            (GetWhoActivateNextPlayedCardBonusEffect() != controllerCurrentPlayer &&
             GetWhoActivateNextPlayedCardBonusEffect() != null))
        {
            DeactivateBonus("Diversion");
            DeactivateBonus("Stagger");
        }
    }

    public bool CanReversal( GameStructureInfo gameStructureInfo, string reverseBy,
        int totalDamage)
    {
        if (bonusStructureInfo.DiversionBonusActive)
            return !(gameStructureInfo.CardBeingPlayedType == "Maneuver");
        if (bonusStructureInfo.StaggerBonusActive)
            return !(gameStructureInfo.CardBeingPlayedType == "Maneuver" && totalDamage <= 7);
        if (bonusStructureInfo.AyatollahBonusActive)
            return (reverseBy == "Hand");
        return true;
    }

    public void DeactivateTurnBonus(PlayerController controllerCurrentPlayer)
    {   
        bonusStructureInfo.IAmTheGameBonus = 0;
        bonusStructureInfo.HaymakerBonus = 0;
        bonusStructureInfo.SuperkickBonus = 0;
        bonusStructureInfo.PowerofDarknessDamageBonus = 0;
        bonusStructureInfo.PowerofDarknessFortitudBonus = 0;
        DeactivateBonus("Ayatollah");
        if (bonusStructureInfo.WhoActivateNextPlayedCardBonusEffect != null)
            if (controllerCurrentPlayer == bonusStructureInfo.WhoActivateNextPlayedCardBonusEffect)
            {   
                bonusStructureInfo.UndertakerSitsUpDamageBonus = 0;
                bonusStructureInfo.UndertakerSitsUpFortitudBonus = 0;
                bonusStructureInfo.KanesReturnDamageBonus = 0;
                bonusStructureInfo.KanesReturnFortitudBonus = 0;
                bonusStructureInfo.DontYouNeverEVERBonus = 0;
            }
    }
    
}