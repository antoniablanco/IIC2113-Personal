using RawDeal.CardClasses;
using RawDeal.DecksBehavior;
using RawDeal.EffectsClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.GameClasses;

public class GameStructureInfo
{
    public BonusManager BonusManager;
    public CardMovement CardMovement;
    public CardsVisualizer CardsVisualizer = new CardsVisualizer();
    public PlayerController ControllerCurrentPlayer;
    public PlayerController ControllerOpponentPlayer;
    public PlayerController ControllerPlayerOne;
    public PlayerController ControllerPlayerTwo;
    public EffectsUtils EffectsUtils;
    public EndTurnManager EndTurnManager;
    public GetSetGameVariables GetSetGameVariables;
    public bool IsTheGameStillPlaying = true;
    public bool IsTheTurnBeingPlayed = true;
    public int LastDamageCommitted = 0;
    public CardController CardBeingPlayed;
    public string CardBeingPlayedType;
    public string LastCardBeingPlayedTitle;
    public string LastCardBeingPlayedType;
    public CardPlay CardPlay;
    public Player PlayerOne;
    public Player PlayerTwo;
    public View View;
    public DeckViewer DeckViewer;
    public PlayerController WinnerPlayer;
    public BonusStructureInfo BonusStructureInfo = new BonusStructureInfo();
    public int NumberOfRoundsInTheTurn = 0;

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

