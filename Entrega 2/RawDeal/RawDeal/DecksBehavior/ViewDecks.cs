using RawDeal.GameClasses;
using RawDealView.Options;

namespace RawDeal.DecksBehavior;

public class ViewDecks
{
    private readonly GameStructureInfo gameStructureInfo;

    public ViewDecks(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }

    public void SelectCardsToViewAction()
    {
        gameStructureInfo.BonusManager.AddingOneTurnJockeyingForPosition();
        SelectCardsToView();
    }

    private void SelectCardsToView()
    {
        var setCardsToView = gameStructureInfo.View.AskUserWhatSetOfCardsHeWantsToSee();
        switch (setCardsToView)
        {
            case CardSet.Hand:
                ActionSeeTotalCards(gameStructureInfo.ControllerCurrentPlayer.StringCardsFrom("Hand"));
                break;
            case CardSet.RingArea:
                ActionSeeTotalCards(gameStructureInfo.ControllerCurrentPlayer.StringCardsFrom("RingArea"));
                break;
            case CardSet.RingsidePile:
                ActionSeeTotalCards(gameStructureInfo.ControllerCurrentPlayer.StringCardsFrom("RingSide"));
                break;
            case CardSet.OpponentsRingArea:
                ActionSeeTotalCards(gameStructureInfo.ControllerOpponentPlayer.StringCardsFrom("RingArea"));
                break;
            case CardSet.OpponentsRingsidePile:
                ActionSeeTotalCards(gameStructureInfo.ControllerOpponentPlayer.StringCardsFrom("RingSide"));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ActionSeeTotalCards(List<string> stringCardSet)
    {
        gameStructureInfo.View.ShowCards(stringCardSet);
    }
}