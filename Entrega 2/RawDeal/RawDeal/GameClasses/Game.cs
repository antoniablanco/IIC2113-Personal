using RawDeal.Exceptions;
using RawDeal.PlayerClasses;
using RawDeal.SuperStarClasses;
using RawDealView;
using RawDealView.Options;

namespace RawDeal.GameClasses;

public class Game
{
    private readonly string deckFolder;
    private readonly GameStructureInfo gameStructureInfo = new();
    private readonly SuperAbilityInformation superAbilityInformation = new();
    private readonly View view;


    public Game(View view, string deckFolder)
    {
        this.view = view;
        this.deckFolder = deckFolder;
        CreateClasses();
    }

    private void CreateClasses()
    {
        gameStructureInfo.View = view;
        new ClassesGenerator(gameStructureInfo);
    }

    public void Play()
    {
        try
        {
            new PlayersGenerator(gameStructureInfo, deckFolder);
            RunGameGivenThatTheDecksAreValid();
        }
        catch (InvalidDeckException e)
        {
            view.SayThatDeckIsInvalid();
        }
    }

    private void RunGameGivenThatTheDecksAreValid()
    {
        while (gameStructureInfo.GetSetGameVariables.ShouldContinueTheGame()) PlayOneTurn();
        view.CongratulateWinner(gameStructureInfo.GetSetGameVariables.GetWinnerSuperstarName());
    }

    private void PlayOneTurn()
    {
        SetTurnStartInformation();

        while (gameStructureInfo.GetSetGameVariables.IsTheTurnIsBeingPlayed())
        {
            gameStructureInfo.BonusManager.RemoveOneTurnFromBonusCounter();
            DisplayPlayerInformation();
            SelectPlayerAction();
        }
    }

    private void SetTurnStartInformation()
    {
        SetVariablesAtTheStartOfTurn();
        gameStructureInfo.ControllerCurrentPlayer.DrawCard();
        view.SayThatATurnBegins(gameStructureInfo.ControllerCurrentPlayer.GetNameOfSuperStar());
        superAbilityInformation.UseStartOfTurnSuperAbility(gameStructureInfo);
        gameStructureInfo.ControllerCurrentPlayer.BlockSuperAbilityBecauseIsJustAtTheStartOfTheTurn();
    }

    private void SetVariablesAtTheStartOfTurn()
    {
        gameStructureInfo.IsTheTurnBeingPlayed = true;
        gameStructureInfo.LastDamageCommitted = 0;
        gameStructureInfo.NumberOfRoundsInTheTurn = 0;
        gameStructureInfo.ControllerCurrentPlayer.MarkSuperAbilityAsUnusedInThisTurn();
    }

    private void DisplayPlayerInformation()
    {
        PlayerInfo playerOne = new PlayerInfo(gameStructureInfo.ControllerPlayerOne.GetNameOfSuperStar(),
            gameStructureInfo.ControllerPlayerOne.FortitudeRating(),
            gameStructureInfo.ControllerPlayerOne.GetNumberOfCardIn("Hand"),
            gameStructureInfo.ControllerPlayerOne.GetNumberOfCardIn("Arsenal"));
        PlayerInfo playerTwo = new PlayerInfo(gameStructureInfo.ControllerPlayerTwo.GetNameOfSuperStar(),
            gameStructureInfo.ControllerPlayerTwo.FortitudeRating(),
            gameStructureInfo.ControllerPlayerTwo.GetNumberOfCardIn("Hand"),
            gameStructureInfo.ControllerPlayerTwo.GetNumberOfCardIn("Arsenal"));

        List<PlayerInfo>  playersListToPrint = new List<PlayerInfo> { playerOne, playerTwo };

        int numCurrentPlayer =
            gameStructureInfo.ControllerCurrentPlayer == gameStructureInfo.ControllerPlayerOne ? 0 : 1;
        int numOppositePlayer =
            gameStructureInfo.ControllerOpponentPlayer == gameStructureInfo.ControllerPlayerOne ? 0 : 1;

        gameStructureInfo.View.ShowGameInfo(playersListToPrint[numCurrentPlayer],
            playersListToPrint[numOppositePlayer]);
    }

    private void SelectPlayerAction()
    {
        NextPlay activityToPerform = GetNextMove();

        switch (activityToPerform)
        {
            case NextPlay.UseAbility:
                superAbilityInformation.UseSuperAbilityAction(gameStructureInfo);
                break;
            case NextPlay.ShowCards:
                gameStructureInfo.DeckViewer.SelectCardsToViewAction();
                break;
            case NextPlay.PlayCard:
                gameStructureInfo.CardPlay.PlayCardAction();
                break;
            case NextPlay.EndTurn:
                gameStructureInfo.EndTurnManager.UpdateVariablesAtEndOfTurn();
                break;
            case NextPlay.GiveUp:
                gameStructureInfo.GetSetGameVariables.SetVariablesAfterGaveUp();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private NextPlay GetNextMove()
    {
        NextPlay activityToPerform;

        if (superAbilityInformation.CanPlayerUseSuperStarAbility(gameStructureInfo))
            activityToPerform = view.AskUserWhatToDoWhenUsingHisAbilityIsPossible();
        else
            activityToPerform = view.AskUserWhatToDoWhenHeCannotUseHisAbility();

        return activityToPerform;
    }
}