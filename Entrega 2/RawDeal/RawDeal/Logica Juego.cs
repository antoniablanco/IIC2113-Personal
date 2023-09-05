
using RawDealView;
using RawDealView.Options;
using RawDealView.Utils;
using RawDealView.Views;

namespace RawDeal;
using System.Text.Json ;


public class Logica_Juego
{   
    private bool _sigueJuego = true;
    public View view;
    public Mazo MazoUno { get; set; }
    public Mazo MazoDos { get; set; }
    public int numJugadorActual = 0;
    public int numJugadorDos = 1;
    public int numJugadorGanador = 1;
    public int numjugadorInicio = 0;
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
    
    public List<Cartas> CrearCartas(string mazoString, List<CartasJson> totalCartas) 
    {
        List<Cartas> cartas = new List<Cartas>();
        string pathDeck = Path.Combine($"{mazoString}");
        string[] lines = File.ReadAllLines(pathDeck);

        foreach (var line in lines)
        {
            foreach (var carta in totalCartas)
            {
                string title = line.Trim();  // Elimina espacios en blanco alrededor
                if (title == carta.Title)
                {
                    Cartas nuevaCarta = new Cartas(carta.Title, carta.Types,carta.Subtypes,carta.Fortitude, carta.Damage, carta.StunValue, carta.CardEffect );
                    cartas.Add(nuevaCarta);
                }
            }
        }
        return cartas;
    }
    
    public SuperStar CrearSuperStar(string deck, List<SuperStarJSON> totalSuperStars)
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
        numjugadorInicio = (MazoUno.superestar.SuperstarValue < MazoDos.superestar.SuperstarValue) ? 1 : 0;
    }

    public List<PlayerInfo> CrearListaJugadores()
    {   
        
        PlayerInfo playerUno = new PlayerInfo(MazoUno.superestar.Name, 0,MazoUno.cartasHand.Count, MazoUno.cartasArsenal.Count);
        PlayerInfo playerDos = new PlayerInfo(MazoDos.superestar.Name, 0, MazoDos.cartasHand.Count, MazoDos.cartasArsenal.Count);
        
        List<PlayerInfo> listaPlayers = (numjugadorInicio == 0) ? new List<PlayerInfo> { playerUno, playerDos } : new List<PlayerInfo> { playerDos, playerUno };
        
        return listaPlayers;
    }

    public void CrearListaMazos()
    {
        listaMazos = (numjugadorInicio == 0) ? new List<Mazo> { MazoUno, MazoDos } : new List<Mazo> { MazoDos, MazoUno };
    }

    public void RobarCarta(Mazo mazo, PlayerInfo player)
    {
        mazo.robarCarta();
        //player.numberOfCardsInHand = mazo.cartasHand.Count;
        //player.numberOfCardsInArsenal = mazo.cartasArsenal.Count;
    }

    public bool SigueJuego()
    {   
        return (MazoUno.cartasArsenal.Count() > 0 && MazoDos.cartasArsenal.Count() > 0 && _sigueJuego);
    }

    public void AccionSeleccionadaJugador()
    {
        var actividadRealizar = view.AskUserWhatToDoWhenHeCannotUseHisAbility();
        Mazo mazo = listaMazos[numJugadorActual];

        switch (actividadRealizar)
        {
            case NextPlay.UseAbility:
                Console.WriteLine("El usuario eligió usar su habilidad.");
                break;
            case NextPlay.ShowCards:
                Console.WriteLine("El usuario eligió ver sus cartas.");
                AccionVerCartas(mazo);
                break;
            case NextPlay.PlayCard:
                Console.WriteLine("El usuario eligió jugar una carta.");
                break;
            case NextPlay.EndTurn:
                Console.WriteLine("El usuario eligió terminar su turno.");
                break;
            case NextPlay.GiveUp:
                Console.WriteLine("El usuario eligió rendirse.");
                numJugadorGanador = (numJugadorActual == 0) ? 1 : 0;
                _sigueJuego = false;
                break;
            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }

    public void AccionVerCartas(Mazo mazo)
    {
        var setCartasParaVer = view.AskUserWhatSetOfCardsHeWantsToSee();
        switch (setCartasParaVer)
        {
            case CardSet.Hand:
                Console.WriteLine("El usuario eligió ver su mano.");
                break;
            case CardSet.RingArea:
                Console.WriteLine("El usuario eligió ver Ring Area.");
                break;
            case CardSet.RingsidePile:
                Console.WriteLine("El usuario eligió ver su RingsidePile.");
                break;
            case CardSet.OpponentsRingArea:
                Console.WriteLine("El usuario eligió ver OpponentsRingArea.");
                break;
            case CardSet.OpponentsRingsidePile:
                Console.WriteLine("El usuario eligió ver OpponentsRingsidePile.");
                break;
            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }
}
