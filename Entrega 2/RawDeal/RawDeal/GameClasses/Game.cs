using RawDeal.DecksBehavior;
using RawDeal.Exceptions;
using RawDeal.PlayerClasses;
using RawDeal.SuperStarClasses;
using RawDealView;
using RawDealView.Options;

namespace RawDeal.GameClasses;

public class Game
{
    private View view;
    private string deckFolder;
    private GameStructureInfo gameStructureInfo = new GameStructureInfo();
    private SuperAbilityInformation superAbilityInformation = new SuperAbilityInformation();
    
    
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
        while (gameStructureInfo.GetSetGameVariables.ShouldWeContinueTheGame())
        {
            PlayOneTurn();
        }
        view.CongratulateWinner(gameStructureInfo.GetSetGameVariables.GetWinnerSuperstarName());
    }

    private void PlayOneTurn()
    {   
        SetTurnStartInformation();

        while (gameStructureInfo.GetSetGameVariables.TheTurnIsBeingPlayed())
        {   
            gameStructureInfo.BonusManager.RemoveOneTurnFromJockeyingForPosition();
            DisplayPlayerInformation();
            PlayerSelectedAction();
        }
    }

    private void SetTurnStartInformation()
    {
        gameStructureInfo.ControllerCurrentPlayer.DrawCard();
        SetVariableTrueBecauseTurnStarted();
        view.SayThatATurnBegins(gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar());
        superAbilityInformation.TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed(gameStructureInfo);
        gameStructureInfo.ControllerCurrentPlayer.BlockSuperAbilityBecauseIsJustAtTheStartOfTheTurn();
    }

    private void SetVariableTrueBecauseTurnStarted()
    {   
        gameStructureInfo.IsTheTurnBeingPlayed = true;
        gameStructureInfo.LastDamageComited = 0;
        gameStructureInfo.ControllerCurrentPlayer.TheTurnHasJustStartTheSuperStarHasNotUsedHisSuperAbility();
    }

    private void DisplayPlayerInformation() 
    {   
        PlayerInfo playerUno = new PlayerInfo(gameStructureInfo.ControllerPlayerOne.NameOfSuperStar(), gameStructureInfo.ControllerPlayerOne.FortitudRating(), gameStructureInfo.ControllerPlayerOne.NumberOfCardIn("Hand"), gameStructureInfo.ControllerPlayerOne.NumberOfCardIn("Arsenal"));
        PlayerInfo playerDos = new PlayerInfo(gameStructureInfo.ControllerPlayerTwo.NameOfSuperStar(), gameStructureInfo.ControllerPlayerTwo.FortitudRating(), gameStructureInfo.ControllerPlayerTwo.NumberOfCardIn("Hand"), gameStructureInfo.ControllerPlayerTwo.NumberOfCardIn("Arsenal"));
        
        List<PlayerInfo> playersListToPrint =  new List<PlayerInfo> { playerUno, playerDos };
        
        int numCurrentPlayer = gameStructureInfo.ControllerCurrentPlayer == gameStructureInfo.ControllerPlayerOne ? 0 : 1;
        int numOppositePlayer = gameStructureInfo.ControllerOpponentPlayer == gameStructureInfo.ControllerPlayerOne ? 0 : 1;

        gameStructureInfo.View.ShowGameInfo(playersListToPrint[numCurrentPlayer], playersListToPrint[numOppositePlayer]);
    }
    
    private void PlayerSelectedAction()
    {
        var activityToPerform = GetNextMove();

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