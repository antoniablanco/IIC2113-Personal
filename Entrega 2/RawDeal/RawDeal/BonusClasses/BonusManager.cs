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
    
    public void ApplyTurnBonusEffect(BonusEnum.CardBonusName cardBonusName, int bonusValue)
    {   
        switch (cardBonusName)
        {
            case BonusEnum.CardBonusName.IAmTheGame:
                bonusStructureInfo.IAmTheGameBonus += bonusValue;
                break;
            case BonusEnum.CardBonusName.Haymaker:
                bonusStructureInfo.HaymakerBonus += bonusValue;
                break;
            case BonusEnum.CardBonusName.Superkick:
                bonusStructureInfo.SuperkickBonus = bonusValue;
                break;
            case BonusEnum.CardBonusName.PowerofDarknessDamage:
                bonusStructureInfo.PowerofDarknessDamageBonus += bonusValue;
                break;
            case BonusEnum.CardBonusName.PowerofDarknessFortitud:
                bonusStructureInfo.PowerofDarknessFortitudBonus += bonusValue;
                break;
            case BonusEnum.CardBonusName.DontYouNeverEVER:
                bonusStructureInfo.DontYouNeverEVERBonus += bonusValue;
                break;
            case BonusEnum.CardBonusName.UndertakerSitsUpDamage:
                bonusStructureInfo.UndertakerSitsUpDamageBonus += bonusValue;
                break;
            case BonusEnum.CardBonusName.UndertakerSitsUpFortitud:
                bonusStructureInfo.UndertakerSitsUpFortitudBonus += bonusValue;
                break;
            case BonusEnum.CardBonusName.KanesReturnDamage:
                bonusStructureInfo.KanesReturnDamageBonus += bonusValue;
                break;
            case BonusEnum.CardBonusName.KanesReturnFortitud:
                bonusStructureInfo.KanesReturnFortitudBonus += bonusValue;
                break;
        }
    }
    
    public void ApplyNextPlayedCardBonusEffect(BonusEnum.CardBonusName cardName, int bonusValue, BonusEnum.CardBonusType bonusType)
    {  
        SetBonus(bonusType, bonusValue);
        ActivateBonus(cardName);
    }
    
    private void SetBonus(BonusEnum.CardBonusType type, int bonus)
    {
        switch (type)
        {
            case BonusEnum.CardBonusType.Fortitude:
                bonusStructureInfo.BonusFortitude = bonus;
                break;
            case BonusEnum.CardBonusType.Damage:
                bonusStructureInfo.BonusDamage = bonus;
                break;
            case BonusEnum.CardBonusType.Reversal:
                break;
        }
    }
    
    private void ActivateBonus(BonusEnum.CardBonusName name)
    {
        switch (name)
        {
            case BonusEnum.CardBonusName.JockeyingFortitud:
                bonusStructureInfo.IsJockeyingForPositionBonusFortitudeActive = true;
                break;
            case BonusEnum.CardBonusName.JockeyingDamage:
                bonusStructureInfo.IsJockeyingForPositionBonusDamageActive = true;
                break;
            case BonusEnum.CardBonusName.IrishWhip:
                bonusStructureInfo.IsIrishWhipBonusActive = true;
                break;
            case BonusEnum.CardBonusName.Clothesline:
                bonusStructureInfo.ClotheslineBonusActive = true;
                break;
            case BonusEnum.CardBonusName.AtomicDrop:
                bonusStructureInfo.AtomicDropBonusActive = true;
                break;
            case BonusEnum.CardBonusName.SnapMare:
                bonusStructureInfo.SnapMareBonusActive = true;
                break;
            case BonusEnum.CardBonusName.GetCrowdSupport:
                bonusStructureInfo.GetCrowdSupportBonusActive = true;
                break;
            case BonusEnum.CardBonusName.OpenUpaCanOfWhoopAss:
                bonusStructureInfo.OpenUpaCanOfWhoopAssBonusActive = true;
                break;
            case BonusEnum.CardBonusName.SmackdownHotel:
                bonusStructureInfo.SmackdownHotelBonusActive = true;
                break;
            case BonusEnum.CardBonusName.Diversion:
                bonusStructureInfo.DiversionBonusActive = true;
                break;
            case BonusEnum.CardBonusName.Stagger:
                bonusStructureInfo.StaggerBonusActive = true;
                break;
            case BonusEnum.CardBonusName.Ayatollah:
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
        if (cardController.DoesTheCardContainType("Maneuver"))
        {
            damage += bonusStructureInfo.IAmTheGameBonus + bonusStructureInfo.PowerofDarknessDamageBonus +
                      bonusStructureInfo.UndertakerSitsUpDamageBonus + bonusStructureInfo.KanesReturnDamageBonus +
                      bonusStructureInfo.DontYouNeverEVERBonus;
        }
        if (cardController.DoesTheCardContainType("Maneuver") && cardController.DoesTheCardContainsSubtype("Strike"))
            damage += bonusStructureInfo.HaymakerBonus;
        
        return damage;
    }
    
    public int GetEternalDamage(CardController cardController, PlayerController controllerCurrentPlayer)
    {   
        int damage = 0;
        if (DoesHaveMrSockoInArsenal(controllerCurrentPlayer) && cardController.DoesTheCardContainType("Maneuver"))
            damage += bonusStructureInfo.MrSockoBonus;
        return damage;
    }

    private bool DoesHaveMrSockoInArsenal(PlayerController controllerCurrentPlayer)
    {
        try
        {   
            controllerCurrentPlayer.GetCardInDeckByName("RingArea", "Mr. Socko");
            return true;
        } catch (CardNotFoundException e) { return false;} 
    }
    
    public int GetDamageForSuccessfulManeuver(CardController cardController, int lastDamageCommitted)
    {   
        int damage = 0;
        if (CheckConditionsForSuperKickBonus(cardController, lastDamageCommitted))
            damage += bonusStructureInfo.SuperkickBonus;
        return damage;
    }

    private bool CheckConditionsForSuperKickBonus(CardController cardController, int lastDamageCommitted)
    {
        return cardController.DoesTheCardContainType("Maneuver") && lastDamageCommitted >= 5 &&
               cardController.GetCardTitle() == "Superkick";
    }

    public int GetFortitudeBonus(string lastTypePlayed)
    {
        int bonus = 0;
        if (bonusStructureInfo.IsJockeyingForPositionBonusFortitudeActive || bonusStructureInfo.GetCrowdSupportBonusActive 
                                                                         || bonusStructureInfo.OpenUpaCanOfWhoopAssBonusActive )
            bonus += bonusStructureInfo.BonusFortitude;
        if (lastTypePlayed == "Maneuver")
            bonus += bonusStructureInfo.PowerofDarknessFortitudBonus + bonusStructureInfo.UndertakerSitsUpFortitudBonus
                + bonusStructureInfo.KanesReturnFortitudBonus;
        return bonus;
    }

    public void AddOneTurnFromBonusCounter()
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
            bonusStructureInfo.IsJockeyingForPositionBonusFortitudeActive)
            return !cardController.DoesTheCardContainsSubtype("Grapple");
        
        if (bonusStructureInfo.IsIrishWhipBonusActive)
            return !cardController.DoesTheCardContainsSubtype("Strike");

        if (bonusStructureInfo.ClotheslineBonusActive || bonusStructureInfo.AtomicDropBonusActive 
                                                      || bonusStructureInfo.GetCrowdSupportBonusActive 
                                                      || bonusStructureInfo.OpenUpaCanOfWhoopAssBonusActive 
                                                      || bonusStructureInfo.SmackdownHotelBonusActive)
            return type != "Maneuver";

        if (bonusStructureInfo.SnapMareBonusActive)
            return !(type == "Maneuver" && cardController.DoesTheCardContainsSubtype("Strike"));
        
        return false;
    }
    
    private void DeactivateNextPlayedCardBonusEffect()
    {   
        DeactivateBonus(BonusEnum.CardBonusName.JockeyingFortitud);
        DeactivateBonus(BonusEnum.CardBonusName.JockeyingDamage);
        DeactivateBonus(BonusEnum.CardBonusName.IrishWhip);
        DeactivateBonus(BonusEnum.CardBonusName.Clothesline);
        DeactivateBonus(BonusEnum.CardBonusName.AtomicDrop);
        DeactivateBonus(BonusEnum.CardBonusName.SnapMare);
        DeactivateBonus(BonusEnum.CardBonusName.GetCrowdSupport);
        DeactivateBonus(BonusEnum.CardBonusName.OpenUpaCanOfWhoopAss);
        DeactivateBonus(BonusEnum.CardBonusName.SmackdownHotel);
    }
    
    private void DeactivateBonus(BonusEnum.CardBonusName name)
    {
        switch (name)
        {
            case BonusEnum.CardBonusName.JockeyingFortitud:
                bonusStructureInfo.IsJockeyingForPositionBonusFortitudeActive = false;
                break;
            case BonusEnum.CardBonusName.JockeyingDamage:
                bonusStructureInfo.IsJockeyingForPositionBonusDamageActive = false;
                break;
            case BonusEnum.CardBonusName.IrishWhip:
                bonusStructureInfo.IsIrishWhipBonusActive = false;
                break;
            case BonusEnum.CardBonusName.Clothesline:
                bonusStructureInfo.ClotheslineBonusActive = false;
                break;
            case BonusEnum.CardBonusName.AtomicDrop:
                bonusStructureInfo.AtomicDropBonusActive = false;
                break;
            case BonusEnum.CardBonusName.SnapMare:
                bonusStructureInfo.SnapMareBonusActive = false;
                break;
            case BonusEnum.CardBonusName.GetCrowdSupport:
                bonusStructureInfo.GetCrowdSupportBonusActive = false;
                break;
            case BonusEnum.CardBonusName.OpenUpaCanOfWhoopAss:
                bonusStructureInfo.OpenUpaCanOfWhoopAssBonusActive = false;
                break;
            case BonusEnum.CardBonusName.SmackdownHotel:
                bonusStructureInfo.SmackdownHotelBonusActive = false;
                break;
            case BonusEnum.CardBonusName.Diversion:
                bonusStructureInfo.DiversionBonusActive = false;
                break;
            case BonusEnum.CardBonusName.Stagger:
                bonusStructureInfo.StaggerBonusActive = false;
                break;
            case BonusEnum.CardBonusName.Ayatollah:
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
            DeactivateBonus(BonusEnum.CardBonusName.Diversion);
            DeactivateBonus(BonusEnum.CardBonusName.Stagger);
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
        DeactivateNormalTurnBonus();
        try
        {
            if (controllerCurrentPlayer == bonusStructureInfo.WhoActivateNextPlayedCardBonusEffect)
                DeactivateReversalBonus();
        } catch (NullReferenceException e) {}
    }
    
    private void DeactivateNormalTurnBonus()
    {
        bonusStructureInfo.IAmTheGameBonus = 0;
        bonusStructureInfo.HaymakerBonus = 0;
        bonusStructureInfo.SuperkickBonus = 0;
        bonusStructureInfo.PowerofDarknessDamageBonus = 0;
        bonusStructureInfo.PowerofDarknessFortitudBonus = 0;
        DeactivateBonus(BonusEnum.CardBonusName.Ayatollah);
    }
    
    private void DeactivateReversalBonus()
    {
        bonusStructureInfo.UndertakerSitsUpDamageBonus = 0;
        bonusStructureInfo.UndertakerSitsUpFortitudBonus = 0;
        bonusStructureInfo.KanesReturnDamageBonus = 0;
        bonusStructureInfo.KanesReturnFortitudBonus = 0;
        bonusStructureInfo.DontYouNeverEVERBonus = 0;
    }
    
}