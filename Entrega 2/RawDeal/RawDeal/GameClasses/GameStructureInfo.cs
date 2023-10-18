using RawDeal.CardClasses;
using RawDeal.DecksBehavior;
using RawDeal.EffectsClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.GameClasses;

public class GameStructureInfo
{
    public int BonusDamage = 4;
    public int BonusFortitude = 8;
    public BonusManager BonusManager;
    public CardMovement CardMovement =new CardMovement();
    public CardsVisualizor CardsVisualizor = new CardsVisualizor();
    public PlayerController ControllerCurrentPlayer;
    public PlayerController ControllerOpponentPlayer;
    public PlayerController ControllerPlayerOne;
    public PlayerController ControllerPlayerTwo;
    public DamageEffects DamageEffects;
    public Effects Effects;
    public EndTurnManager EndTurnManager;
    public GetSetGameVariables GetSetGameVariables;
    public int IsJockeyingForPositionBonusDamageActive = 0;
    public int IsJockeyingForPositionBonusFortitudActive = 0;
    public bool IsTheGameStillPlaying = true;
    public bool IsTheTurnBeingPlayed = true;
    public int LastDamageComited = 0;
    public CardController LastPlayedCard;
    public string LastPlayedType;
    public PlayCard PlayCard;
    public Player PlayerOne;
    public Player PlayerTwo;
    public int TurnCounterForJokeyingForPosition = 0;
    public View View;
    public ViewDecks ViewDecks;
    public PlayerController WhoActivateJockeyingForPosition;
    public PlayerController WinnerPlayer;

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

