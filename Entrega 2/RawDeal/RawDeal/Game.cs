using System.ComponentModel.Design;
using RawDealView;
using RawDealView.Options;
using RawDeal.SuperStarClass;
using RawDeal.CardClass;
using RawDeal.DecksBehavior;
using RawDeal.Exceptions;
using RawDeal.PlayerClass;

namespace RawDeal;

public class Game
{
    private View _view;
    private string _deckFolder;
    private GameLogic _gameLogic = new GameLogic();
    private CreateCards _createCards = new CreateCards();
    public GameStructureInfo gameStructureInfo = new GameStructureInfo();
    public PlayCard PlayCard = new PlayCard();
    
    
    public Game(View view, string deckFolder)
    {
        _view = view;
        _deckFolder = deckFolder;
        AsignGameStructureInfo();
    }
    
    public void AsignGameStructureInfo()
    {
        _gameLogic.gameStructureInfo = gameStructureInfo;
        gameStructureInfo.view = _view;
        gameStructureInfo.GameLogic = _gameLogic;
        PlayCard.gameStructureInfo = gameStructureInfo;
        gameStructureInfo.PlayCard = PlayCard;
    }
    
    public void Play() 
    {
        try
        {
            CreatePlayers();
            GameGivenThatTheDecksAreValid();
        }
        catch (InvalidDeckException e)
        {
            _view.SayThatDeckIsInvalid();
        }
    }
    
    private void CreatePlayers()
    {
        PlayerController playerUno = CreateOnePlayer();
        PlayerController playerDos = CreateOnePlayer();
        InitializeGameVariables(playerUno, playerDos);
        InitializePlayerHands();
    }
    
    private PlayerController CreateOnePlayer()
    {
        var (totalCards, totalSuperStars) = GetTotalCardsAndSuperStars();
        Player player = InitializePlayer(totalCards, totalSuperStars);
        ValidateDeck _validateDeck = new ValidateDeck(player);
        
        if (!_validateDeck.IsValidDeck())
        {
            throw new InvalidDeckException("The Deck Is Not Valid");
        }

        PlayerController playerController = InitializePlayerController(player);
        return playerController;
    }
    
    private (List<CardJson>, List<SuperStarJSON>) GetTotalCardsAndSuperStars() 
    {
        List<CardJson> totalCards = _createCards.DeserializeJsonCards();
        List<SuperStarJSON> totalSuperStars = _gameLogic.DeserializeJsonSuperStar();
    
        return (totalCards, totalSuperStars);
    }
    
    private Player InitializePlayer(List<CardJson> totalCards,List<SuperStarJSON> totalSuperStars) 
    {
        string stringPlayer = _view.AskUserToSelectDeck(_deckFolder);
        List<CardController> playerCardList = _createCards.CreateDiferentTypesOfCard(stringPlayer, totalCards, _view);
        SuperStar? superStarPlayer = _gameLogic.CreateSuperStar(stringPlayer, totalSuperStars);
        
        Player playerReturn = new Player(playerCardList, superStarPlayer);

        SavePlayerInGameStructureInfo(playerReturn);
        
        return playerReturn;
    }

    private void SavePlayerInGameStructureInfo(Player player)
    {
        if (gameStructureInfo.playerOne == null)
            gameStructureInfo.playerOne = player;
        else
            gameStructureInfo.playerTwo = player;
    }

    private PlayerController InitializePlayerController(Player player)
    {
        PlayerController playerController = new PlayerController(player);
        return playerController;
    }
    
    private void InitializeGameVariables(PlayerController playerOne, PlayerController playerTwo) 
    {
        gameStructureInfo.ControllerPlayerOne = playerOne;
        gameStructureInfo.ControllerPlayerTwo = playerTwo;
        
        _gameLogic.CreatePlayerInitialOrder();
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
                gameStructureInfo.PlayCard.ActionPlayCard();
                break;
            case NextPlay.EndTurn:
                _gameLogic.UpdateVariablesAtEndOfTurn();
                break;
            case NextPlay.GiveUp:
                _gameLogic.SetVariablesAfterGaveUp();
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