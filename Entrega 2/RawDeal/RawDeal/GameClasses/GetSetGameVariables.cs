#region

using RawDeal.PlayerClasses;

#endregion

namespace RawDeal.GameClasses;

public class GetSetGameVariables
{
    private GameStructureInfo gameStructureInfo = new();

    public GetSetGameVariables(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }

    public void CreatePlayerInitialOrder()
    {
        gameStructureInfo.ControllerCurrentPlayer =
            gameStructureInfo.ControllerPlayerOne.GetSuperStarValue() <
            gameStructureInfo.ControllerPlayerTwo.GetSuperStarValue()
                ? gameStructureInfo.ControllerPlayerTwo
                : gameStructureInfo.ControllerPlayerOne;
        
        gameStructureInfo.ControllerOpponentPlayer =
            gameStructureInfo.ControllerPlayerOne.GetSuperStarValue() <
            gameStructureInfo.ControllerPlayerTwo.GetSuperStarValue()
                ? gameStructureInfo.ControllerPlayerOne
                : gameStructureInfo.ControllerPlayerTwo;
    }

    public bool ShouldContinueTheGame()
    {
        if ((!gameStructureInfo.ControllerPlayerOne.HasCardsInArsenal() ||
             !gameStructureInfo.ControllerPlayerTwo.HasCardsInArsenal())
            && gameStructureInfo.WinnerPlayer == null)
            gameStructureInfo.WinnerPlayer = gameStructureInfo.ControllerCurrentPlayer.HasCardsInArsenal()
                ? gameStructureInfo.ControllerCurrentPlayer
                : gameStructureInfo.ControllerOpponentPlayer;
        return gameStructureInfo.ControllerPlayerOne.HasCardsInArsenal() && gameStructureInfo.ControllerPlayerTwo
                                                                             .HasCardsInArsenal()
                                                                         && gameStructureInfo.IsTheGameStillPlaying;
    }

    public bool IsTheTurnIsBeingPlayed()
    {
        return gameStructureInfo.IsTheTurnBeingPlayed;
    }

    public void SetVariablesAfterWinning(PlayerController loserPlayer)
    {
        gameStructureInfo.WinnerPlayer = gameStructureInfo.ControllerCurrentPlayer == loserPlayer
            ? gameStructureInfo.ControllerOpponentPlayer
            : gameStructureInfo.ControllerCurrentPlayer;
        gameStructureInfo.IsTheGameStillPlaying = false;
        gameStructureInfo.EndTurnManager.DeclareEndOfTurn();
    }

    public void SetVariablesAfterGaveUp()
    {
        gameStructureInfo.WinnerPlayer =
            gameStructureInfo.ControllerCurrentPlayer == gameStructureInfo.ControllerPlayerOne
                ? gameStructureInfo.ControllerPlayerTwo
                : gameStructureInfo.ControllerPlayerOne;
        gameStructureInfo.IsTheGameStillPlaying = false;
        gameStructureInfo.EndTurnManager.DeclareEndOfTurn();
    }

    public string GetWinnerSuperstarName()
    {
        return gameStructureInfo.WinnerPlayer.GetNameOfSuperStar();
    }

    public void AddOneRoundMoreInTurn()
    {
        gameStructureInfo.NumberOfRoundsInTheTurn += 1;
    }

    public void DecrementRoundInTurn()
    {
        gameStructureInfo.NumberOfRoundsInTheTurn -= 1;
    }

    public int GetRoundsInTurn()
    {
        return gameStructureInfo.NumberOfRoundsInTheTurn;
    }
}