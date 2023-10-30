using RawDeal.CardClasses;
using RawDeal.GameClasses;

namespace RawDeal.DecksBehavior;

public class PlayReversalHandCard
{
    private readonly GameStructureInfo gameStructureInfo = new();

    public PlayReversalHandCard(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }


    public bool IsUserUsingReversalCard()
    {
        var possibleReversals = gameStructureInfo.ControllerOpponentPlayer.CardsAvailableToReversal();

        if (possibleReversals.Count > 0)
            return AskWhichReversalCardWantsToUse(possibleReversals);

        return false;
    }

    private bool AskWhichReversalCardWantsToUse(List<CardController> possibleReversals)
    {
        var indexReversalCard = UserSelectReversalCard();
        if (gameStructureInfo.PlayCard.HasSelectedAValidCard(indexReversalCard))
        {
            PlayingReversalCard(indexReversalCard, possibleReversals);
            return true;
        }

        return false;
    }

    private int UserSelectReversalCard()
    {
        var possibleCardsAndTheirTypes = gameStructureInfo.ControllerOpponentPlayer
            .GetPosiblesCardsForReveralWithTheirReversalTypeIndex();
        var possibleReversalsString =
            gameStructureInfo.CardsVisualizor.GetStringCardsForSpecificType(possibleCardsAndTheirTypes);
        var indexReversalCard =
            gameStructureInfo.View.AskUserToSelectAReversal(
                gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), possibleReversalsString);
        return indexReversalCard;
    }

    private void PlayingReversalCard(int indexReversalCard, List<CardController> possibleReversals)
    {
        var cardController = possibleReversals[indexReversalCard];
        SayTheReversalCardIsPlayed(cardController);
        MoveCardsImplicateInReversal(cardController);
        cardController.ApplyReversalEffect();
    }

    private void SayTheReversalCardIsPlayed(CardController cardController)
    {
        var indexType = cardController.GetIndexForType("Reversal");
        var reversalString = cardController.GetStringPlayedInfo(indexType);
        gameStructureInfo.View.SayThatPlayerReversedTheCard(
            gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), reversalString);
    }

    private void MoveCardsImplicateInReversal(CardController cardController)
    {
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(gameStructureInfo.GetCurrentPlayer(),
            gameStructureInfo.CardBeingPlayed);
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetOpponentPlayer(),
            cardController);
    }
}