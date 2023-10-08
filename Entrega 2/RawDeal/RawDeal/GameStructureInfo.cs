namespace RawDeal;
using RawDeal.SuperStarClass;
using RawDealView;
using RawDealView.Options;

public class GameStructureInfo
{
    public View view;
    public VisualizeCards VisualizeCards = new VisualizeCards();
    public PlayerController playerOne;
    public PlayerController playerTwo;
    public PlayerController currentPlayer;
    public PlayerController opponentPlayer;

}