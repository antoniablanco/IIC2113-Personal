using RawDeal.CardClass;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class PlayManeuverCard
{
    private GameStructureInfo gameStructureInfo;
    private bool isUserReversalDeckCard = false;
    private bool isStunValueUsed = false;
    
    public PlayManeuverCard(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }
    
    public void PlayCard(CardController playedCardController)
    {
        PrintActionPlayCard(playedCardController);
        if (!isUserReversalDeckCard)
            gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetCurrentPlayer(), playedCardController);

        else
            gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetOpponentPlayer(), playedCardController);

    }

    private void PrintActionPlayCard(CardController playedCardController)
    {
        int totalDamage = GetDamageProduced(playedCardController);
        gameStructureInfo.LastDamageComited = totalDamage;
        playedCardController.ApplyManeuverEffect(playedCardController);
        if (totalDamage > 0 && gameStructureInfo.IsTheGameStillPlaying)
        {
            SayThatTheyAreGoingToReceiveDamage(totalDamage);
            CauseDamageActionPlayCard(totalDamage, gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.GetOpponentPlayer());
        }
    }
    
    public void CauseDamageActionPlayCard(int totalDamage, PlayerController controllerOpponentPlayer, Player player)
    {
        isUserReversalDeckCard = false;
        isStunValueUsed = false;
        for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
        {   
            if (CheckCanReceiveDamage(controllerOpponentPlayer) && !isUserReversalDeckCard)
                ShowOneFaceDownCard(currentDamage + 1, totalDamage, player, controllerOpponentPlayer);
            else if (isUserReversalDeckCard && gameStructureInfo.LastPlayedCard.TheCardHadStunValue() &&
                     !isStunValueUsed)
            {   
                UseStunValueOpcion();
            }
            else if (!CheckCanReceiveDamage(controllerOpponentPlayer))
            {
                gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(controllerOpponentPlayer);
                break;
            }
        }
    }

    private void UseStunValueOpcion()
    {   
        isStunValueUsed = true;
        int numberOfCardsToSteal = gameStructureInfo.View.AskHowManyCardsToDrawBecauseOfStunValue(gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), gameStructureInfo.LastPlayedCard.GetCardStunValue());
        gameStructureInfo.Effects.StealCards(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.GetOpponentPlayer(), numberOfCardsToSteal);
    }
    
    private bool CheckCanReceiveDamage(PlayerController controllerOpponentPlayer)
    {
        return controllerOpponentPlayer.HasCardsInArsenal();
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
        isUserReversalDeckCard = flippedCardController.CanUseThisReversalCard(controllerOpponentPlayer);
        if (isUserReversalDeckCard)
        {   
            Console.WriteLine(gameStructureInfo.IsTheGameStillPlaying);
            gameStructureInfo.Effects.EndTurn();
            gameStructureInfo.View.SayThatCardWasReversedByDeck(controllerOpponentPlayer.NameOfSuperStar());
        }
    }
    
    private int GetDamageProduced(CardController playedCardController)
    {   
        int damage = playedCardController.GetDamageProducedByTheCard() + gameStructureInfo.GetSetGameVariables.AddBonusDamage();
        int totalDamage = gameStructureInfo.PlayCard.GetDamageProducedCheckingMankindSuperStarAbility(damage, gameStructureInfo.ControllerOpponentPlayer);
        return totalDamage;
    }

    private void SayThatTheyAreGoingToReceiveDamage(int totalDamage)
    {
        string opposingSuperStarName = gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar();
        gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(opposingSuperStarName, totalDamage);
    }

}