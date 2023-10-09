using RawDeal.CardClass;

namespace RawDeal.DecksBehavior;

public class PlayReversal
{   
    public GameStructureInfo gameStructureInfo = new GameStructureInfo();
    
    public List<CardController> GetReversalCards()
    {   
        List<CardController> possibleReversals = gameStructureInfo.ControllerOpponentPlayer.CardsAvailableToReversal();
        return possibleReversals;
    }

    public void PlayingReversalCard(List<CardController> possibleReversals)
    {
        Console.WriteLine("Se tiene reversals para jugar");
    }
}