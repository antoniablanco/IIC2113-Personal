using RawDeal.SuperStarClases;
using RawDealView;
using RawDealView.Options;
using RawDealView.Utils;
using RawDealView.Views;

namespace RawDeal;
using System.Text.Json ;


public class Logica_Juego
{   
    private bool _sigueJuego = true;
    private bool _sigueTurno = true;
    public View view;
    public Player PlayerUno { get; set; }
    public Player PlayerDos { get; set; }
    public int numJugadorActual = 0;
    public int numJugadorDos = 1;
    public int numJugadorGanador = 1;
    public int NumJugadorInicio = 0;
    public List<Player> listaPlayers;
    public VisualisarCartas visualisarCartas = new VisualisarCartas();
    
    public List<CartasJson> DescerializarJsonCartas()
    {
        string myJson = File.ReadAllText (Path.Combine("data","cards.json")) ;
        var cartas = JsonSerializer.Deserialize<List<CartasJson>>(myJson) ;
        return cartas;
    }
    
    public List<SuperStarJSON> DescerializarJsonSuperStar()
    {
        string myJson = File.ReadAllText (Path.Combine("data","superstar.json")) ;
        var superstars = JsonSerializer.Deserialize<List<SuperStarJSON>>(myJson) ;
        return superstars;
    }
    
    public List<Carta> CrearCartas(string mazoString, List<CartasJson> totalCartas) // Aplicar Clean Code
    {
        List<Carta> cartas = new List<Carta>();
        string pathDeck = Path.Combine($"{mazoString}");
        string[] lines = File.ReadAllLines(pathDeck);

        foreach (var line in lines)
        {
            foreach (var carta in totalCartas)
            {
                string title = line.Trim();  
                if (title == carta.Title)
                {
                    Carta nuevaCarta = new Carta(carta.Title, carta.Types,carta.Subtypes,carta.Fortitude, carta.Damage, carta.StunValue, carta.CardEffect );
                    cartas.Add(nuevaCarta);
                }
            }
        }
        return cartas;
    }
    
    public SuperStar CrearSuperStar(string deck, List<SuperStarJSON> totalSuperStars) // Aplicar Clean Code
    {
        string pathDeck = Path.Combine($"{deck}");
        string[] lines = File.ReadAllLines(pathDeck);
        string firstLine = lines[0];

        Dictionary<SuperStarJSON, Type> superStarTypes = ObtenerDiccionarioSuperStars(totalSuperStars);
        
        foreach (var super in superStarTypes)
        {
            string superStarName = super.Key.Name;
            Type superStarType = super.Value;

            if (firstLine.Contains(superStarName))
            {   
                SuperStar superstar = (SuperStar)Activator.CreateInstance(superStarType,super.Key.Name, super.Key.Logo, super.Key.HandSize, super.Key.SuperstarValue, super.Key.SuperstarAbility);
                return superstar;
            }
        }
        
        SuperStar superstarNull = new HHH("Null", "Null", 0, 0,"Null");
        return superstarNull;
    }
    
    public Dictionary<SuperStarJSON, Type> ObtenerDiccionarioSuperStars(List<SuperStarJSON> totalSuperStars)
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

    public void JugadorInicioJuego()
    {
        NumJugadorInicio = (PlayerUno.superestar.SuperstarValue < PlayerDos.superestar.SuperstarValue) ? 1 : 0;
    }

    public void MostrarInformacionJugadores() 
    {   
        
        PlayerInfo playerUno = new PlayerInfo(PlayerUno.superestar.Name, PlayerUno.FortitudRating(),PlayerUno.cartasHand.Count, PlayerUno.cartasArsenal.Count);
        PlayerInfo playerDos = new PlayerInfo(PlayerDos.superestar.Name, PlayerDos.FortitudRating(), PlayerDos.cartasHand.Count, PlayerDos.cartasArsenal.Count);
        
        List<PlayerInfo> listaPlayersParaImprimir = (NumJugadorInicio == 0) ? new List<PlayerInfo> { playerUno, playerDos } : new List<PlayerInfo> { playerDos, playerUno };
        
        view.ShowGameInfo(listaPlayersParaImprimir[numJugadorActual], listaPlayersParaImprimir[numJugadorDos]);
        
    }

    public void CrearListaPlayers()
    {
        listaPlayers = (NumJugadorInicio == 0) ? new List<Player> { PlayerUno, PlayerDos } : new List<Player> { PlayerDos, PlayerUno };
    }

    public bool SigueJuego()
    {   
        return (PlayerUno.cartasArsenal.Count() > 0 && PlayerDos.cartasArsenal.Count() > 0 && _sigueJuego);
    }

    public bool SigueTurno()
    {
        return _sigueTurno;
    }
    
    public void AccionSeleccionadaJugador()
    {
        var actividadRealizar = ObtenerProximaJugada();

        switch (actividadRealizar)
        {
            case NextPlay.UseAbility:
                break;
            case NextPlay.ShowCards:
                SeleccionarCartasVer();
                break;
            case NextPlay.PlayCard:
                AccionJugarCarta();
                break;
            case NextPlay.EndTurn:
                ActualizarVariablesPorFinTurno();
                break;
            case NextPlay.GiveUp:
                SetearVariablesTrasPerder();
                break;
        }
    }

    public NextPlay ObtenerProximaJugada()
    {   
        NextPlay actividadRealizar;
        
        if (JugadorPuedeUtilizarHabilidadSuperStar())
        {
            actividadRealizar = view.AskUserWhatToDoWhenUsingHisAbilityIsPossible();
        }
        else
        {
            actividadRealizar = view.AskUserWhatToDoWhenHeCannotUseHisAbility();
        }

        return actividadRealizar;
    }

    public bool JugadorPuedeUtilizarHabilidadSuperStar() // Hay que corregirla para que realmente verifique
    {
        return false;
    }

    public void AccionJugarCarta() 
    {
        int cartaSeleccionada = view.AskUserToSelectAPlay(ObtenerStringCartasPosiblesJugar());
        if (cartaSeleccionada != -1)
        {   
            Carta cartaJugada = listaPlayers[numJugadorActual].CartasPosiblesDeJugar()[cartaSeleccionada];
            ImpresionesAccionJugarCarta(cartaJugada);
            AgregarCartaJugadaRingArea(cartaJugada);
        }
    }

    public void AgregarCartaJugadaRingArea(Carta cartaJugada)
    {
        listaPlayers[numJugadorActual].CartaPasaDeHandAlRingArea(cartaJugada);
    }
    
    public void ImpresionesAccionJugarCarta(Carta cartaJugada)
    {   
        DecirQueVaAJugarCarta(cartaJugada);
        view.SayThatPlayerSuccessfullyPlayedACard();
        DecirQueVaARecibirDaño(cartaJugada);
        ProbocarDanoAccionJugarCarta(cartaJugada);
    }

    public List<string> ObtenerStringCartasPosiblesJugar()
    {   
        List<Carta> cartasPosiblesJugar = listaPlayers[numJugadorActual].CartasPosiblesDeJugar();
        List<string> stringDeCartas= visualisarCartas.CrearListaStringCartaPlayed(cartasPosiblesJugar);

        return stringDeCartas;
    }
    
    public void SeleccionarCartasVer()
    {
        var setCartasParaVer = view.AskUserWhatSetOfCardsHeWantsToSee();
        switch (setCartasParaVer)
        {
            case CardSet.Hand:
                AccionVercartasTotales(listaPlayers[numJugadorActual].cartasHand);
                break;
            case CardSet.RingArea:
                AccionVercartasTotales(listaPlayers[numJugadorActual].cartasRingArea);
                break;
            case CardSet.RingsidePile:
                AccionVercartasTotales(listaPlayers[numJugadorActual].cartasRingSide);
                break;
            case CardSet.OpponentsRingArea:
                AccionVercartasTotales(listaPlayers[numJugadorDos].cartasRingArea);
                break;
            case CardSet.OpponentsRingsidePile:
                AccionVercartasTotales(listaPlayers[numJugadorDos].cartasRingSide);
                break;
        }
    }

    public void AccionVercartasTotales(List<Carta> conjuntoCartas)
    {   
        List<String> stringCartas = visualisarCartas.CrearListaStringCarta(conjuntoCartas);
        view.ShowCards(stringCartas);
    }

    public void ActualizacionNumJugadores()
    {
        numJugadorActual = (numJugadorActual == 0) ? 1 : 0;
        numJugadorDos = (numJugadorDos == 0) ? 1 : 0;
    }

    public void DecirQueVaAJugarCarta(Carta cartaJugada)
    {
        string cartaJugadaString = visualisarCartas.ObtenerStringPlayedInfo(cartaJugada);
        string nombreSuperStar = listaPlayers[numJugadorActual].superestar.Name;
        view.SayThatPlayerIsTryingToPlayThisCard(nombreSuperStar, cartaJugadaString);
    }

    public void DecirQueVaARecibirDaño(Carta cartaJugada)
    {   
        int danoRecibido = int.Parse(cartaJugada.Damage);
        string nombreSuperStarContrario = listaPlayers[numJugadorDos].superestar.Name;
        view.SayThatSuperstarWillTakeSomeDamage(nombreSuperStarContrario, danoRecibido);
    }

    public void ProbocarDanoAccionJugarCarta(Carta cartaJugada) 
    {   
        int danoTotal = int.Parse(cartaJugada.Damage);
        
        for (int i = 0; i < int.Parse(cartaJugada.Damage); i++)
        {   
            if (VerificarPuedeRecibirDano())
                MostrarUnaCartaVolteada(i, danoTotal);
            else
                SetearVariablesTrasGanar();
        }
    }
    
    public void MostrarUnaCartaVolteada(int danoActual, int danoTotal)
    {   
        Carta cartaVolteada = listaPlayers[numJugadorDos].CartaPasaDelArsenalAlRingSide();
        string cartaVolteadaString = visualisarCartas.ObtenerStringCartaInfo(cartaVolteada);
        view.ShowCardOverturnByTakingDamage(cartaVolteadaString, danoActual+1, danoTotal);
    }
    
    public bool VerificarPuedeRecibirDano()
    {
        return listaPlayers[numJugadorDos].cartasArsenal.Count > 0;
    }
    
    public void DeclararFinTurno()
    {
        _sigueTurno = false;
    }

    public void SetearVariableTruePorqueInicioTurno()
    {   
        _sigueTurno = true;
    }
    
    public void ActualizarVariablesPorFinTurno()
    {   
        DeclararFinTurno();
        ActualizacionNumJugadores();
        if (!VerificarSiJugadorTieneCartasEnArsenalParaSeguirJugando())
        {
            SetearVariablesTrasPerder();
        }
    }

    private bool VerificarSiJugadorTieneCartasEnArsenalParaSeguirJugando()
    {
        return listaPlayers[numJugadorActual].TieneCartasEnArsenal();
    }
    
    public void SetearVariablesTrasGanar()
    {
        numJugadorGanador = numJugadorActual;
        _sigueJuego = false;
        DeclararFinTurno();
    }
    
    public void SetearVariablesTrasPerder()
    {
        numJugadorGanador = (numJugadorActual == 0) ? 1 : 0;
        _sigueJuego = false;
        DeclararFinTurno();
    }
    
}
