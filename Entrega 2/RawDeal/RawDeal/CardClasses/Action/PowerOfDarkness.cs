using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class PowerOfDarkness: Card
{
    public PowerOfDarkness(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        ApplyEffect(gameStructureInfo, gameStructureInfo.ControllerCurrentPlayer);
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetCurrentPlayer(),
            playedCardController);
    }

    private void ApplyEffect(GameStructureInfo gameStructureInfo, PlayerController playerController)
    {
        gameStructureInfo.BonusManager.ApplyTurnBonusEffect(BonusEnum.BonusType.PowerofDarknessDamage, bonusValue:5);
        gameStructureInfo.BonusManager.ApplyTurnBonusEffect(BonusEnum.BonusType.PowerofDarknessFortitud, bonusValue:20);
    }
}