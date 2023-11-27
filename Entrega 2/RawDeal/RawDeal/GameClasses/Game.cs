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
        while (gameStructureInfo.GetSetGameVariables.ShouldWeContinueTheGame()) PlayOneTurn();
        view.CongratulateWinner(gameStructureInfo.GetSetGameVariables.GetWinnerSuperstarName());
    }

    private void PlayOneTurn()
    {
        SetTurnStartInformation();

        while (gameStructureInfo.GetSetGameVariables.TheTurnIsBeingPlayed())
        {
            gameStructureInfo.BonusManager.RemoveOneTurnFromBonusCounter();
            DisplayPlayerInformation();
            PlayerSelectedAction();
        }
    }

    private void SetTurnStartInformation()
    {
        SetVariablesBecauseTurnStarted();
        gameStructureInfo.ControllerCurrentPlayer.DrawCard();
        view.SayThatATurnBegins(gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar());
        superAbilityInformation.TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed(gameStructureInfo);
        gameStructureInfo.ControllerCurrentPlayer.BlockSuperAbilityBecauseIsJustAtTheStartOfTheTurn();
    }

    private void SetVariablesBecauseTurnStarted()
    {
        gameStructureInfo.IsTheTurnBeingPlayed = true;
        gameStructureInfo.LastDamageComited = 0;
        gameStructureInfo.NumberOfRoundsInTheTurn = 0;
        gameStructureInfo.ControllerCurrentPlayer.TheTurnHasJustStartTheSuperStarHasNotUsedHisSuperAbility();
    }

    private void DisplayPlayerInformation()
    {
        PlayerInfo playerOne = new PlayerInfo(gameStructureInfo.ControllerPlayerOne.NameOfSuperStar(),
            gameStructureInfo.ControllerPlayerOne.FortitudRating(),
            gameStructureInfo.ControllerPlayerOne.NumberOfCardIn("Hand"),
            gameStructureInfo.ControllerPlayerOne.NumberOfCardIn("Arsenal"));
        PlayerInfo playerTwo = new PlayerInfo(gameStructureInfo.ControllerPlayerTwo.NameOfSuperStar(),
            gameStructureInfo.ControllerPlayerTwo.FortitudRating(),
            gameStructureInfo.ControllerPlayerTwo.NumberOfCardIn("Hand"),
            gameStructureInfo.ControllerPlayerTwo.NumberOfCardIn("Arsenal"));

        List<PlayerInfo>  playersListToPrint = new List<PlayerInfo> { playerOne, playerTwo };

        int numCurrentPlayer =
            gameStructureInfo.ControllerCurrentPlayer == gameStructureInfo.ControllerPlayerOne ? 0 : 1;
        int numOppositePlayer =
            gameStructureInfo.ControllerOpponentPlayer == gameStructureInfo.ControllerPlayerOne ? 0 : 1;

        gameStructureInfo.View.ShowGameInfo(playersListToPrint[numCurrentPlayer],
            playersListToPrint[numOppositePlayer]);
    }

    private void PlayerSelectedAction()
    {
        NextPlay activityToPerform = GetNextMove();

        switch (activityToPerform)
        {
            case NextPlay.UseAbility:
                superAbilityInformation.UseSuperAbilityAction(gameStructureInfo);
                break;
            case NextPlay.ShowCards:
                gameStructureInfo.ViewDecks.SelectCardsToViewAction();
                break;
            case NextPlay.PlayCard:
                gameStructureInfo.PlayCard.PlayCardAction();
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

        if (superAbilityInformation.PlayerCanUseSuperStarAbility(gameStructureInfo))
            activityToPerform = view.AskUserWhatToDoWhenUsingHisAbilityIsPossible();
        else
            activityToPerform = view.AskUserWhatToDoWhenHeCannotUseHisAbility();

        return activityToPerform;
    }
}