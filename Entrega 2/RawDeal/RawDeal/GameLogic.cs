using RawDeal.CardClass;
using RawDeal.PlayerClass;
using RawDeal.SuperStarClass;
using RawDealView;
using RawDealView.Options;

namespace RawDeal;
using System.Text.Json ;


public class GameLogic
{   
    private bool _isTheGameStillPlaying = true;
    private bool _IsTheTurnBeingPlayed = true;
    
    public GameStructureInfo gameStructureInfo = new GameStructureInfo();
    
    public void SettingTurnStartInformation()
    {
        gameStructureInfo.ControllerCurrentPlayer.DrawCard();
        SetVariableTrueBecauseTurnStarted();
        gameStructureInfo.view.SayThatATurnBegins(gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar());
        TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed();
    }

    private void SetVariableTrueBecauseTurnStarted()
    {   
        _IsTheTurnBeingPlayed = true;
        gameStructureInfo.ControllerCurrentPlayer.TheTurnHasJustStartTheSuperStarHasNotUsedHisSuperAbility();
    }

    private void TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed()
    {
        gameStructureInfo.ControllerCurrentPlayer.UsingAutomaticSuperAbility(gameStructureInfo);
    }
    
    public void ThePlayerDrawTheirInitialsHands()
    {
        gameStructureInfo.ControllerPlayerOne.DrawInitialHandCards();
        gameStructureInfo.ControllerPlayerTwo.DrawInitialHandCards();
    }
    
    public string GetWinnerSuperstarName() 
    {   
        return gameStructureInfo.winnerPlayer.NameOfSuperStar();
    }
    
    public void DisplayPlayerInformation() 
    {
        PlayerInfo playerUno = new PlayerInfo(gameStructureInfo.ControllerPlayerOne.NameOfSuperStar(), gameStructureInfo.ControllerPlayerOne.FortitudRating(), gameStructureInfo.ControllerPlayerOne.NumberOfCardsInTheHand(), gameStructureInfo.ControllerPlayerOne.NumberOfCardsInTheArsenal());
        PlayerInfo playerDos = new PlayerInfo(gameStructureInfo.ControllerPlayerTwo.NameOfSuperStar(), gameStructureInfo.ControllerPlayerTwo.FortitudRating(), gameStructureInfo.ControllerPlayerTwo.NumberOfCardsInTheHand(), gameStructureInfo.ControllerPlayerTwo.NumberOfCardsInTheArsenal());
        
        List<PlayerInfo> playersListToPrint =  new List<PlayerInfo> { playerUno, playerDos };
        
        int numCurrentPlayer = gameStructureInfo.ControllerCurrentPlayer == gameStructureInfo.ControllerPlayerOne ? 0 : 1;
        int numOppositePlayer = gameStructureInfo.ControllerOpponentPlayer == gameStructureInfo.ControllerPlayerOne ? 0 : 1;
        
        gameStructureInfo.view.ShowGameInfo(playersListToPrint[numCurrentPlayer], playersListToPrint[numOppositePlayer]);
    }

    public void CreatePlayerInitialOrder()
    {
        gameStructureInfo.ControllerCurrentPlayer = (gameStructureInfo.ControllerPlayerOne.GetSuperStarValue() < gameStructureInfo.ControllerPlayerTwo.GetSuperStarValue()) ? gameStructureInfo.ControllerPlayerTwo : gameStructureInfo.ControllerPlayerOne;
        gameStructureInfo.ControllerOpponentPlayer = (gameStructureInfo.ControllerPlayerOne.GetSuperStarValue() < gameStructureInfo.ControllerPlayerTwo.GetSuperStarValue()) ? gameStructureInfo.ControllerPlayerOne : gameStructureInfo.ControllerPlayerTwo;
    }

    public bool ShouldWeContinueTheGame()
    {   
        return (gameStructureInfo.ControllerPlayerOne.AreThereCardsLeftInTheArsenal() && gameStructureInfo.ControllerPlayerTwo.AreThereCardsLeftInTheArsenal() && _isTheGameStillPlaying);
    }

    public bool TheTurnIsBeingPlayed()
    {
        return _IsTheTurnBeingPlayed;
    }
    
    public bool PlayerCanUseSuperStarAbility() 
    {
        return gameStructureInfo.ControllerCurrentPlayer.TheirSuperStarCanUseSuperAbility(gameStructureInfo.ControllerCurrentPlayer);
    }

    public void ActionUseSuperAbility()
    {
        gameStructureInfo.ControllerCurrentPlayer.UsingElectiveSuperAbility(gameStructureInfo);
    }
    
    public void SetVariablesAfterWinning()
    {
        gameStructureInfo.winnerPlayer = gameStructureInfo.ControllerCurrentPlayer;
        _isTheGameStillPlaying = false;
        DeclareEndOfTurn();
    }

    private void DeclareEndOfTurn()
    {
        _IsTheTurnBeingPlayed = false;
    }

    public void AddCardPlayedToRingArea(CardController playedCardController) 
    {   
        Player player = gameStructureInfo.GetCurrentPlayer();
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(player, playedCardController);
    }
    
    public void SelectCardsToView()
    {
        var setCardsToView = gameStructureInfo.view.AskUserWhatSetOfCardsHeWantsToSee();
        switch (setCardsToView)
        {
            case CardSet.Hand:
                ActionSeeTotalCards(gameStructureInfo.ControllerCurrentPlayer.StringCardsHand());
                break;
            case CardSet.RingArea:
                ActionSeeTotalCards(gameStructureInfo.ControllerCurrentPlayer.StringCardsRingArea());
                break;
            case CardSet.RingsidePile:
                ActionSeeTotalCards(gameStructureInfo.ControllerCurrentPlayer.StringCardsRingSide());
                break;
            case CardSet.OpponentsRingArea:
                ActionSeeTotalCards(gameStructureInfo.ControllerOpponentPlayer.StringCardsRingArea());
                break;
            case CardSet.OpponentsRingsidePile:
                ActionSeeTotalCards(gameStructureInfo.ControllerOpponentPlayer.StringCardsRingSide());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ActionSeeTotalCards(List<String> stringCardSet)
    {   
        gameStructureInfo.view.ShowCards(stringCardSet);
    }

    public void UpdateVariablesAtEndOfTurn()
    {   
        DeclareEndOfTurn();
        if (!CheckIfPlayersHasCardsInArsenalToContinuePlaying())
        {
            SetVariablesAfterLosing();
        }
        UpdateNumberOfPlayers();
    }
    
    private void UpdateNumberOfPlayers()
    {
        gameStructureInfo.ControllerCurrentPlayer = (gameStructureInfo.ControllerCurrentPlayer == gameStructureInfo.ControllerPlayerOne) ? gameStructureInfo.ControllerPlayerTwo : gameStructureInfo.ControllerPlayerOne;
        gameStructureInfo.ControllerOpponentPlayer = (gameStructureInfo.ControllerOpponentPlayer == gameStructureInfo.ControllerPlayerOne) ? gameStructureInfo.ControllerPlayerTwo : gameStructureInfo.ControllerPlayerOne;
    }
    
    private bool CheckIfPlayersHasCardsInArsenalToContinuePlaying()
    {   
        return gameStructureInfo.ControllerCurrentPlayer.HasCardsInArsenal() && gameStructureInfo.ControllerOpponentPlayer.HasCardsInArsenal();
    }

    private void SetVariablesAfterLosing() 
    {   
        gameStructureInfo.winnerPlayer = (gameStructureInfo.ControllerCurrentPlayer.HasCardsInArsenal()) ? gameStructureInfo.ControllerPlayerTwo : gameStructureInfo.ControllerPlayerOne;
        _isTheGameStillPlaying = false;
        DeclareEndOfTurn();
    }

    public void SetVariablesAfterGaveUp()
    {
        gameStructureInfo.winnerPlayer = (gameStructureInfo.ControllerCurrentPlayer == gameStructureInfo.ControllerPlayerOne) ? gameStructureInfo.ControllerPlayerTwo : gameStructureInfo.ControllerPlayerOne;
        _isTheGameStillPlaying = false;
        DeclareEndOfTurn();
    }
    
}
