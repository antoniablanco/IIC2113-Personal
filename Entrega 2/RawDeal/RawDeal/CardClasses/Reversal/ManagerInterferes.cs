using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.CardClass.Reversal;

public class ManagerInterferes: Card
{
    public ManagerInterferes(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController)
    {
        return playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver");
    }
    
    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        gameStructureInfo.Effects.StealCards(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.GetOpponentPlayer());
        
        PlayerController damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        int damageProduce = gameStructureInfo.PlayCard.ObtainDamageByCheckingIfTheCardBelongsToMankindSuperStar(int.Parse(Damage), damagedPlayerController);
        
        gameStructureInfo.Effects.ProduceSeveralDamage(damageProduce, damagedPlayerController,gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.Effects.EndTurn();
    }
}