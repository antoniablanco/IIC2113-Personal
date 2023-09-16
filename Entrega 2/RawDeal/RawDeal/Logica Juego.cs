using RawDeal.SuperStarClases;
using RawDealView;
using RawDealView.Options;
using RawDealView.Utils;
using RawDealView.Views;

namespace RawDeal;
using System.Text.Json ;


public class Logica_Juego
{   
    private bool _isTheGameStillPlaying = true;
    private bool _IsTheTurnBeingPlayed = true;
    
    public View view;
    public VisualizeCards VisualizeCards = new VisualizeCards();
    
    public Player playerOne { get; set; }
    public Player playerTwo { get; set; }
    public List<Player> listaPlayers;
    
    public int numJugadorActual = 0;
    public int numJugadorDos = 1;
    public int numJugadorGanador = 1;
    public int NumJugadorInicio = 0;
    
    
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
    
    public List<Card> CrearCartas(string mazoString, List<CardJson> totalCartas) // Aplicar Clean Code
    {
        List<Card> cartas = new List<Card>();
        string pathDeck = Path.Combine($"{mazoString}");
        string[] lines = File.ReadAllLines(pathDeck);

        foreach (var line in lines)
        {
            foreach (var carta in totalCartas)
            {  
                if (line.Trim() == carta.Title)
                {
                    Card nuevaCard = new Card(carta.Title, carta.Types,carta.Subtypes,carta.Fortitude, carta.Damage, carta.StunValue, carta.CardEffect );
                    cartas.Add(nuevaCard);
                }
            }
        }
        return cartas;
    }
    
    public SuperStar? CrearSuperStar(string deck, List<SuperStarJSON> totalSuperStars) // Aplicar Clean Code
    {
        string firstLineDeck = ObtenerNombreSuperStar(deck);
        Dictionary<SuperStarJSON, Type> superStarTypes = ObtenerDiccionarioTiposSuperStars(totalSuperStars);
        
        foreach (var super in superStarTypes)
        {
            if (firstLineDeck.Contains(super.Key.Name))
            {   
                SuperStar superstar = (SuperStar)Activator.CreateInstance(super.Value,super.Key.Name, super.Key.Logo, super.Key.HandSize, super.Key.SuperstarValue, super.Key.SuperstarAbility, view);
                return superstar;
            }
        }
        SuperStar superstarNull = new HHH("Null", "Null", 0, 0,"Null", view);
        return superstarNull;
    }
    
    public string ObtenerNombreSuperStar(string deck)
    {
        string pathDeck = Path.Combine($"{deck}");
        string[] lines = File.ReadAllLines(pathDeck);
        return lines[0];
    }
    
    public Dictionary<SuperStarJSON, Type> ObtenerDiccionarioTiposSuperStars(List<SuperStarJSON> totalSuperStars)
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
        listaPlayers[numJugadorActual].DrawCard();
        SetearVariableTruePorqueInicioTurno();
        view.SayThatATurnBegins(listaPlayers[numJugadorActual].superestar.Name);
        SeUtilizaLaSuperHabilidadQueEsAlInicioDelTurno();
    }
    
    public void JugadorInicioJuego()
    {
        NumJugadorInicio = (playerOne.superestar.SuperstarValue < playerTwo.superestar.SuperstarValue) ? 1 : 0;
    }

    public void ThePlayerDrawTheirInitialsHands()
    {
        playerOne.DrawInitialHandCards();
        playerTwo.DrawInitialHandCards();
    }
    
    public string GetWinnerSuperstarName()
    {
        return listaPlayers[numJugadorGanador].NameOfSuperStar();
    }
    
    public void DisplayPlayerInformation() 
    {
        PlayerInfo playerUno = new PlayerInfo(playerOne.superestar.Name, playerOne.FortitudRating(),playerOne.cardsHand.Count, playerOne.cardsArsenal.Count);
        PlayerInfo playerDos = new PlayerInfo(playerTwo.superestar.Name, playerTwo.FortitudRating(), playerTwo.cardsHand.Count, playerTwo.cardsArsenal.Count);
        
        List<PlayerInfo> listaPlayersParaImprimir = (NumJugadorInicio == 0) ? new List<PlayerInfo> { playerUno, playerDos } : new List<PlayerInfo> { playerDos, playerUno };
        
        view.ShowGameInfo(listaPlayersParaImprimir[numJugadorActual], listaPlayersParaImprimir[numJugadorDos]);
    }

    public void CrearListaPlayers()
    {
        listaPlayers = (NumJugadorInicio == 0) ? new List<Player> { playerOne, playerTwo } : new List<Player> { playerTwo, playerOne };
    }

    public bool ShouldWeContinueTheGame()
    {   
        return (playerOne.cardsArsenal.Count() > 0 && playerTwo.cardsArsenal.Count() > 0 && _isTheGameStillPlaying);
    }

    public bool TheTurnIsBeingPlayed()
    {
        return _IsTheTurnBeingPlayed;
    }
    
    public bool PlayerCanUseSuperStarAbility() 
    {
        return listaPlayers[numJugadorActual].TheirSuperStarCanUseSuperAbility(listaPlayers[numJugadorActual]);
    }

    public void ActionUseSuperAbility()
    {
        listaPlayers[numJugadorActual].UsingElectiveSuperAbility(listaPlayers[numJugadorActual], listaPlayers[numJugadorDos]);
    }
    
    public void SeUtilizaLaSuperHabilidadQueEsAlInicioDelTurno()
    {
        listaPlayers[numJugadorActual]
            .UsingAutomaticSuperAbility(listaPlayers[numJugadorActual], listaPlayers[numJugadorDos]);
    }
    
    public void ActionPlayCard() 
    {
        int cartaSeleccionada = view.AskUserToSelectAPlay(ObtenerStringCartasPosiblesJugar());
        if (cartaSeleccionada != -1)
        {   
            Card cardJugada = listaPlayers[numJugadorActual].CardsAvailableToPlay()[cartaSeleccionada];
            ImpresionesAccionJugarCarta(cardJugada);
            AgregarCartaJugadaRingArea(cardJugada);
        }
    }

    public void AgregarCartaJugadaRingArea(Card cardJugada)
    {
        List<Card> cartasHand = listaPlayers[numJugadorActual].cardsHand;
        List<Card> cartasRingArea = listaPlayers[numJugadorActual].cardsRingArea;
        listaPlayers[numJugadorActual].CardTransferChoosingWhichOneToChange(cardJugada, cartasHand, cartasRingArea);
    }
    
    public void ImpresionesAccionJugarCarta(Card cardJugada)
    {   
        DecirQueVaAJugarCarta(cardJugada);
        view.SayThatPlayerSuccessfullyPlayedACard();
        int danoTotal = ObtenerDanoProducido(cardJugada);
        DecirQueVaARecibirDaño(cardJugada, danoTotal);
        ProbocarDanoAccionJugarCarta(cardJugada, danoTotal);
    }

    public int ObtenerDanoProducido(Card cardJugada)
    {
        int danoTotal = int.Parse(cardJugada.Damage);
        if (listaPlayers[numJugadorDos].superestar.Name == "MANKIND")
            danoTotal -= 1;
        return danoTotal;
    }

    public List<string> ObtenerStringCartasPosiblesJugar()
    {
        List<Card> cartasPosiblesJugar = listaPlayers[numJugadorActual].CardsAvailableToPlay();
        List<string> stringDeCartas = VisualizeCards.CreateStringPlayedCardList(cartasPosiblesJugar);
        return stringDeCartas;
    }
    
    public void SelectCardsToView()
    {
        var setCartasParaVer = view.AskUserWhatSetOfCardsHeWantsToSee();
        switch (setCartasParaVer)
        {
            case CardSet.Hand:
                AccionVercartasTotales(listaPlayers[numJugadorActual].cardsHand);
                break;
            case CardSet.RingArea:
                AccionVercartasTotales(listaPlayers[numJugadorActual].cardsRingArea);
                break;
            case CardSet.RingsidePile:
                AccionVercartasTotales(listaPlayers[numJugadorActual].cardsRingSide);
                break;
            case CardSet.OpponentsRingArea:
                AccionVercartasTotales(listaPlayers[numJugadorDos].cardsRingArea);
                break;
            case CardSet.OpponentsRingsidePile:
                AccionVercartasTotales(listaPlayers[numJugadorDos].cardsRingSide);
                break;
        }
    }

    public void AccionVercartasTotales(List<Card> conjuntoCartas)
    {   
        List<String> stringCartas = VisualizeCards.CreateStringCardList(conjuntoCartas);
        view.ShowCards(stringCartas);
    }

    public void ActualizacionNumJugadores()
    {
        numJugadorActual = (numJugadorActual == 0) ? 1 : 0;
        numJugadorDos = (numJugadorDos == 0) ? 1 : 0;
    }

    public void DecirQueVaAJugarCarta(Card cardJugada)
    {
        string cartaJugadaString = VisualizeCards.GetStringPlayedInfo(cardJugada);
        string nombreSuperStar = listaPlayers[numJugadorActual].superestar.Name;
        view.SayThatPlayerIsTryingToPlayThisCard(nombreSuperStar, cartaJugadaString);
    }

    public void DecirQueVaARecibirDaño(Card cardJugada, int danoRecibido)
    {   
        string nombreSuperStarContrario = listaPlayers[numJugadorDos].superestar.Name;
        view.SayThatSuperstarWillTakeSomeDamage(nombreSuperStarContrario, danoRecibido);
    }

    public void ProbocarDanoAccionJugarCarta(Card cardJugada, int danoTotal) 
    {
        for (int i = 0; i < danoTotal; i++)
        {   
            if (VerificarPuedeRecibirDano())
                MostrarUnaCartaVolteada(i, danoTotal);
            else
                SetearVariablesTrasGanar();
        }
    }
    
    public void MostrarUnaCartaVolteada(int danoActual, int danoTotal)
    {   
        List<Card> cartasRingSide = listaPlayers[numJugadorDos].cardsRingSide;
        List<Card> cartasArsenal = listaPlayers[numJugadorDos].cardsArsenal;
        Card cardVolteada = listaPlayers[numJugadorDos].TransferOfUnselectedCard(cartasArsenal, cartasRingSide);
        string cartaVolteadaString = VisualizeCards.GetStringCardInfo(cardVolteada);
        view.ShowCardOverturnByTakingDamage(cartaVolteadaString, danoActual+1, danoTotal);
    }
    
    public bool VerificarPuedeRecibirDano()
    {
        return listaPlayers[numJugadorDos].cardsArsenal.Count > 0;
    }
    
    public void DeclararFinTurno()
    {
        _IsTheTurnBeingPlayed = false;
    }

    public void SetearVariableTruePorqueInicioTurno()
    {   
        _IsTheTurnBeingPlayed = true;
        listaPlayers[numJugadorActual].theHabilityHasBeenUsedThisTurn = false;
    }
    
    public void UpdateVariablesAtEndOfTurn()
    {   
        DeclararFinTurno();
        ActualizacionNumJugadores();
        if (!VerificarSiJugadorTieneCartasEnArsenalParaSeguirJugando())
        {
            SetVariablesAfterLosing();
        }
    }

    private bool VerificarSiJugadorTieneCartasEnArsenalParaSeguirJugando()
    {
        return listaPlayers[numJugadorActual].HasCardsInArsenal();
    }
    
    public void SetearVariablesTrasGanar()
    {
        numJugadorGanador = numJugadorActual;
        _isTheGameStillPlaying = false;
        DeclararFinTurno();
    }
    
    public void SetVariablesAfterLosing()
    {
        numJugadorGanador = (numJugadorActual == 0) ? 1 : 0;
        _isTheGameStillPlaying = false;
        DeclararFinTurno();
    }
    
}
