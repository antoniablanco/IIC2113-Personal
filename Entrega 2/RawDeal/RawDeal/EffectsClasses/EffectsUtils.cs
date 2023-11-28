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
        PlayerController controllerCurrentPlayer)
    {   
        Player player = gameStructureInfo.ControllerOpponentPlayer == controllerCurrentPlayer ? 
            gameStructureInfo.GetOpponentPlayer() : gameStructureInfo.GetCurrentPlayer();
        
        gameStructureInfo.View.SayThatPlayerMustDiscardThisCard(controllerCurrentPlayer.GetNameOfSuperStar(),
            playedCardController.GetCardTitle());
        gameStructureInfo.CardMovement.TransferSelectedCardFromHandToRingSide(player, playedCardController);
    }
    
    public void DiscardActionCardToRingAreButNotSaying(CardController playedCardController, Player player)
    {
        gameStructureInfo.CardMovement.TransferSelectedCardFromHandToRingArea(player, playedCardController);
    }
    
    public bool IsTheSuperStarMankind(PlayerController playerController)
    {
        return playerController.GetNameOfSuperStar() == "MANKIND";
    }

    public int GetDamageProducedByReversalCardWithNotSpecificDamage()
    {
        var totalDamage = gameStructureInfo.CardBeingPlayed.GetDamageProducedByTheCard() +
                          gameStructureInfo.BonusManager.GetNexPlayCardDamageBonus() + 
                          gameStructureInfo.BonusManager.GetTurnDamageBonus(gameStructureInfo.CardBeingPlayed) +
                          gameStructureInfo.BonusManager.GetDamageForSuccessfulManeuver(gameStructureInfo.CardBeingPlayed, 
                              gameStructureInfo.LastDamageCommitted);
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