namespace RawDeal;

public class GetSetGameVariables
{   
    public GameStructureInfo gameStructureInfo = new GameStructureInfo();
    
    public void SetVariableTrueBecauseTurnStarted()
    {   
        gameStructureInfo.IsTheTurnBeingPlayed = true;
        gameStructureInfo.ControllerCurrentPlayer.TheTurnHasJustStartTheSuperStarHasNotUsedHisSuperAbility();
    }
    
    public void CreatePlayerInitialOrder()
    {
        gameStructureInfo.ControllerCurrentPlayer = (gameStructureInfo.ControllerPlayerOne.GetSuperStarValue() < gameStructureInfo.ControllerPlayerTwo.GetSuperStarValue()) ? gameStructureInfo.ControllerPlayerTwo : gameStructureInfo.ControllerPlayerOne;
        gameStructureInfo.ControllerOpponentPlayer = (gameStructureInfo.ControllerPlayerOne.GetSuperStarValue() < gameStructureInfo.ControllerPlayerTwo.GetSuperStarValue()) ? gameStructureInfo.ControllerPlayerOne : gameStructureInfo.ControllerPlayerTwo;
    }
    
    public bool ShouldWeContinueTheGame()
    {   
        return (gameStructureInfo.ControllerPlayerOne.AreThereCardsLeftInTheArsenal() && gameStructureInfo.ControllerPlayerTwo.AreThereCardsLeftInTheArsenal() && gameStructureInfo.IsTheGameStillPlaying);
    }
    
    public bool TheTurnIsBeingPlayed()
    {
        return gameStructureInfo.IsTheTurnBeingPlayed;
    }
    
    public void SetVariablesAfterWinning()
    {
        gameStructureInfo.winnerPlayer = gameStructureInfo.ControllerCurrentPlayer;
        gameStructureInfo.IsTheGameStillPlaying = false;
        DeclareEndOfTurn();
    }

    private void DeclareEndOfTurn()
    {
        gameStructureInfo.IsTheTurnBeingPlayed = false;
    }
    
    public void UpdateVariablesAtEndOfTurn()
    {   
        DeclareEndOfTurn();
        if (!gameStructureInfo.GameLogic.CheckIfPlayersHasCardsInArsenalToContinuePlaying())
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
    
    private void SetVariablesAfterLosing() 
    {   
        gameStructureInfo.winnerPlayer = (gameStructureInfo.ControllerCurrentPlayer.HasCardsInArsenal()) ? gameStructureInfo.ControllerCurrentPlayer : gameStructureInfo.ControllerOpponentPlayer;
        gameStructureInfo.IsTheGameStillPlaying = false;
        DeclareEndOfTurn();
    }

    public void SetVariablesAfterGaveUp()
    {
        gameStructureInfo.winnerPlayer = (gameStructureInfo.ControllerCurrentPlayer == gameStructureInfo.ControllerPlayerOne) ? gameStructureInfo.ControllerPlayerTwo : gameStructureInfo.ControllerPlayerOne;
        gameStructureInfo.IsTheGameStillPlaying = false;
        DeclareEndOfTurn();
    }

}