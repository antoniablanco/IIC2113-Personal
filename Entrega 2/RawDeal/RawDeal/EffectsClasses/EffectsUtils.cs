using RawDeal.CardClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class EffectsUtils
{
    protected readonly GameStructureInfo gameStructureInfo = new();
    
    public EffectsUtils(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }
    
    public void DiscardCardFromHandNotifying(CardController playedCardController,
        PlayerController controllerCurrentPlayer, Player player)
    {
        gameStructureInfo.View.SayThatPlayerMustDiscardThisCard(controllerCurrentPlayer.NameOfSuperStar(),
            playedCardController.GetCardTitle());
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(player, playedCardController);
    }
    
    public void DiscardActionCardToRingAreButNotSaying(CardController playedCardController, Player player)
    {
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(player, playedCardController);
    }
    
    public bool IsTheSuperStarMankind(PlayerController playerController)
    {
        return playerController.NameOfSuperStar() == "MANKIND";
    }

    public int GetDamageProducedByReversalCardWithNotEspecificDamage()
    {
        var totalDamage = gameStructureInfo.CardBeingPlayed.GetDamageProducedByTheCard() +
                          gameStructureInfo.BonusManager.GetNexPlayCardDamageBonus() + 
                          gameStructureInfo.BonusManager.GetTurnDamageBonus(gameStructureInfo.CardBeingPlayed) +
                          gameStructureInfo.BonusManager.GetDamageForSuccessfulManeuver(gameStructureInfo.CardBeingPlayed);
        if (IsTheSuperStarMankind(gameStructureInfo.ControllerOpponentPlayer) ||
            IsTheSuperStarMankind(gameStructureInfo.ControllerCurrentPlayer))
            totalDamage -= 1;
        return totalDamage;
    }
    
    protected bool IsPositive(int number)
    {
        return number > 0;
    }
    
    protected bool CheckIfThePlayerHasCardInArsenal(PlayerController controllerPlayer)
    {
        return controllerPlayer.HasCardsInArsenal();
    }
    
    public void EndTurn()
    {
        gameStructureInfo.EndTurnManager.UpdateVariablesAtEndOfTurn();
    }
    
}