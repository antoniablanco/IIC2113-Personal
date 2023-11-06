using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Reversal;

public class ElbowToTheFace : Card
{
    public ElbowToTheFace(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, string reverseBy, int totaldamage)
    {   
        const int maximumDamageProducedByPlayedCard = 7;
        return playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver") &&
               totaldamage <= maximumDamageProducedByPlayedCard;
    }

    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        /*
        var damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        var damageProduce =
            gameStructureInfo.PlayCard.ObtainDamageByCheckingIfTheCardBelongsToMankindSuperStar(int.Parse(Damage),
                damagedPlayerController);
        
        new ProduceDamageEffectUtils(damageProduce, damagedPlayerController,
            gameStructureInfo);
        */
        gameStructureInfo.EffectsUtils.EndTurn();
    }
}