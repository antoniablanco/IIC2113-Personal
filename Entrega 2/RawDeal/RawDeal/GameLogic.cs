using RawDeal.CardClass;
using RawDeal.PlayerClass;
using RawDeal.SuperStarClass;
using RawDealView;
using RawDealView.Options;

namespace RawDeal;
using System.Text.Json ;


public class GameLogic
{
    
    public GameStructureInfo GameStructureInfo;
    public GetSetGameVariables GetSetGameVariables;
    
    public void SettingTurnStartInformation()
    {
        GameStructureInfo.ControllerCurrentPlayer.DrawCard();
        GetSetGameVariables.SetVariableTrueBecauseTurnStarted();
        GameStructureInfo.view.SayThatATurnBegins(GameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar());
        TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed();
        GameStructureInfo.ControllerCurrentPlayer.BlockinSuperAbilityBecauseIsJustAtTheStartOfTheTurn(GameStructureInfo);
    }
    
    private void TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed()
    {
        GameStructureInfo.ControllerCurrentPlayer.UsingAutomaticSuperAbility(GameStructureInfo);
    }
    
    public void ThePlayerDrawTheirInitialsHands()
    {
        GameStructureInfo.ControllerPlayerOne.DrawInitialHandCards();
        GameStructureInfo.ControllerPlayerTwo.DrawInitialHandCards();
    }
    
    public string GetWinnerSuperstarName() 
    {   
        return GameStructureInfo.winnerPlayer.NameOfSuperStar();
    }
    
    public void DisplayPlayerInformation() 
    {   
        PlayerInfo playerUno = new PlayerInfo(GameStructureInfo.ControllerPlayerOne.NameOfSuperStar(), GameStructureInfo.ControllerPlayerOne.FortitudRating(), GameStructureInfo.ControllerPlayerOne.NumberOfCardsInTheHand(), GameStructureInfo.ControllerPlayerOne.NumberOfCardsInTheArsenal());
        PlayerInfo playerDos = new PlayerInfo(GameStructureInfo.ControllerPlayerTwo.NameOfSuperStar(), GameStructureInfo.ControllerPlayerTwo.FortitudRating(), GameStructureInfo.ControllerPlayerTwo.NumberOfCardsInTheHand(), GameStructureInfo.ControllerPlayerTwo.NumberOfCardsInTheArsenal());
        
        List<PlayerInfo> playersListToPrint =  new List<PlayerInfo> { playerUno, playerDos };
        
        int numCurrentPlayer = GameStructureInfo.ControllerCurrentPlayer == GameStructureInfo.ControllerPlayerOne ? 0 : 1;
        int numOppositePlayer = GameStructureInfo.ControllerOpponentPlayer == GameStructureInfo.ControllerPlayerOne ? 0 : 1;

        GameStructureInfo.view.ShowGameInfo(playersListToPrint[numCurrentPlayer], playersListToPrint[numOppositePlayer]);
    }
    
    public bool PlayerCanUseSuperStarAbility() 
    {
        return GameStructureInfo.ControllerCurrentPlayer.TheirSuperStarCanUseSuperAbility(GameStructureInfo.ControllerCurrentPlayer);
    }

    public void ActionUseSuperAbility()
    {   
        GameStructureInfo.ContadorTurnosJokeyingForPosition += 1;
        GameStructureInfo.ControllerCurrentPlayer.UsingElectiveSuperAbility(GameStructureInfo);
    }
    
    public void SelectCardsToView()
    {   
        GameStructureInfo.ContadorTurnosJokeyingForPosition += 1;
        var setCardsToView = GameStructureInfo.view.AskUserWhatSetOfCardsHeWantsToSee();
        switch (setCardsToView)
        {
            case CardSet.Hand:
                ActionSeeTotalCards(GameStructureInfo.ControllerCurrentPlayer.StringCardsHand());
                break;
            case CardSet.RingArea:
                ActionSeeTotalCards(GameStructureInfo.ControllerCurrentPlayer.StringCardsRingArea());
                break;
            case CardSet.RingsidePile:
                ActionSeeTotalCards(GameStructureInfo.ControllerCurrentPlayer.StringCardsRingSide());
                break;
            case CardSet.OpponentsRingArea:
                ActionSeeTotalCards(GameStructureInfo.ControllerOpponentPlayer.StringCardsRingArea());
                break;
            case CardSet.OpponentsRingsidePile:
                ActionSeeTotalCards(GameStructureInfo.ControllerOpponentPlayer.StringCardsRingSide());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ActionSeeTotalCards(List<String> stringCardSet)
    {   
        GameStructureInfo.view.ShowCards(stringCardSet);
    }
    
    public bool CheckIfPlayersHasCardsInArsenalToContinuePlaying()
    {   
        return GameStructureInfo.ControllerCurrentPlayer.HasCardsInArsenal() && GameStructureInfo.ControllerOpponentPlayer.HasCardsInArsenal();
    }
    
}
