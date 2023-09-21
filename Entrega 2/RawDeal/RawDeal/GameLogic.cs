using RawDeal.SuperStarClases;
using RawDealView;
using RawDealView.Options;
using RawDealView.Utils;
using RawDealView.Views;

namespace RawDeal;
using System.Text.Json ;


public class GameLogic
{   
    private bool _isTheGameStillPlaying = true;
    private bool _IsTheTurnBeingPlayed = true;
    
    public View view;
    private VisualizeCards VisualizeCards = new VisualizeCards();
    
    public PlayerController playerOne { get; set; }
    public PlayerController playerTwo { get; set; }
    
    private List<PlayerController> playersList;

    private int numCurrentPlayer = 0;
    private int numOppositePlayer = 1;
    private int numWinnerPlayer = 1;
    private int StartingPlayerNumber = 0;
    
    
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
    
    public List<Card> CreateCards(string playerString, List<CardJson> totalCards) 
    {
        string pathDeck = Path.Combine($"{playerString}");
        string[] lines = File.ReadAllLines(pathDeck);

        return (from line in lines from card in totalCards where line.Trim() ==  card.Title 
            select new Card( card.Title,  card.Types,  card.Subtypes,  card.Fortitude,  card.Damage,  card.StunValue,  card.CardEffect)).ToList();
    }
    
    public SuperStar? CreateSuperStar(string deck, List<SuperStarJSON> totalSuperStars) 
    {
        string firstLineDeck = GetSuperStarName(deck);
        Dictionary<SuperStarJSON, Type> superStarTypes = GetSuperStarTypesDictionary(totalSuperStars);
        
        foreach (var superstar in from super in superStarTypes where firstLineDeck.Contains(super.Key.Name) 
                 select (SuperStar)Activator.CreateInstance(super.Value,super.Key.Name, super.Key.Logo, super.Key.HandSize, super.Key.SuperstarValue, super.Key.SuperstarAbility, view))
        {
            return superstar;
        }
        SuperStar superstarNull = new HHH("Null", "Null", 0, 0,"Null", view);
        return superstarNull;
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
        playersList[numCurrentPlayer].DrawCard();
        SetVariableTrueBecauseTurnStarted();
        view.SayThatATurnBegins(playersList[numCurrentPlayer].NameOfSuperStar());
        TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed();
    }

    private void SetVariableTrueBecauseTurnStarted()
    {   
        _IsTheTurnBeingPlayed = true;
        playersList[numCurrentPlayer].TheTurnHasJustStartTheSuperStarHasNotUsedHisSuperAbility();
    }

    private void TheSuperAbilityThatIsAtTheStartOfTheTurnIsUsed()
    {
        PlayerController currentPlayer = playersList[numCurrentPlayer];
        PlayerController oppositePlayer = playersList[numOppositePlayer];
        playersList[numCurrentPlayer].UsingAutomaticSuperAbility(currentPlayer, oppositePlayer);
    }
    
    public void PlayerStartedGame()
    {
        StartingPlayerNumber = (playerOne.GetSuperStarValue() < playerTwo.GetSuperStarValue()) ? 1 : 0;
    }

    public void ThePlayerDrawTheirInitialsHands()
    {
        playerOne.DrawInitialHandCards();
        playerTwo.DrawInitialHandCards();
    }
    
    public string GetWinnerSuperstarName()
    {
        return playersList[numWinnerPlayer].NameOfSuperStar();
    }
    
    public void DisplayPlayerInformation() 
    {
        PlayerInfo playerUno = new PlayerInfo(playerOne.NameOfSuperStar(), playerOne.FortitudRating(), playerOne.NumberOfCardsInTheHand(), playerOne.NumberOfCardsInTheArsenal());
        PlayerInfo playerDos = new PlayerInfo(playerTwo.NameOfSuperStar(), playerTwo.FortitudRating(), playerTwo.NumberOfCardsInTheHand(), playerTwo.NumberOfCardsInTheArsenal());
        
        List<PlayerInfo> playersListToPrint = (StartingPlayerNumber == 0) ? new List<PlayerInfo> { playerUno, playerDos } : new List<PlayerInfo> { playerDos, playerUno };
        
        view.ShowGameInfo(playersListToPrint[numCurrentPlayer], playersListToPrint[numOppositePlayer]);
    }

    public void CreatePlayerList()
    {
        playersList = (StartingPlayerNumber == 0) ? new List<PlayerController> { playerOne, playerTwo } : new List<PlayerController> { playerTwo, playerOne };
    }

    public bool ShouldWeContinueTheGame()
    {   
        return (playerOne.AreThereCardsLeftInTheArsenal() && playerTwo.AreThereCardsLeftInTheArsenal() && _isTheGameStillPlaying);
    }

    public bool TheTurnIsBeingPlayed()
    {
        return _IsTheTurnBeingPlayed;
    }
    
    public bool PlayerCanUseSuperStarAbility() 
    {
        return playersList[numCurrentPlayer].TheirSuperStarCanUseSuperAbility(playersList[numCurrentPlayer]);
    }

    public void ActionUseSuperAbility()
    {
        playersList[numCurrentPlayer].UsingElectiveSuperAbility(playersList[numCurrentPlayer], playersList[numOppositePlayer]);
    }
    
    public void ActionPlayCard() 
    {
        int selectedCard = view.AskUserToSelectAPlay(GetPossibleCardsToPlayString());
        if ( IsValidIndexOfCard(selectedCard))
        {   
            Card playedCard = playersList[numCurrentPlayer].CardsAvailableToPlay()[selectedCard];
            PrintActionPlayCard(playedCard);
            AddCardPlayedToRingArea(playedCard);
        }
    }

    private bool IsValidIndexOfCard(int selectedCard)
    {
        return selectedCard != -1;
    }

    private List<string> GetPossibleCardsToPlayString()
    {
        List<Card> possibleCardsToPlay = playersList[numCurrentPlayer].CardsAvailableToPlay();
        List<string> cardsStrings = VisualizeCards.CreateStringPlayedCardList(possibleCardsToPlay);
        return cardsStrings;
    }

    private void PrintActionPlayCard(Card playedCard)
    {   
        SayThatTheyAreGoingToPlayACard(playedCard);
        view.SayThatPlayerSuccessfullyPlayedACard();
        int totalDamage = GetDamageProduced(playedCard);
        SayThatTheyAreGoingToReceiveDamage(totalDamage);
        CauseDamageActionPlayCard(totalDamage);
    }

    private void SayThatTheyAreGoingToPlayACard(Card playedCard)
    {
        string playedCardString = VisualizeCards.GetStringPlayedInfo(playedCard);
        string nameSuperStar = playersList[numCurrentPlayer].NameOfSuperStar();
        view.SayThatPlayerIsTryingToPlayThisCard(nameSuperStar, playedCardString);
    }

    private int GetDamageProduced(Card playedCard)
    {
        int totalDamage = int.Parse(playedCard.Damage);
        if (playersList[numOppositePlayer].IsTheSuperStarMankind())
            totalDamage -= 1;
        return  totalDamage;
    }

    private void SayThatTheyAreGoingToReceiveDamage(int totalDamage)
    {   
        string opposingSuperStarName = playersList[numOppositePlayer].NameOfSuperStar();
        view.SayThatSuperstarWillTakeSomeDamage(opposingSuperStarName, totalDamage);
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
        return playersList[numOppositePlayer].AreThereCardsLeftInTheArsenal();
    }

    private void ShowOneFaceDownCard(int currentDamage, int totalDamage)
    {
        Card flippedCard = playersList[numOppositePlayer].TranferUnselectedCardFromArsenalToRingSide();
        string flippedCardString = VisualizeCards.GetStringCardInfo(flippedCard);
        view.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
    }

    private void SetVariablesAfterWinning()
    {
        numWinnerPlayer = numCurrentPlayer;
        _isTheGameStillPlaying = false;
        DeclareEndOfTurn();
    }

    private void DeclareEndOfTurn()
    {
        _IsTheTurnBeingPlayed = false;
    }

    private void AddCardPlayedToRingArea(Card playedCard)
    {
        playersList[numCurrentPlayer].TransferChoosinCardFromHandToRingArea(playedCard);
    }
    
    public void SelectCardsToView()
    {
        var setCardsToView = view.AskUserWhatSetOfCardsHeWantsToSee();
        switch (setCardsToView)
        {
            case CardSet.Hand:
                ActionSeeTotalCards(playersList[numCurrentPlayer].StringCardsHand());
                break;
            case CardSet.RingArea:
                ActionSeeTotalCards(playersList[numCurrentPlayer].StringCardsRingArea());
                break;
            case CardSet.RingsidePile:
                ActionSeeTotalCards(playersList[numCurrentPlayer].StringCardsRingSide());
                break;
            case CardSet.OpponentsRingArea:
                ActionSeeTotalCards(playersList[numOppositePlayer].StringCardsRingArea());
                break;
            case CardSet.OpponentsRingsidePile:
                ActionSeeTotalCards(playersList[numOppositePlayer].StringCardsRingSide());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ActionSeeTotalCards(List<String> stringCardSet)
    {   
        view.ShowCards(stringCardSet);
    }

    public void UpdateVariablesAtEndOfTurn()
    {   
        DeclareEndOfTurn();
        UpdateNumberOfPlayers();
        if (!CheckIfPlayerHasCardsInArsenalToContinuePlaying())
        {
            SetVariablesAfterLosing();
        }
    }
    
    private void UpdateNumberOfPlayers()
    {
        numCurrentPlayer = (numCurrentPlayer == 0) ? 1 : 0;
        numOppositePlayer = (numOppositePlayer == 0) ? 1 : 0;
    }
    
    private bool CheckIfPlayerHasCardsInArsenalToContinuePlaying()
    {
        return playersList[numCurrentPlayer].HasCardsInArsenal();
    }
    
    public void SetVariablesAfterLosing()
    {
        numWinnerPlayer = (numCurrentPlayer == 0) ? 1 : 0;
        _isTheGameStillPlaying = false;
        DeclareEndOfTurn();
    }
    
}
