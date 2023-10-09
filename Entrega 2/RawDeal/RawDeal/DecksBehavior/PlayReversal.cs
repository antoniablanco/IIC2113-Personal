using RawDeal.CardClass;

namespace RawDeal.DecksBehavior;

public class PlayReversal
{   
    public GameStructureInfo gameStructureInfo = new GameStructureInfo();

    public bool IsUserUsingReversalCard()
    {   
        List<CardController> possibleReversals = gameStructureInfo.ControllerOpponentPlayer.CardsAvailableToReversal();
        if (possibleReversals.Count() > 0)
        {
            int indexReversalCard = UserSelectReversalCard(possibleReversals);
            if (indexReversalCard != -1)
            {
                PlayingReversalCard(indexReversalCard);
                return true;
            }
        }
        return false;
    }

    public int UserSelectReversalCard(List<CardController> possibleReversals)
    {
        List<String> possibleReversalsString =
            gameStructureInfo.VisualizeCards.CreateStringPlayedCardList(possibleReversals);
        int indexReversalCard = gameStructureInfo.view.AskUserToSelectAReversal(
            gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), possibleReversalsString);
        return indexReversalCard;
    }
    
    public void PlayingReversalCard(int indexReversalCard)
    {
        if (indexReversalCard != -1)
        {
            Console.WriteLine("You are playing a reversal card");
        }
    }
}