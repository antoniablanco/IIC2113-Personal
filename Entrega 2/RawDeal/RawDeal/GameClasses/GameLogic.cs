using RawDeal.CardClass;
using RawDeal.PlayerClasses;
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
        PlayerInfo playerUno = new PlayerInfo(gameStructureInfo.ControllerPlayerOne.NameOfSuperStar(), gameStructureInfo.ControllerPlayerOne.FortitudRating(), gameStructureInfo.ControllerPlayerOne.NumberOfCardIn("Hand"), gameStructureInfo.ControllerPlayerOne.NumberOfCardIn("Arsenal"));
        PlayerInfo playerDos = new PlayerInfo(gameStructureInfo.ControllerPlayerTwo.NameOfSuperStar(), gameStructureInfo.ControllerPlayerTwo.FortitudRating(), gameStructureInfo.ControllerPlayerTwo.NumberOfCardIn("Hand"), gameStructureInfo.ControllerPlayerTwo.NumberOfCardIn("Arsenal"));
        
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

    private void ActionSeeTotalCards(List<String> stringCardSet)
    {   
        gameStructureInfo.View.ShowCards(stringCardSet);
    }
    
    public bool CheckIfPlayersHasCardsInArsenalToContinuePlaying()
    {   
        return gameStructureInfo.ControllerCurrentPlayer.HasCardsInArsenal() && gameStructureInfo.ControllerOpponentPlayer.HasCardsInArsenal();
    }
    
}
