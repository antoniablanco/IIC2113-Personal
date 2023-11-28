using RawDeal.CardClasses;
using RawDeal.EffectsClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class ManeuverCardPlay
{
    private readonly GameStructureInfo gameStructureInfo;
    private bool isStunValueUsed;
    private bool theReversalCardIsUsed;

    public ManeuverCardPlay(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }

    public void PlayCard(CardController playedCardController)
    {
        playedCardController.ApplyManeuverEffect(playedCardController);
        StartDamageProduceByTheCard(playedCardController);
        MoveTheCardUsedToRingArea(playedCardController);
    }

    private void StartDamageProduceByTheCard(CardController playedCardController)
    {   
        var totalDamage = GetDamageProduced(playedCardController);
        playedCardController.ApplyBonusEffect();
        int extraDamage = gameStructureInfo.BonusManager.GetDamageForSuccessfulManeuver(playedCardController, 
            gameStructureInfo.LastDamageCommitted);

        if (CanThePlayerReceiveDamage(totalDamage))
        {   
            SayThatTheyAreGoingToReceiveDamage(totalDamage + extraDamage);
            CauseDamageActionPlayCard(totalDamage+ extraDamage, gameStructureInfo.ControllerOpponentPlayer);
        }
    }

    private int GetDamageProduced(CardController playedCardController)
    {   
        var damage = playedCardController.GetDamageProducedByTheCard() +
                     gameStructureInfo.BonusManager.GetNexPlayCardDamageBonus() +
                     gameStructureInfo.BonusManager.GetTurnDamageBonus(playedCardController) + 
                     playedCardController.GetExtraDamage()+ 
                     gameStructureInfo.BonusManager.GetEternalDamage(playedCardController, gameStructureInfo.ControllerCurrentPlayer);
        var totalDamage =
            gameStructureInfo.CardPlay.ObtainDamageByCheckingIfTheCardBelongsToMankindSuperStar(damage,
                gameStructureInfo.ControllerOpponentPlayer);
        return totalDamage;
    }

    private bool CanThePlayerReceiveDamage(int totalDamage)
    {
        return totalDamage > 0 && gameStructureInfo.IsTheGameStillPlaying;
    }

    private void SayThatTheyAreGoingToReceiveDamage(int totalDamage)
    {   
        gameStructureInfo.LastDamageCommitted = totalDamage;
        var opposingSuperStarName = gameStructureInfo.ControllerOpponentPlayer.GetNameOfSuperStar();
        gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(opposingSuperStarName, totalDamage);
    }

    private void CauseDamageActionPlayCard(int totalDamage, PlayerController controllerOpponentPlayer)
    {
        DeclaresWithoutUseVariablesForReversalDeck();
        for (var currentDamage = 0; currentDamage < totalDamage; currentDamage++)
            HandleDifferentOptionsForDamage(currentDamage, totalDamage, 
                controllerOpponentPlayer);
    }

    private void DeclaresWithoutUseVariablesForReversalDeck()
    {
        theReversalCardIsUsed = false;
        isStunValueUsed = false;
    }

    private void HandleDifferentOptionsForDamage(int currentDamage, int totalDamage,
        PlayerController controllerOpponentPlayer)
    {   
        if (CheckShouldReceiveDamage(controllerOpponentPlayer))
            ShowOneFaceDownCard(currentDamage + 1, totalDamage, controllerOpponentPlayer);
        else if (CheckIfShouldApplyStunValue())
            UseStunValueOption();
        else if (CheckIfPlayerLostDueToLackOfCardsToReceiveDamage(gameStructureInfo.ControllerOpponentPlayer))
            gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(gameStructureInfo.ControllerOpponentPlayer);
        else if (CheckIfPlayerLostDueToLackOfCardsToReceiveDamage(gameStructureInfo.ControllerCurrentPlayer))
            gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(gameStructureInfo.ControllerCurrentPlayer);
    }

    private bool CheckShouldReceiveDamage(PlayerController controllerOpponentPlayer)
    {
        return controllerOpponentPlayer.HasCardsInArsenal() && !theReversalCardIsUsed;
    }

    private void ShowOneFaceDownCard(int currentDamage, int totalDamage,
        PlayerController controllerOpponentPlayer)
    {   
        Player player = gameStructureInfo.ControllerOpponentPlayer == controllerOpponentPlayer ? 
            gameStructureInfo.GetOpponentPlayer() : gameStructureInfo.GetCurrentPlayer();
        
        var flippedCardController = gameStructureInfo.CardMovement.TransferUnselectedCardFromArsenalToRingSide(player);
        var flippedCardString = flippedCardController.GetStringCardInfo();
        gameStructureInfo.View.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
        ApplyDeckReversalCard(flippedCardController, controllerOpponentPlayer, totalDamage);
    }

    private void ApplyDeckReversalCard(CardController flippedCardController, PlayerController controllerOpponentPlayer, int totalDamage)
    {   

        theReversalCardIsUsed = flippedCardController.CanUseThisReversalCard(controllerOpponentPlayer, "Deck",  totalDamage);
        if (theReversalCardIsUsed)
        {
            gameStructureInfo.EffectsUtils.EndTurn();
            gameStructureInfo.View.SayThatCardWasReversedByDeck(controllerOpponentPlayer.GetNameOfSuperStar());
        }
    }

    private bool CheckIfShouldApplyStunValue()
    {
        return theReversalCardIsUsed && gameStructureInfo.CardBeingPlayed.DoesTheCardHasStunValue() && !isStunValueUsed;
    }

    private void UseStunValueOption()
    {
        if (!CheckIfPlayerLostDueToLackOfCardsToReceiveDamage(gameStructureInfo.ControllerOpponentPlayer))
        {
            isStunValueUsed = true;
            var numberOfCardsToSteal = gameStructureInfo.View.AskHowManyCardsToDrawBecauseOfStunValue(
                gameStructureInfo.ControllerOpponentPlayer.GetNameOfSuperStar(),
                gameStructureInfo.CardBeingPlayed.GetCardStunValue());
        
            new CardDrawEffect(gameStructureInfo.ControllerOpponentPlayer, 
                gameStructureInfo).StealCards(numberOfCardsToSteal);
        }
    }

    private bool CheckIfPlayerLostDueToLackOfCardsToReceiveDamage(PlayerController controllerOpponentPlayer)
    {   
        return !controllerOpponentPlayer.HasCardsInArsenal();
    }

    private void MoveTheCardUsedToRingArea(CardController playedCardController)
    {
        if (!theReversalCardIsUsed)
            gameStructureInfo.CardMovement.TransferSelectedCardFromHandToRingArea(gameStructureInfo.GetCurrentPlayer(),
                playedCardController);
        else
            gameStructureInfo.CardMovement.TransferSelectedCardFromHandToRingArea(gameStructureInfo.GetOpponentPlayer(),
                playedCardController);
    }
}