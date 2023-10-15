using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.CardClass.Reversal;

public class ChynaInterferes: Card
{
    public ChynaInterferes(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController)
    {
        return playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver");
    }
    
    public override void ReversalEffect(GameStructureInfo gameStructureInfo)
    {    
        const int numberOfCardsToSteal = 2;
        gameStructureInfo.Effects.StealCards(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.GetOpponentPlayer(), numberOfCardsToSteal);
        PlayerController damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        int damageProduce = int.Parse(Damage);
        if (gameStructureInfo.Effects.IsTheCardWeAreReversalOfMankindSuperStart(gameStructureInfo.ControllerCurrentPlayer))
            damageProduce -= 1;
        gameStructureInfo.Effects.ProduceDamage(damageProduce, damagedPlayerController,gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.Effects.EndTurn();
    }
}