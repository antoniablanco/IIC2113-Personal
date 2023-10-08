using RawDeal.DecksBehavior;

namespace RawDeal;
using RawDeal.SuperStarClass;
using RawDealView;
using RawDealView.Options;

public class GameStructureInfo
{
    public View view;
    public VisualizeCards VisualizeCards = new VisualizeCards();
    public CardMovement CardMovement =new CardMovement();
    public Player playerOne;
    public Player playerTwo;
    public PlayerController ControllerPlayerOne;
    public PlayerController ControllerPlayerTwo;
    public PlayerController ControllerCurrentPlayer;
    public PlayerController ControllerOpponentPlayer;
    public PlayerController winnerPlayer;
    
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

