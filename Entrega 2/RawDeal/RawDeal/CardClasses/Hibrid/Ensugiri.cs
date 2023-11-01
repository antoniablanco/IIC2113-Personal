using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class Ensugiri: Card
{
    public Ensugiri(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totaldamage)
    {
        return playedCardController.GetCardTitle() == "Kick" && 
               playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver");
    }
    
    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        var damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        var damageProduce =
            gameStructureInfo.PlayCard.ObtainDamageByCheckingIfTheCardBelongsToMankindSuperStar(int.Parse(Damage),
                damagedPlayerController);
        
        new ProduceDamageEffectUtils(damageProduce, damagedPlayerController, gameStructureInfo.GetCurrentPlayer(),
            gameStructureInfo);
        gameStructureInfo.EffectsUtils.EndTurn();
    }
}