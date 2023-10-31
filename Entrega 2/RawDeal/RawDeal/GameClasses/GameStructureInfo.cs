using RawDeal.CardClasses;
using RawDeal.DecksBehavior;
using RawDeal.EffectsClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.GameClasses;

public class GameStructureInfo
{
    public BonusManager BonusManager;
    public CardMovement CardMovement =new CardMovement();
    public CardsVisualizor CardsVisualizor = new CardsVisualizor();
    public PlayerController ControllerCurrentPlayer;
    public PlayerController ControllerOpponentPlayer;
    public PlayerController ControllerPlayerOne;
    public PlayerController ControllerPlayerTwo;
    public EffectsUtils EffectsUtils;
    public EndTurnManager EndTurnManager;
    public GetSetGameVariables GetSetGameVariables;
    public bool IsTheGameStillPlaying = true;
    public bool IsTheTurnBeingPlayed = true;
    public int LastDamageComited = 0;
    public CardController CardBeingPlayed;
    public CardController LastPlayedCard;
    public string CardBeingPlayedType;
    public PlayCard PlayCard;
    public Player PlayerOne;
    public Player PlayerTwo;
    public View View;
    public ViewDecks ViewDecks;
    public PlayerController WinnerPlayer;
    public BonusStructureInfo BonusStructureInfo = new BonusStructureInfo();

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

