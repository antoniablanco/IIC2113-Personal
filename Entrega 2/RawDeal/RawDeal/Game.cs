using System.ComponentModel.Design;
using RawDealView;
using RawDealView.Options;
using RawDeal.SuperStarClases;

namespace RawDeal;

public class Game
{
    private View _view;
    private string _deckFolder;
    private GameLogic _gameLogic = new GameLogic();
    private ValidateDeck _validateDeck = new ValidateDeck();
    
    
    public Game(View view, string deckFolder)
    {
        _view = view;
        _deckFolder = deckFolder;
    }
    
    public void Play() 
    {
        try
        {
            Player playerUno = CreatePlayer();
            Player playerDos = CreatePlayer();
            InitializeGameLogicVariables(playerUno, playerDos);
            InitializePlayerHands();
            GameGivenThatTheDecksAreValid();
        }
        catch (InvalidDeckException e)
        {
            _view.SayThatDeckIsInvalid();
        }
    }
    
    private Player CreatePlayer()
    {
        var (totalCards, totalSuperStars) = GetTotalCardsAndSuperStars();
        Player player = InitializePlayer(totalCards, totalSuperStars);
        if (!_validateDeck.IsValidDeck(player))
        {
            throw new InvalidDeckException("The Deck Is Not Valid");
        }
        return player;
    }
    
    private (List<CardJson>, List<SuperStarJSON>) GetTotalCardsAndSuperStars() 
    {
        List<CardJson> totalCards = _gameLogic.DeserializeJsonCards();
        List<SuperStarJSON> totalSuperStars = _gameLogic.DeserializeJsonSuperStar();
    
        return (totalCards, totalSuperStars);
    }
    
    private Player InitializePlayer(List<CardJson> totalCards,List<SuperStarJSON> totalSuperStars) 
    {
        string stringPlayer = _view.AskUserToSelectDeck(_deckFolder);
        _gameLogic.view = _view;
        List<Card> playerCardList = _gameLogic.CreateCards(stringPlayer, totalCards);
        SuperStar? superStarPlayer = _gameLogic.CreateSuperStar(stringPlayer, totalSuperStars);
        
        Player playerReturn = new Player(playerCardList, superStarPlayer);
        
        return playerReturn;
    }
    
    private void InitializeGameLogicVariables(Player playerOne, Player playerTwo)
    {
        _gameLogic.playerOne = playerOne;
        _gameLogic.playerTwo = playerTwo;
        
        _gameLogic.PlayerStartedGame();
        _gameLogic.CreatePlayerList();
    }

    private void InitializePlayerHands()
    {
        _gameLogic.ThePlayerDrawTheirInitialsHands();
    }
    
    private void GameGivenThatTheDecksAreValid()
    {
        while (_gameLogic.ShouldWeContinueTheGame())
        {
            OneTurnIsPlayed();
        }
        _view.CongratulateWinner(_gameLogic.GetWinnerSuperstarName());
    }

    private void OneTurnIsPlayed()
    {
        _gameLogic.SettingTurnStartInformation();

        while (_gameLogic.TheTurnIsBeingPlayed())
        {
            _gameLogic.DisplayPlayerInformation();
            PlayerSelectedAction();
        }
    }

    private void PlayerSelectedAction()
    {
        var activityToPerform = GetNextMove();

        switch (activityToPerform)
        {
            case NextPlay.UseAbility:
                _gameLogic.ActionUseSuperAbility();
                break;
            case NextPlay.ShowCards:
                _gameLogic.SelectCardsToView();
                break;
            case NextPlay.PlayCard:
                _gameLogic.ActionPlayCard();
                break;
            case NextPlay.EndTurn:
                _gameLogic.UpdateVariablesAtEndOfTurn();
                break;
            case NextPlay.GiveUp:
                _gameLogic.SetVariablesAfterLosing();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private NextPlay GetNextMove()
    {   
        NextPlay activityToPerform;
        
        if (_gameLogic.PlayerCanUseSuperStarAbility())
            activityToPerform = _view.AskUserWhatToDoWhenUsingHisAbilityIsPossible();
        else
            activityToPerform = _view.AskUserWhatToDoWhenHeCannotUseHisAbility();

        return activityToPerform;
    }
    
}