using RawDeal.CardClass;
using RawDeal.DecksBehavior;
using RawDeal.PlayerClass;

namespace RawDeal;
using RawDeal.SuperStarClass;
using RawDealView;
using RawDealView.Options;

public class GameStructureInfo
{
    public View view;
    public VisualizeCards VisualizeCards = new VisualizeCards();
    public CardMovement CardMovement =new CardMovement();
    public PlayCard PlayCard;
    public Player playerOne;
    public Player playerTwo;
    public PlayerController ControllerPlayerOne;
    public PlayerController ControllerPlayerTwo;
    public PlayerController ControllerCurrentPlayer;
    public PlayerController ControllerOpponentPlayer;
    public PlayerController winnerPlayer;
    public GameLogic GameLogic;
    public GetSetGameVariables GetSetGameVariables;
    public Effects Effects;
    public bool IsTheGameStillPlaying = true;
    public bool IsTheTurnBeingPlayed = true;
    public CardController LastPlayedCard;
    public string LastPlayedType;
    public int bonusFortitude = 8;
    public int bonusDamage = 4;
    public int IsJockeyingForPositionBonusFortitud = 0;
    public int IsJockeyingForPositionBonusDamage = 0;
    public int ContadorTurnosJokeyingForPosition = 0;
    public int LastDamageComited = 0;
    public PlayerController HowActivateJockeyingForPosition;
    
    public Player GetCurrentPlayer()
    {
        Player player = (ControllerCurrentPlayer == ControllerPlayerOne) ? playerOne : playerTwo;
        return player;
    }
    
    public Player GetOpponentPlayer()
    {
        Player player = (ControllerCurrentPlayer == ControllerPlayerOne) ? playerTwo : playerOne;
        return player;
    }
}

