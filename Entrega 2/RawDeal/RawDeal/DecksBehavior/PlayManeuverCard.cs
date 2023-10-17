using RawDeal.CardClass;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class PlayManeuverCard
{
    private GameStructureInfo gameStructureInfo;
    private bool theReversalCardIsUsed = false;
    private bool isStunValueUsed = false;
    
    public PlayManeuverCard(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }
    
    public void PlayCard(CardController playedCardController)
    {   
        playedCardController.ApplyManeuverEffect(playedCardController);
        StartDamageProduceByTheCard(playedCardController);
        MoveCardHasBeenUsedToRingArea(playedCardController);
    }
    
    private void StartDamageProduceByTheCard(CardController playedCardController)
    {
        int totalDamage = GetDamageProduced(playedCardController);
        
        if (CanThePlayerReceiveDamage(totalDamage))
        {
            SayThatTheyAreGoingToReceiveDamage(totalDamage);
            CauseDamageActionPlayCard(totalDamage, gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.GetOpponentPlayer());
        }
    }
    
    private int GetDamageProduced(CardController playedCardController)
    {   
        int damage = playedCardController.GetDamageProducedByTheCard() + gameStructureInfo.BonusManager.AddBonus("JockeyingDamage");
        int totalDamage = gameStructureInfo.PlayCard.GetDamageProducedCheckingMankindSuperStarAbility(damage, gameStructureInfo.ControllerOpponentPlayer);
        gameStructureInfo.LastDamageComited = totalDamage;
        return totalDamage;
    }
    
    private bool CanThePlayerReceiveDamage(int totalDamage)
    {
        return totalDamage > 0 && gameStructureInfo.IsTheGameStillPlaying;
    }
    
    private void SayThatTheyAreGoingToReceiveDamage(int totalDamage)
    {
        string opposingSuperStarName = gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar();
        gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(opposingSuperStarName, totalDamage);
    }
    
    private void CauseDamageActionPlayCard(int totalDamage, PlayerController controllerOpponentPlayer, Player player)
    {
        DeclaresWithoutUseVariablesForReversalDeck();
        
        for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
        {
            HandleDifferentOptionsForDamage(currentDamage, totalDamage, controllerOpponentPlayer, player);
        }
    }

    private void DeclaresWithoutUseVariablesForReversalDeck()
    {
        theReversalCardIsUsed = false;
        isStunValueUsed = false;
    }

    private void HandleDifferentOptionsForDamage(int currentDamage, int totalDamage, PlayerController controllerOpponentPlayer, Player player)
    {
        if (CheckShouldReceiveDamage(controllerOpponentPlayer))
            ShowOneFaceDownCard(currentDamage + 1, totalDamage, player, controllerOpponentPlayer);
        else if (CheckIfShouldApplyStunValue())
            UseStunValueOpcion();
        else if (PlayerLostDueToLackOfCardsToReceiveDamage(controllerOpponentPlayer))
            gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(controllerOpponentPlayer);
    }
    
    private bool CheckShouldReceiveDamage(PlayerController controllerOpponentPlayer)
    {
        return (controllerOpponentPlayer.HasCardsInArsenal() && !theReversalCardIsUsed);
    }
    
    private void ShowOneFaceDownCard(int currentDamage, int totalDamage, Player player, PlayerController controllerOpponentPlayer)
    {
        CardController flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
        string flippedCardString = flippedCardController.GetStringCardInfo();
        gameStructureInfo.View.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
        DeckReversal(flippedCardController, controllerOpponentPlayer);
    }
    
    private void DeckReversal(CardController flippedCardController, PlayerController controllerOpponentPlayer)
    {
        theReversalCardIsUsed = flippedCardController.CanUseThisReversalCard(controllerOpponentPlayer);
        if (theReversalCardIsUsed)
        {
            Console.WriteLine(gameStructureInfo.IsTheGameStillPlaying);
            gameStructureInfo.Effects.EndTurn();
            gameStructureInfo.View.SayThatCardWasReversedByDeck(controllerOpponentPlayer.NameOfSuperStar());
        }
    }
    
    private bool CheckIfShouldApplyStunValue()
    {
        return (theReversalCardIsUsed && gameStructureInfo.LastPlayedCard.TheCardHadStunValue() && !isStunValueUsed);
    }
    
    private void UseStunValueOpcion()
    {   
        isStunValueUsed = true;
        int numberOfCardsToSteal = gameStructureInfo.View.AskHowManyCardsToDrawBecauseOfStunValue(gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), gameStructureInfo.LastPlayedCard.GetCardStunValue());
        gameStructureInfo.Effects.StealCards(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.GetOpponentPlayer(), numberOfCardsToSteal);
    }
    
    private bool PlayerLostDueToLackOfCardsToReceiveDamage(PlayerController controllerOpponentPlayer)
    {
        return (!controllerOpponentPlayer.HasCardsInArsenal());
    }
    
    private void MoveCardHasBeenUsedToRingArea(CardController playedCardController)
    {
        if (!theReversalCardIsUsed)
            gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetCurrentPlayer(), playedCardController);
        else
            gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetOpponentPlayer(), playedCardController);
    }

}