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

    public void PlayCard(CardController playedCardController, int lastDamageComited)
    {
        playedCardController.ApplyManeuverEffect(playedCardController);
        StartDamageProduceByTheCard(playedCardController, lastDamageComited);
        MoveCardHasBeenUsedToRingArea(playedCardController);
    }

    private void StartDamageProduceByTheCard(CardController playedCardController, int lastDamageComited)
    {   
        var totalDamage = GetDamageProduced(playedCardController);
        playedCardController.ApplyBonusEffect();
        int extraDamage = gameStructureInfo.BonusManager.GetDamageForSuccessfulManeuver(playedCardController, lastDamageComited);

        if (CanThePlayerReceiveDamage(totalDamage))
        {   
            int ultimoDaño = gameStructureInfo.LastDamageComited;
            SayThatTheyAreGoingToReceiveDamage(totalDamage + extraDamage);
            CauseDamageActionPlayCard(totalDamage+ extraDamage, gameStructureInfo.ControllerOpponentPlayer,
                gameStructureInfo.GetOpponentPlayer(), ultimoDaño);
        }
    }

    private int GetDamageProduced(CardController playedCardController)
    {   
        var damage = playedCardController.GetDamageProducedByTheCard() +
                     gameStructureInfo.BonusManager.GetNexPlayCardDamageBonus() +
                     gameStructureInfo.BonusManager.GetTurnDamageBonus(playedCardController);
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

    private void CauseDamageActionPlayCard(int totalDamage, PlayerController controllerOpponentPlayer, 
        Player player, int lastDamageComited)
    {
        DeclaresWithoutUseVariablesForReversalDeck();
        for (var currentDamage = 0; currentDamage < totalDamage; currentDamage++)
            HandleDifferentOptionsForDamage(currentDamage, totalDamage, 
                controllerOpponentPlayer, player, lastDamageComited);
    }

    private void DeclaresWithoutUseVariablesForReversalDeck()
    {
        theReversalCardIsUsed = false;
        isStunValueUsed = false;
    }

    private void HandleDifferentOptionsForDamage(int currentDamage, int totalDamage,
        PlayerController controllerOpponentPlayer, Player player, int lastDamageComited)
    {
        if (CheckShouldReceiveDamage(controllerOpponentPlayer))
            ShowOneFaceDownCard(currentDamage + 1, totalDamage, player, controllerOpponentPlayer, lastDamageComited);
        else if (CheckIfShouldApplyStunValue())
            UseStunValueOpcion();
        else if (PlayerLostDueToLackOfCardsToReceiveDamage(controllerOpponentPlayer))
            gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(controllerOpponentPlayer);
    }

    private bool CheckShouldReceiveDamage(PlayerController controllerOpponentPlayer)
    {
        return controllerOpponentPlayer.HasCardsInArsenal() && !theReversalCardIsUsed;
    }

    private void ShowOneFaceDownCard(int currentDamage, int totalDamage, Player player,
        PlayerController controllerOpponentPlayer, int lastDamageComited)
    {
        var flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
        var flippedCardString = flippedCardController.GetStringCardInfo();
        gameStructureInfo.View.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
        DeckReversal(flippedCardController, controllerOpponentPlayer, lastDamageComited);
    }

    private void DeckReversal(CardController flippedCardController, PlayerController controllerOpponentPlayer, int lastDamageComited)
    {   
        int extraDamage = gameStructureInfo.BonusManager.GetDamageForSuccessfulManeuver(gameStructureInfo.CardBeingPlayed, lastDamageComited);
        theReversalCardIsUsed = flippedCardController.CanUseThisReversalCard(controllerOpponentPlayer, "Deck", extraDamage);
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
        isStunValueUsed = true;
        var numberOfCardsToSteal = gameStructureInfo.View.AskHowManyCardsToDrawBecauseOfStunValue(
            gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(),
            gameStructureInfo.CardBeingPlayed.GetCardStunValue());
        
        new DrawCardEffect(gameStructureInfo.ControllerOpponentPlayer,gameStructureInfo.GetOpponentPlayer(), 
            gameStructureInfo).StealCards(numberOfCardsToSteal);
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