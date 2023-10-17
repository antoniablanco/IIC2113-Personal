using RawDeal.CardClass;
using RawDeal.DecksBehavior;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.GameClasses;

public class GameStructureInfo
{
    public View View;
    public CardsVisualizor CardsVisualizor = new CardsVisualizor();
    public CardMovement CardMovement =new CardMovement();
    public PlayCard PlayCard;
    public Player PlayerOne;
    public Player PlayerTwo;
    public PlayerController ControllerPlayerOne;
    public PlayerController ControllerPlayerTwo;
    public PlayerController ControllerCurrentPlayer;
    public PlayerController ControllerOpponentPlayer;
    public PlayerController WinnerPlayer;
    public PlayerController WhoActivateJockeyingForPosition;
    public GetSetGameVariables GetSetGameVariables;
    public Effects Effects;
    public CardController LastPlayedCard;
    public ViewDecks ViewDecks;
    public BonusManager BonusManager;
    public bool IsTheGameStillPlaying = true;
    public bool IsTheTurnBeingPlayed = true;
    public string LastPlayedType;
    public int BonusFortitude = 8;
    public int BonusDamage = 4;
    public int IsJockeyingForPositionBonusFortitudActive = 0;
    public int IsJockeyingForPositionBonusDamageActive = 0;
    public int TurnCounterForJokeyingForPosition = 0;
    public int LastDamageComited = 0;
    
    public Player GetCurrentPlayer()
    {
        Player player = (ControllerCurrentPlayer == ControllerPlayerOne) ? PlayerOne : PlayerTwo;
        return player;
    }
    
    public Player GetOpponentPlayer()
    {
        Player player = (ControllerCurrentPlayer == ControllerPlayerOne) ? PlayerTwo : PlayerOne;
        return player;
    }
}

