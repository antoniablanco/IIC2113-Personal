using RawDeal.SuperStarClass;
using RawDealView;
using RawDealView.Options;

namespace RawDeal;
using System.Text.Json ;


public class GameLogic
{   
    private bool _isTheGameStillPlaying = true;
    private bool _IsTheTurnBeingPlayed = true;
    
    private VisualizeCards VisualizeCards = new VisualizeCards();
    public GameStructureInfo gameStructureInfo = new GameStructureInfo();
    
    public List<CardJson> DeserializeJsonCards()
    {
        string myJson = File.ReadAllText (Path.Combine("data","cards.json")) ;
        var cartas = JsonSerializer.Deserialize<List<CardJson>>(myJson) ;
        return cartas;
    }
    
    public List<SuperStarJSON> DeserializeJsonSuperStar()
    {
        string myJson = File.ReadAllText (Path.Combine("data","superstar.json")) ;
        var superstars = JsonSerializer.Deserialize<List<SuperStarJSON>>(myJson) ;
        return superstars;
    }
    
    public SuperStar? CreateSuperStar(string deck, List<SuperStarJSON> totalSuperStars) 
    {
        string firstLineDeck = GetSuperStarName(deck);
        Dictionary<SuperStarJSON, Type> superStarTypes = GetSuperStarTypesDictionary(totalSuperStars);
        
        foreach (var superstar in from super in superStarTypes where firstLineDeck.Contains(super.Key.Name) 
                 select (SuperStar)Activator.CreateInstance(super.Value,super.Key.Name, super.Key.Logo, super.Key.HandSize, super.Key.SuperstarValue, super.Key.SuperstarAbility, gameStructureInfo.view))
        {
            return superstar;
        }

        return null;
    }

    private string GetSuperStarName(string deck)
    {
        string pathDeck = Path.Combine($"{deck}");
        string[] lines = File.ReadAllLines(pathDeck);
        return lines[0];
    }

    private Dictionary<SuperStarJSON, Type> GetSuperStarTypesDictionary(List<SuperStarJSON> totalSuperStars)
    {
        Dictionary<SuperStarJSON, Type> superStarTypes = new Dictionary<SuperStarJSON, Type>();
        foreach (var super in totalSuperStars)
        {
            if (super.Name == "STONE COLD STEVE AUSTIN")
                superStarTypes.Add(super, typeof(StoneCold));
            else if (super.Name == "THE UNDERTAKER")
                superStarTypes.Add(super, typeof(Undertaker));
            else if (super.Name == "MANKIND")
                superStarTypes.Add(super, typeof(Mankind));
            else if (super.Name == "KANE")
                superStarTypes.Add(super, typeof(Kane));
            else if (super.Name == "HHH")
                superStarTypes.Add(super, typeof(HHH));
            else if (super.Name == "THE ROCK")
                superStarTypes.Add(super, typeof(TheRock));
            else if (super.Name == "CHRIS JERICHO")
                superStarTypes.Add(super, typeof(Jericho));
        }

        return superStarTypes;
    }

    public void SettingTurnStartInformation()
    {
        gameStructureInfo.currentPlayer.DrawCard();
        SetVariableTrueBecauseTurnStarted();
        gameStructureInfo.view.SayThatATurnBegins(gameStructureInfo.currentPlayer.NameOfSuperStar());
        TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed();
    }

    private void SetVariableTrueBecauseTurnStarted()
    {   
        _IsTheTurnBeingPlayed = true;
        gameStructureInfo.currentPlayer.TheTurnHasJustStartTheSuperStarHasNotUsedHisSuperAbility();
    }

    private void TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed()
    {
        PlayerController currentPlayer = gameStructureInfo.currentPlayer;
        PlayerController oppositePlayer = gameStructureInfo.opponentPlayer;
        gameStructureInfo.currentPlayer.UsingAutomaticSuperAbility(currentPlayer, oppositePlayer);
    }
    

    public void ThePlayerDrawTheirInitialsHands()
    {
        gameStructureInfo.playerOne.DrawInitialHandCards();
        gameStructureInfo.playerTwo.DrawInitialHandCards();
    }
    
    public string GetWinnerSuperstarName() //ELIMINAR PRINT
    {   
        Console.WriteLine("El ganador es: ");
        return gameStructureInfo.winnerPlayer.NameOfSuperStar();
    }
    
    public void DisplayPlayerInformation() 
    {
        PlayerInfo playerUno = new PlayerInfo(gameStructureInfo.playerOne.NameOfSuperStar(), gameStructureInfo.playerOne.FortitudRating(), gameStructureInfo.playerOne.NumberOfCardsInTheHand(), gameStructureInfo.playerOne.NumberOfCardsInTheArsenal());
        PlayerInfo playerDos = new PlayerInfo(gameStructureInfo.playerTwo.NameOfSuperStar(), gameStructureInfo.playerTwo.FortitudRating(), gameStructureInfo.playerTwo.NumberOfCardsInTheHand(), gameStructureInfo.playerTwo.NumberOfCardsInTheArsenal());
        
        List<PlayerInfo> playersListToPrint =  new List<PlayerInfo> { playerUno, playerDos };
        
        int numCurrentPlayer = gameStructureInfo.currentPlayer == gameStructureInfo.playerOne ? 0 : 1;
        int numOppositePlayer = gameStructureInfo.opponentPlayer == gameStructureInfo.playerOne ? 0 : 1;
        
        gameStructureInfo.view.ShowGameInfo(playersListToPrint[numCurrentPlayer], playersListToPrint[numOppositePlayer]);
    }

    public void CreatePlayerInitialOrder()
    {
        gameStructureInfo.currentPlayer = (gameStructureInfo.playerOne.GetSuperStarValue() < gameStructureInfo.playerTwo.GetSuperStarValue()) ? gameStructureInfo.playerTwo : gameStructureInfo.playerOne;
        gameStructureInfo.opponentPlayer = (gameStructureInfo.playerOne.GetSuperStarValue() < gameStructureInfo.playerTwo.GetSuperStarValue()) ? gameStructureInfo.playerOne : gameStructureInfo.playerTwo;
    }

    public bool ShouldWeContinueTheGame()
    {   
        return (gameStructureInfo.playerOne.AreThereCardsLeftInTheArsenal() && gameStructureInfo.playerTwo.AreThereCardsLeftInTheArsenal() && _isTheGameStillPlaying);
    }

    public bool TheTurnIsBeingPlayed()
    {
        return _IsTheTurnBeingPlayed;
    }
    
    public bool PlayerCanUseSuperStarAbility() 
    {
        return gameStructureInfo.currentPlayer.TheirSuperStarCanUseSuperAbility(gameStructureInfo.currentPlayer);
    }

    public void ActionUseSuperAbility()
    {
        gameStructureInfo.currentPlayer.UsingElectiveSuperAbility(gameStructureInfo.currentPlayer, gameStructureInfo.opponentPlayer);
    }
    
    public void ActionPlayCard() 
    {
        int selectedCard = gameStructureInfo.view.AskUserToSelectAPlay(GetPossibleCardsToPlayString());
        if ( IsValidIndexOfCard(selectedCard))
        {   
            CardController playedCardController = gameStructureInfo.currentPlayer.CardsAvailableToPlay()[selectedCard];
            PrintActionPlayCard(playedCardController);
            AddCardPlayedToRingArea(playedCardController);
        }
    }

    private bool IsValidIndexOfCard(int selectedCard)
    {
        return selectedCard != -1;
    }

    private List<string> GetPossibleCardsToPlayString()
    {
        List<CardController> possibleCardsToPlay = gameStructureInfo.currentPlayer.CardsAvailableToPlay();
        List<string> cardsStrings = VisualizeCards.CreateStringPlayedCardList(possibleCardsToPlay);
        return cardsStrings;
    }

    private void PrintActionPlayCard(CardController playedCardController)
    {   
        SayThatTheyAreGoingToPlayACard(playedCardController);
        gameStructureInfo.view.SayThatPlayerSuccessfullyPlayedACard();
        int totalDamage = GetDamageProduced(playedCardController);
        SayThatTheyAreGoingToReceiveDamage(totalDamage);
        CauseDamageActionPlayCard(totalDamage);
    }

    private void SayThatTheyAreGoingToPlayACard(CardController playedCardController)
    {
        string playedCardString = VisualizeCards.GetStringPlayedInfo(playedCardController);
        string nameSuperStar = gameStructureInfo.currentPlayer.NameOfSuperStar();
        gameStructureInfo.view.SayThatPlayerIsTryingToPlayThisCard(nameSuperStar, playedCardString);
    }

    private int GetDamageProduced(CardController playedCardController)
    {
        int totalDamage = playedCardController.GetDamageProducedByTheCard();
        if (gameStructureInfo.opponentPlayer.IsTheSuperStarMankind())
            totalDamage -= 1;
        return  totalDamage;
    }

    private void SayThatTheyAreGoingToReceiveDamage(int totalDamage)
    {   
        string opposingSuperStarName = gameStructureInfo.opponentPlayer.NameOfSuperStar();
        gameStructureInfo.view.SayThatSuperstarWillTakeSomeDamage(opposingSuperStarName, totalDamage);
    }

    private void CauseDamageActionPlayCard(int totalDamage) 
    {
        for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
        {   
            if (CheckCanReceiveDamage())
                ShowOneFaceDownCard(currentDamage+1, totalDamage);
            else
                SetVariablesAfterWinning();
        }
    }

    private bool CheckCanReceiveDamage()
    {
        return gameStructureInfo.opponentPlayer.AreThereCardsLeftInTheArsenal();
    }

    private void ShowOneFaceDownCard(int currentDamage, int totalDamage)
    {
        CardController flippedCardController = gameStructureInfo.opponentPlayer.TranferUnselectedCardFromArsenalToRingSide();
        string flippedCardString = VisualizeCards.GetStringCardInfo(flippedCardController);
        gameStructureInfo.view.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
    }

    private void SetVariablesAfterWinning()
    {
        gameStructureInfo.winnerPlayer = gameStructureInfo.currentPlayer;
        _isTheGameStillPlaying = false;
        DeclareEndOfTurn();
    }

    private void DeclareEndOfTurn()
    {
        _IsTheTurnBeingPlayed = false;
    }

    private void AddCardPlayedToRingArea(CardController playedCardController)
    {
        gameStructureInfo.currentPlayer.TransferChoosinCardFromHandToRingArea(playedCardController);
    }
    
    public void SelectCardsToView()
    {
        var setCardsToView = gameStructureInfo.view.AskUserWhatSetOfCardsHeWantsToSee();
        switch (setCardsToView)
        {
            case CardSet.Hand:
                ActionSeeTotalCards(gameStructureInfo.currentPlayer.StringCardsHand());
                break;
            case CardSet.RingArea:
                ActionSeeTotalCards(gameStructureInfo.currentPlayer.StringCardsRingArea());
                break;
            case CardSet.RingsidePile:
                ActionSeeTotalCards(gameStructureInfo.currentPlayer.StringCardsRingSide());
                break;
            case CardSet.OpponentsRingArea:
                ActionSeeTotalCards(gameStructureInfo.opponentPlayer.StringCardsRingArea());
                break;
            case CardSet.OpponentsRingsidePile:
                ActionSeeTotalCards(gameStructureInfo.opponentPlayer.StringCardsRingSide());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ActionSeeTotalCards(List<String> stringCardSet)
    {   
        gameStructureInfo.view.ShowCards(stringCardSet);
    }

    public void UpdateVariablesAtEndOfTurn()
    {   
        DeclareEndOfTurn();
        if (!CheckIfPlayersHasCardsInArsenalToContinuePlaying())
        {
            SetVariablesAfterLosing();
        }
        UpdateNumberOfPlayers();
    }
    
    private void UpdateNumberOfPlayers()
    {
        gameStructureInfo.currentPlayer = (gameStructureInfo.currentPlayer == gameStructureInfo.playerOne) ? gameStructureInfo.playerTwo : gameStructureInfo.playerOne;
        gameStructureInfo.opponentPlayer = (gameStructureInfo.opponentPlayer == gameStructureInfo.playerOne) ? gameStructureInfo.playerTwo : gameStructureInfo.playerOne;
    }
    
    private bool CheckIfPlayersHasCardsInArsenalToContinuePlaying()
    {   
        return gameStructureInfo.currentPlayer.HasCardsInArsenal() && gameStructureInfo.opponentPlayer.HasCardsInArsenal();
    }
    
    public void SetVariablesAfterLosing() 
    {   
        gameStructureInfo.winnerPlayer = (gameStructureInfo.currentPlayer.HasCardsInArsenal()) ? gameStructureInfo.playerTwo : gameStructureInfo.playerOne;
        _isTheGameStillPlaying = false;
        DeclareEndOfTurn();
    }

    public void SetVariablesAfterGaveUp()
    {
        gameStructureInfo.winnerPlayer = (gameStructureInfo.currentPlayer == gameStructureInfo.playerOne) ? gameStructureInfo.playerTwo : gameStructureInfo.playerOne;
        _isTheGameStillPlaying = false;
        DeclareEndOfTurn();
    }
    
}
