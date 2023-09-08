
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
    public Mazo MazoUno { get; set; }
    public Mazo MazoDos { get; set; }
    public int numJugadorActual = 0;
    public int numJugadorDos = 1;
    public int numJugadorGanador = 1;
    public int NumJugadorInicio = 0;
    public List<Mazo> listaMazos;
    
    public List<CartasJson> DescerializarJsonCartas()
    {
        string myJson = File.ReadAllText (Path.Combine("data","cards.json")) ;
        var cartas = JsonSerializer.Deserialize<List<CartasJson>>(myJson) ;
        return cartas;
    }
    
    public List<SuperStarJSON> DescerializarJsonSuperSta()
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
                string title = line.Trim();  // Elimina espacios en blanco alrededor
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
        
        foreach (var super in totalSuperStars)
        {   
            string superName = firstLine.Trim();  
            if (superName.Contains(super.Name))
            {   
                SuperStar superstar = new SuperStar(super.Name, super.Logo, super.HandSize, super.SuperstarValue,
                    super.SuperstarAbility);
                return superstar;
            }
        }
        SuperStar superstarNull = new SuperStar("Null", "Null", 0, 0,"Null");
        return superstarNull;
        
    }

    public void JugadorInicioJuego()
    {
        NumJugadorInicio = (MazoUno.superestar.SuperstarValue < MazoDos.superestar.SuperstarValue) ? 1 : 0;
    }

    public List<PlayerInfo> CrearListaJugadores()
    {   
        
        PlayerInfo playerUno = new PlayerInfo(MazoUno.superestar.Name, 0,MazoUno.cartasHand.Count, MazoUno.cartasArsenal.Count);
        PlayerInfo playerDos = new PlayerInfo(MazoDos.superestar.Name, 0, MazoDos.cartasHand.Count, MazoDos.cartasArsenal.Count);
        
        List<PlayerInfo> listaPlayers = (NumJugadorInicio == 0) ? new List<PlayerInfo> { playerUno, playerDos } : new List<PlayerInfo> { playerDos, playerUno };
        
        return listaPlayers;
    }

    public void CrearListaMazos()
    {
        listaMazos = (NumJugadorInicio == 0) ? new List<Mazo> { MazoUno, MazoDos } : new List<Mazo> { MazoDos, MazoUno };
    }

    public bool SigueJuego()
    {   
        return (MazoUno.cartasArsenal.Count() > 0 && MazoDos.cartasArsenal.Count() > 0 && _sigueJuego);
    }

    public bool SigueTurno()
    {
        return _sigueTurno;
    }
    
    public void AccionSeleccionadaJugador()
    {
        var actividadRealizar = view.AskUserWhatToDoWhenHeCannotUseHisAbility();

        switch (actividadRealizar)
        {
            case NextPlay.UseAbility:
                break;
            case NextPlay.ShowCards:
                SeleccionarCartasVer();
                break;
            case NextPlay.PlayCard:
                SeleccionarCartasJugar();
                break;
            case NextPlay.EndTurn:
                ActualizarVariablesPorFinTurno();
                break;
            case NextPlay.GiveUp:
                SetearVariablesTrasRendirse();
                break;
        }
    }

    public void SeleccionarCartasJugar()
    {
        
    }
    
    public void SeleccionarCartasVer()
    {
        var setCartasParaVer = view.AskUserWhatSetOfCardsHeWantsToSee();
        switch (setCartasParaVer)
        {
            case CardSet.Hand:
                AccionVercartas(listaMazos[numJugadorActual], listaMazos[numJugadorActual].cartasHand);
                break;
            case CardSet.RingArea:
                AccionVercartas(listaMazos[numJugadorActual], listaMazos[numJugadorActual].cartasRingArea);
                break;
            case CardSet.RingsidePile:
                AccionVercartas(listaMazos[numJugadorActual], listaMazos[numJugadorActual].cartasRingSide);
                break;
            case CardSet.OpponentsRingArea:
                AccionVercartas(listaMazos[numJugadorDos], listaMazos[numJugadorDos].cartasRingArea);
                break;
            case CardSet.OpponentsRingsidePile:
                AccionVercartas(listaMazos[numJugadorDos], listaMazos[numJugadorDos].cartasRingSide);
                break;
        }
    }

    public void AccionVercartas(Mazo mazo, List<Carta> conjuntoCartas)
    {
        List<String> stringCartas = mazo.CrearListaStringCarta(conjuntoCartas);
        view.ShowCards(stringCartas);
    }

    public void ActualizacionNumJugadores()
    {
        numJugadorActual = (numJugadorActual == 0) ? 1 : 0;
        numJugadorDos = (numJugadorDos == 0) ? 1 : 0;
    }

    public void DeclararFinTurno()
    {
        _sigueTurno = false;
    }

    public void DeclararInicioTurno()
    {
        _sigueTurno = true;
    }
    
    public void ActualizarVariablesPorFinTurno()
    {   
        DeclararFinTurno();
        ActualizacionNumJugadores();
    }
    
    public void SetearVariablesTrasRendirse()
    {
        numJugadorGanador = (numJugadorActual == 0) ? 1 : 0;
        _sigueJuego = false;
        DeclararFinTurno();
    }
    
}
