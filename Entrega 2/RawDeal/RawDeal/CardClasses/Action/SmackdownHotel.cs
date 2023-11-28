using RawDeal.EffectsClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class SmackdownHotel: Card
{
    public SmackdownHotel(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
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
        new CardDrawEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo).StealCards();
        
        new SeeOponentHandEffect(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.ControllerOpponentPlayer,
        gameStructureInfo);
        
        gameStructureInfo.BonusManager.ApplyNextPlayedCardBonusEffect(BonusEnum.CardBonusName.SmackdownHotel, bonusValue:6, 
            BonusEnum.CardBonusType.Damage);
        
        gameStructureInfo.BonusManager.SetWhoActivateNextPlayedCardBonusEffect(playerController);
        
        var turnsBeforeEffectExpires = 2;
        gameStructureInfo.BonusManager.SetTurnsLeftForBonusCounter(turnsBeforeEffectExpires);
    }
}