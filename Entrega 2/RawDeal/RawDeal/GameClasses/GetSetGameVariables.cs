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
    
    public void SetVariablesAfterWinning(PlayerController loserPlayer)
    {   
        gameStructureInfo.WinnerPlayer =(gameStructureInfo.ControllerCurrentPlayer == loserPlayer) ?  gameStructureInfo.ControllerOpponentPlayer : gameStructureInfo.ControllerCurrentPlayer;
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
        if (!CheckIfPlayersHasCardsInArsenalToContinuePlaying())
        {   
            SetVariablesAfterLosing();
        }
        UpdateNumberOfPlayers();
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

    private void SetVariablesAfterLosing() 
    {   
        gameStructureInfo.WinnerPlayer = (gameStructureInfo.ControllerCurrentPlayer.HasCardsInArsenal()) ? gameStructureInfo.ControllerCurrentPlayer : gameStructureInfo.ControllerOpponentPlayer;
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
    
    /*
    public void ActivateJockeyingForPositionBonusFortitud()
    {
        gameStructureInfo.IsJockeyingForPositionBonusFortitudActive = 1;
    }
    
    public void ActivateJockeyingForPositionBonusDamage()
    {
        gameStructureInfo.IsJockeyingForPositionBonusDamageActive = 1;
    }
    
    public void DesactivateJockeyingForPositionBonusFortitud()
    {
        gameStructureInfo.IsJockeyingForPositionBonusFortitudActive = 0;
    }
    
    public void DesactivateJockeyingForPositionBonusDamage()
    {
        gameStructureInfo.IsJockeyingForPositionBonusDamageActive = 0;
    }

    public int AddBonusFortitud()
    {
        return gameStructureInfo.BonusFortitude * gameStructureInfo.IsJockeyingForPositionBonusFortitudActive;
    }

    public int AddBonusDamage()
    {
        return gameStructureInfo.BonusDamage * gameStructureInfo.IsJockeyingForPositionBonusDamageActive;
    }
    
    public void AddingOneTurnJockeyingForPosition()
    {
        gameStructureInfo.TurnCounterForJokeyingForPosition += 1;
    }
    
    public void RemoveOneTurnFromJockeyingForPosition()
    {
        gameStructureInfo.TurnCounterForJokeyingForPosition -= 1;
    }
    */

    
}