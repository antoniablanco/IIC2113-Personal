using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.CardClass.Reversal;

public class ElbowToTheFace: Card
{
    public ElbowToTheFace(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController)
    {
        const int maximumDamageProducedByPlayedCard = 7;
        return playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver") && playedCardController.DealsTheMaximumDamage(maximumDamageProducedByPlayedCard);
    }
    
    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        PlayerController damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        int damageProduce = gameStructureInfo.PlayCard.ObtainDamageByCheckingIfTheCardBelongsToMankindSuperStar(int.Parse(Damage), damagedPlayerController);
        
        gameStructureInfo.Effects.ProduceSeveralDamage(damageProduce, damagedPlayerController,gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.Effects.EndTurn();
    }
}