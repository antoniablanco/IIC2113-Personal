using RawDealView;
using RawDealView.Options;

namespace RawDeal.GameClasses;

public class GameLogic
{
    
    private GameStructureInfo gameStructureInfo;

    public GameLogic(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }
    public void ThePlayerDrawTheirInitialsHands()
    {
        gameStructureInfo.ControllerPlayerOne.DrawInitialHandCards();
        gameStructureInfo.ControllerPlayerTwo.DrawInitialHandCards();
    }
    
    public string GetWinnerSuperstarName() 
    {   
        return gameStructureInfo.WinnerPlayer.NameOfSuperStar();
    }
    
    public void DisplayPlayerInformation() 
    {   
        PlayerInfo playerUno = new PlayerInfo(gameStructureInfo.ControllerPlayerOne.NameOfSuperStar(), gameStructureInfo.ControllerPlayerOne.FortitudRating(), gameStructureInfo.ControllerPlayerOne.NumberOfCardsInTheHand(), gameStructureInfo.ControllerPlayerOne.NumberOfCardsInTheArsenal());
        PlayerInfo playerDos = new PlayerInfo(gameStructureInfo.ControllerPlayerTwo.NameOfSuperStar(), gameStructureInfo.ControllerPlayerTwo.FortitudRating(), gameStructureInfo.ControllerPlayerTwo.NumberOfCardsInTheHand(), gameStructureInfo.ControllerPlayerTwo.NumberOfCardsInTheArsenal());
        
        List<PlayerInfo> playersListToPrint =  new List<PlayerInfo> { playerUno, playerDos };
        
        int numCurrentPlayer = gameStructureInfo.ControllerCurrentPlayer == gameStructureInfo.ControllerPlayerOne ? 0 : 1;
        int numOppositePlayer = gameStructureInfo.ControllerOpponentPlayer == gameStructureInfo.ControllerPlayerOne ? 0 : 1;

        gameStructureInfo.View.ShowGameInfo(playersListToPrint[numCurrentPlayer], playersListToPrint[numOppositePlayer]);
    }
    
    public void SelectCardsToView()
    {   
        gameStructureInfo.GetSetGameVariables.AddingOneTurnJockeyingForPosition();
        var setCardsToView = gameStructureInfo.View.AskUserWhatSetOfCardsHeWantsToSee();
        switch (setCardsToView)
        {
            case CardSet.Hand:
                ActionSeeTotalCards(gameStructureInfo.ControllerCurrentPlayer.StringCardsHand());
                break;
            case CardSet.RingArea:
                ActionSeeTotalCards(gameStructureInfo.ControllerCurrentPlayer.StringCardsRingArea());
                break;
            case CardSet.RingsidePile:
                ActionSeeTotalCards(gameStructureInfo.ControllerCurrentPlayer.StringCardsRingSide());
                break;
            case CardSet.OpponentsRingArea:
                ActionSeeTotalCards(gameStructureInfo.ControllerOpponentPlayer.StringCardsRingArea());
                break;
            case CardSet.OpponentsRingsidePile:
                ActionSeeTotalCards(gameStructureInfo.ControllerOpponentPlayer.StringCardsRingSide());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ActionSeeTotalCards(List<String> stringCardSet)
    {   
        gameStructureInfo.View.ShowCards(stringCardSet);
    }
    
    public bool CheckIfPlayersHasCardsInArsenalToContinuePlaying()
    {   
        return gameStructureInfo.ControllerCurrentPlayer.HasCardsInArsenal() && gameStructureInfo.ControllerOpponentPlayer.HasCardsInArsenal();
    }
    
}
