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
        new CreateClasses(gameStructureInfo);
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
        view.CongratulateWinner(gameStructureInfo.GameLogic.GetWinnerSuperstarName());
    }

    private void PlayOneTurn()
    {   
        SetTurnStartInformation();

        while (gameStructureInfo.GetSetGameVariables.TheTurnIsBeingPlayed())
        {   
            gameStructureInfo.GetSetGameVariables.RemoveOneTurnFromJockeyingForPosition();
            gameStructureInfo.GameLogic.DisplayPlayerInformation();
            PlayerSelectedAction();
        }
    }

    private void SetTurnStartInformation()
    {
        gameStructureInfo.ControllerCurrentPlayer.DrawCard();
        gameStructureInfo.GetSetGameVariables.SetVariableTrueBecauseTurnStarted();
        view.SayThatATurnBegins(gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar());
        superAbilityInformation.TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed(gameStructureInfo);
        gameStructureInfo.ControllerCurrentPlayer.BlockSuperAbilityBecauseIsJustAtTheStartOfTheTurn();
    }

    private void PlayerSelectedAction()
    {
        var activityToPerform = GetNextMove();

        switch (activityToPerform)
        {
            case NextPlay.UseAbility:
                superAbilityInformation.ActionUseSuperAbility(gameStructureInfo);
                break;
            case NextPlay.ShowCards:
                gameStructureInfo.GameLogic.SelectCardsToView();
                break;
            case NextPlay.PlayCard:
                gameStructureInfo.PlayCard.ActionPlayCard();
                break;
            case NextPlay.EndTurn:
                gameStructureInfo.GetSetGameVariables.UpdateVariablesAtEndOfTurn();
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