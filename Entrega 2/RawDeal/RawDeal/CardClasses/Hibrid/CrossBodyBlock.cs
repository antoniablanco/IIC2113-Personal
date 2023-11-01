using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class CrossBodyBlock: Card
{
    public CrossBodyBlock(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totaldamage)
    {   
        return gameStructureInfo.LastCardBeingPlayedTitle == "Irish Whip" && playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver");
    }
    
    public override bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo)
    {   
        if (gameStructureInfo.CardBeingPlayed == null)
            return false;
        return gameStructureInfo.CardBeingPlayed.GetCardTitle() == "Irish Whip";
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