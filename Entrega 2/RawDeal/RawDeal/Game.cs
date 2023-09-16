using System.ComponentModel.Design;
using RawDealView;
using RawDealView.Options;
using RawDeal.SuperStarClases;

namespace RawDeal;

public class Game
{
    private View _view;
    private string _deckFolder;
    private Logica_Juego _logicaJuego = new Logica_Juego();
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
        List<CardJson> totalCards = _logicaJuego.DeserializeJsonCards();
        List<SuperStarJSON> totalSuperStars = _logicaJuego.DeserializeJsonSuperStar();
    
        return (totalCards, totalSuperStars);
    }
    
    private Player InitializePlayer(List<CardJson> totalCards,List<SuperStarJSON> totalSuperStars) 
    {
        string stringPlayer = _view.AskUserToSelectDeck(_deckFolder);
        _logicaJuego.view = _view;
        List<Card> playerCardList = _logicaJuego.CrearCartas(stringPlayer, totalCards);
        SuperStar? superStarPlayer = _logicaJuego.CrearSuperStar(stringPlayer, totalSuperStars);
        
        Player playerReturn = new Player(playerCardList, superStarPlayer);
        
        return playerReturn;
    }
    
    private void InitializeGameLogicVariables(Player playerOne, Player playerTwo)
    {
        _logicaJuego.playerOne = playerOne;
        _logicaJuego.playerTwo = playerTwo;
        
        _logicaJuego.JugadorInicioJuego();
        _logicaJuego.CrearListaPlayers();
    }

    private void InitializePlayerHands()
    {
        _logicaJuego.ThePlayerDrawTheirInitialsHands();
    }
    
    private void GameGivenThatTheDecksAreValid()
    {
        while (_logicaJuego.ShouldWeContinueTheGame())
        {
            OneTurnIsPlayed();
        }
        _view.CongratulateWinner(_logicaJuego.GetWinnerSuperstarName());
    }

    private void OneTurnIsPlayed()
    {
        _logicaJuego.SettingTurnStartInformation();

        while (_logicaJuego.TheTurnIsBeingPlayed())
        {
            _logicaJuego.DisplayPlayerInformation();
            PlayerSelectedAction();
        }
    }

    private void PlayerSelectedAction()
    {
        var activityToPerform = GetNextMove();

        switch (activityToPerform)
        {
            case NextPlay.UseAbility:
                _logicaJuego.ActionUseSuperAbility();
                break;
            case NextPlay.ShowCards:
                _logicaJuego.SelectCardsToView();
                break;
            case NextPlay.PlayCard:
                _logicaJuego.ActionPlayCard();
                break;
            case NextPlay.EndTurn:
                _logicaJuego.UpdateVariablesAtEndOfTurn();
                break;
            case NextPlay.GiveUp:
                _logicaJuego.SetVariablesAfterLosing();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private NextPlay GetNextMove()
    {   
        NextPlay activityToPerform;
        
        if (_logicaJuego.PlayerCanUseSuperStarAbility())
            activityToPerform = _view.AskUserWhatToDoWhenUsingHisAbilityIsPossible();
        else
            activityToPerform = _view.AskUserWhatToDoWhenHeCannotUseHisAbility();

        return activityToPerform;
    }
    
}