using RawDeal.GameClasses;
using RawDealView.Options;

namespace RawDeal.DecksBehavior;

public class DeckViewer
{
    private readonly GameStructureInfo gameStructureInfo;

    public DeckViewer(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }

    public void SelectCardsToViewAction()
    {
        gameStructureInfo.BonusManager.AddOneTurnFromBonusCounter();
        SelectCardsToView();
    }

    private void SelectCardsToView()
    {
        var setCardsToView = gameStructureInfo.View.AskUserWhatSetOfCardsHeWantsToSee();
        switch (setCardsToView)
        {
            case CardSet.Hand:
                ShowTotalCardsOf(gameStructureInfo.ControllerCurrentPlayer.GetStringCardsFrom("Hand"));
                break;
            case CardSet.RingArea:
                ShowTotalCardsOf(gameStructureInfo.ControllerCurrentPlayer.GetStringCardsFrom("RingArea"));
                break;
            case CardSet.RingsidePile:
                ShowTotalCardsOf(gameStructureInfo.ControllerCurrentPlayer.GetStringCardsFrom("RingSide"));
                break;
            case CardSet.OpponentsRingArea:
                ShowTotalCardsOf(gameStructureInfo.ControllerOpponentPlayer.GetStringCardsFrom("RingArea"));
                break;
            case CardSet.OpponentsRingsidePile:
                ShowTotalCardsOf(gameStructureInfo.ControllerOpponentPlayer.GetStringCardsFrom("RingSide"));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ShowTotalCardsOf(List<string> stringCardSet)
    {
        gameStructureInfo.View.ShowCards(stringCardSet);
    }
}