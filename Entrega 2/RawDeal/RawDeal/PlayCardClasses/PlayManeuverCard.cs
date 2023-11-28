using RawDeal.CardClasses;
using RawDeal.EffectsClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class PlayManeuverCard
{
    private readonly GameStructureInfo gameStructureInfo;
    private bool isStunValueUsed;
    private bool theReversalCardIsUsed;

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
        var totalDamage = GetDamageProduced(playedCardController);
        playedCardController.ApplyBonusEffect();
        int extraDamage = gameStructureInfo.BonusManager.GetDamageForSuccessfulManeuver(playedCardController, 
            gameStructureInfo.LastDamageComited);

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
                     playedCardController.ExtraDamage()+ 
                     gameStructureInfo.BonusManager.EternalDamage(playedCardController, gameStructureInfo.ControllerCurrentPlayer);
        var totalDamage =
            gameStructureInfo.PlayCard.ObtainDamageByCheckingIfTheCardBelongsToMankindSuperStar(damage,
                gameStructureInfo.ControllerOpponentPlayer);
        return totalDamage;
    }

    private bool CanThePlayerReceiveDamage(int totalDamage)
    {
        return totalDamage > 0 && gameStructureInfo.IsTheGameStillPlaying;
    }

    private void SayThatTheyAreGoingToReceiveDamage(int totalDamage)
    {   
        gameStructureInfo.LastDamageComited = totalDamage;
        var opposingSuperStarName = gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar();
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
            UseStunValueOpcion();
        else if (PlayerLostDueToLackOfCardsToReceiveDamage(gameStructureInfo.ControllerOpponentPlayer))
            gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(gameStructureInfo.ControllerOpponentPlayer);
        else if (PlayerLostDueToLackOfCardsToReceiveDamage(gameStructureInfo.ControllerCurrentPlayer))
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
        
        var flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
        var flippedCardString = flippedCardController.GetStringCardInfo();
        gameStructureInfo.View.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
        DeckReversal(flippedCardController, controllerOpponentPlayer, totalDamage);
    }

    private void DeckReversal(CardController flippedCardController, PlayerController controllerOpponentPlayer, int totalDamage)
    {   

        theReversalCardIsUsed = flippedCardController.CanUseThisReversalCard(controllerOpponentPlayer, "Deck",  totalDamage);
        if (theReversalCardIsUsed)
        {
            gameStructureInfo.EffectsUtils.EndTurn();
            gameStructureInfo.View.SayThatCardWasReversedByDeck(controllerOpponentPlayer.NameOfSuperStar());
        }
    }

    private bool CheckIfShouldApplyStunValue()
    {
        return theReversalCardIsUsed && gameStructureInfo.CardBeingPlayed.TheCardHadStunValue() && !isStunValueUsed;
    }

    private void UseStunValueOpcion()
    {
        if (!PlayerLostDueToLackOfCardsToReceiveDamage(gameStructureInfo.ControllerOpponentPlayer))
        {
            isStunValueUsed = true;
            var numberOfCardsToSteal = gameStructureInfo.View.AskHowManyCardsToDrawBecauseOfStunValue(
                gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(),
                gameStructureInfo.CardBeingPlayed.GetCardStunValue());
        
            new DrawCardEffect(gameStructureInfo.ControllerOpponentPlayer, 
                gameStructureInfo).StealCards(numberOfCardsToSteal);
        }
    }

    private bool PlayerLostDueToLackOfCardsToReceiveDamage(PlayerController controllerOpponentPlayer)
    {   
        return !controllerOpponentPlayer.HasCardsInArsenal();
    }

    private void MoveCardHasBeenUsedToRingArea(CardController playedCardController)
    {
        if (!theReversalCardIsUsed)
            gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetCurrentPlayer(),
                playedCardController);
        else
            gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetOpponentPlayer(),
                playedCardController);
    }
}