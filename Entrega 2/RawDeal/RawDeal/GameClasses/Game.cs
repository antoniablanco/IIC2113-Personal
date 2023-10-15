using RawDeal.DecksBehavior;
using RawDeal.Exceptions;
using RawDeal.PlayerClasses;
using RawDeal.SuperStarClasses;
using RawDealView;
using RawDealView.Options;

namespace RawDeal.GameClasses;

public class Game
{
    private View _view;
    private string _deckFolder;
    private GameLogic _gameLogic = new GameLogic();
    private GameStructureInfo gameStructureInfo = new GameStructureInfo();
    private SuperAbilityInformation SuperAbilityInformation = new SuperAbilityInformation();
    private PlayCard PlayCard = new PlayCard();
    private GetSetGameVariables GetSetGameVariables = new GetSetGameVariables();
    private Effects Effects = new Effects();
    
    
    public Game(View view, string deckFolder)
    {
        _view = view;
        _deckFolder = deckFolder;
        AssigningClassesToGameStructure();
        AssigningClassGameStructureToClasses();
    }
    
    private void AssigningClassesToGameStructure()
    {
        gameStructureInfo.view = _view;
        gameStructureInfo.GameLogic = _gameLogic;
        gameStructureInfo.PlayCard = PlayCard;
        gameStructureInfo.GetSetGameVariables = GetSetGameVariables;
        gameStructureInfo.Effects = Effects;
    }
    
    private void AssigningClassGameStructureToClasses()
    {
        _gameLogic.GameStructureInfo = gameStructureInfo;
        PlayCard.gameStructureInfo = gameStructureInfo;
        GetSetGameVariables.gameStructureInfo = gameStructureInfo;
        Effects.gameStructureInfo = gameStructureInfo;
    }
    
    public void Play() 
    {
        try
        {
            PlayersGenerator playersGenerator = new PlayersGenerator(gameStructureInfo, _deckFolder);
            RunGameGivenThatTheDecksAreValid();
        }
        catch (InvalidDeckException e)
        {
            _view.SayThatDeckIsInvalid();
        }
    }
    
    private void RunGameGivenThatTheDecksAreValid()
    {
        while (gameStructureInfo.GetSetGameVariables.ShouldWeContinueTheGame())
        {
            PlayOneTurn();
        }
        _view.CongratulateWinner(_gameLogic.GetWinnerSuperstarName());
    }

    private void PlayOneTurn()
    {   
        SetTurnStartInformation();

        while (gameStructureInfo.GetSetGameVariables.TheTurnIsBeingPlayed())
        {   
            gameStructureInfo.TurnCounterForJokeyingForPosition -= 1;
            _gameLogic.DisplayPlayerInformation();
            PlayerSelectedAction();
        }
    }

    private void SetTurnStartInformation()
    {
        gameStructureInfo.ControllerCurrentPlayer.DrawCard();
        gameStructureInfo.GetSetGameVariables.SetVariableTrueBecauseTurnStarted();
        gameStructureInfo.view.SayThatATurnBegins(gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar());
        SuperAbilityInformation.TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed(gameStructureInfo);
        gameStructureInfo.ControllerCurrentPlayer.BlockinSuperAbilityBecauseIsJustAtTheStartOfTheTurn();
    }

    private void PlayerSelectedAction()
    {
        var activityToPerform = GetNextMove();

        switch (activityToPerform)
        {
            case NextPlay.UseAbility:
                SuperAbilityInformation.ActionUseSuperAbility(gameStructureInfo);
                break;
            case NextPlay.ShowCards:
                _gameLogic.SelectCardsToView();
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
        
        if (SuperAbilityInformation.PlayerCanUseSuperStarAbility(gameStructureInfo))
            activityToPerform = _view.AskUserWhatToDoWhenUsingHisAbilityIsPossible();
        else
            activityToPerform = _view.AskUserWhatToDoWhenHeCannotUseHisAbility();

        return activityToPerform;
    }
    
}