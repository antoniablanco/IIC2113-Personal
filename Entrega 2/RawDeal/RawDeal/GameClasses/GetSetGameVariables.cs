using RawDeal.PlayerClasses;

namespace RawDeal.GameClasses;

public class GetSetGameVariables
{   
    private GameStructureInfo gameStructureInfo = new GameStructureInfo();

    public GetSetGameVariables(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }
    
    public void CreatePlayerInitialOrder()
    {
        gameStructureInfo.ControllerCurrentPlayer = (gameStructureInfo.ControllerPlayerOne.GetSuperStarValue() < gameStructureInfo.ControllerPlayerTwo.GetSuperStarValue()) ? gameStructureInfo.ControllerPlayerTwo : gameStructureInfo.ControllerPlayerOne;
        gameStructureInfo.ControllerOpponentPlayer = (gameStructureInfo.ControllerPlayerOne.GetSuperStarValue() < gameStructureInfo.ControllerPlayerTwo.GetSuperStarValue()) ? gameStructureInfo.ControllerPlayerOne : gameStructureInfo.ControllerPlayerTwo;
    }
    
    public bool ShouldWeContinueTheGame()
    {   
        if ((!gameStructureInfo.ControllerPlayerOne.HasCardsInArsenal() || !gameStructureInfo.ControllerPlayerTwo.HasCardsInArsenal()) && gameStructureInfo.WinnerPlayer == null)
            gameStructureInfo.WinnerPlayer = (gameStructureInfo.ControllerCurrentPlayer.HasCardsInArsenal()) ? gameStructureInfo.ControllerCurrentPlayer : gameStructureInfo.ControllerOpponentPlayer;
        return (gameStructureInfo.ControllerPlayerOne.HasCardsInArsenal() && gameStructureInfo.ControllerPlayerTwo.HasCardsInArsenal() && gameStructureInfo.IsTheGameStillPlaying);
    }
    
    public bool TheTurnIsBeingPlayed()
    {
        return gameStructureInfo.IsTheTurnBeingPlayed;
    }
    
    public void UpdateVariablesAtEndOfTurn()
    {   
        DeclareEndOfTurn();
        if (!CheckIfPlayersHasCardsInArsenalToContinuePlaying())
        {   
            PlayerController loserPlayer = (gameStructureInfo.ControllerCurrentPlayer.HasCardsInArsenal()) ? gameStructureInfo.ControllerOpponentPlayer : gameStructureInfo.ControllerCurrentPlayer;
            SetVariablesAfterWinning(loserPlayer);
        }
        UpdateNumberOfPlayers();
    }
    
    private void DeclareEndOfTurn()
    {
        gameStructureInfo.IsTheTurnBeingPlayed = false;
    }
    
    private bool CheckIfPlayersHasCardsInArsenalToContinuePlaying()
    {   
        return gameStructureInfo.ControllerCurrentPlayer.HasCardsInArsenal() && gameStructureInfo.ControllerOpponentPlayer.HasCardsInArsenal();
    }
    
    private void UpdateNumberOfPlayers()
    {   
        gameStructureInfo.ControllerCurrentPlayer = (gameStructureInfo.ControllerCurrentPlayer == gameStructureInfo.ControllerPlayerOne) ? gameStructureInfo.ControllerPlayerTwo : gameStructureInfo.ControllerPlayerOne;
        gameStructureInfo.ControllerOpponentPlayer = (gameStructureInfo.ControllerOpponentPlayer == gameStructureInfo.ControllerPlayerOne) ? gameStructureInfo.ControllerPlayerTwo : gameStructureInfo.ControllerPlayerOne;
    }
    
    public void SetVariablesAfterWinning(PlayerController loserPlayer)
    {   
        gameStructureInfo.WinnerPlayer =(gameStructureInfo.ControllerCurrentPlayer == loserPlayer) ?  gameStructureInfo.ControllerOpponentPlayer : gameStructureInfo.ControllerCurrentPlayer;
        gameStructureInfo.IsTheGameStillPlaying = false;
        DeclareEndOfTurn();
    }

    public void SetVariablesAfterGaveUp()
    {   
        gameStructureInfo.WinnerPlayer = (gameStructureInfo.ControllerCurrentPlayer == gameStructureInfo.ControllerPlayerOne) ? gameStructureInfo.ControllerPlayerTwo : gameStructureInfo.ControllerPlayerOne;
        gameStructureInfo.IsTheGameStillPlaying = false;
        DeclareEndOfTurn();
    }
    
    public string GetWinnerSuperstarName() 
    {   
        return gameStructureInfo.WinnerPlayer.NameOfSuperStar();
    }
    
}