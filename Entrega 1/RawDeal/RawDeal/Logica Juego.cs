
using RawDealView;

namespace RawDeal;
using System.Text.Json ;


// Hay que cambiarlo para que reciba una clase mazo con cartas tipo carta
public class Logica_Juego
{
    public List<CartasJson> DescerializarJSONCartas()
    {
        string myJson = File.ReadAllText (Path.Combine("data","cards.json")) ;
        var cartas = JsonSerializer.Deserialize<List<CartasJson>>(myJson) ;
        return cartas;
    }
    
    public List<SuperStarJSON> DescerializarJSONSuperStar()
    {
        string myJson = File.ReadAllText (Path.Combine("data","superstar.json")) ;
        var superstars = JsonSerializer.Deserialize<List<SuperStarJSON>>(myJson) ;
        return superstars;
    }
    
    public List<Cartas> CrearCartas(string mazoString, List<CartasJson> totalCartas) // Crear Función
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
        string firstLine = lines[0];  // Obtener la primera línea del archivo
        
        foreach (var super in totalSuperStars)
        {   
            string superName = firstLine.Trim();  // Elimina espacios en blanco alrededor
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

    public int jugadorInicioJuego(Mazo mazoUno, Mazo mazoDos)
    {
        int respuesta = 0;
        if (mazoUno.superestar.SuperstarValue < mazoDos.superestar.SuperstarValue)
            respuesta = 1;
        
        return respuesta;
    }

    public List<PlayerInfo> crearListaJugadores(int numjugadorInicio, Mazo mazoUno, Mazo mazoDos)
    {   
        List<PlayerInfo> listaPlayers = new List<PlayerInfo>();
        
        PlayerInfo playerUno = new PlayerInfo(mazoUno.superestar.Name, 0,mazoUno.cartasHand.Count, mazoUno.cartasArsenal.Count);
        PlayerInfo playerDos = new PlayerInfo(mazoDos.superestar.Name, 0, mazoDos.cartasHand.Count, mazoDos.cartasArsenal.Count);
        
        if (numjugadorInicio == 0)
        {
            listaPlayers.Add(playerUno);
            listaPlayers.Add(playerDos);
        }
        else
        {
            listaPlayers.Add(playerDos);
            listaPlayers.Add(playerUno);
        }
        return listaPlayers;
    }

    public List<Mazo> crearListaMazos(int numInicio, Mazo mazoUno, Mazo mazoDos)
    {
        List<Mazo> listaMazos = new List<Mazo>();

        if (numInicio == 0)
        {
            listaMazos.Add(mazoUno);
            listaMazos.Add(mazoDos);
        }
        else
        {
            listaMazos.Add(mazoDos);
            listaMazos.Add(mazoUno);
        }

        return listaMazos;
    }

    public void RobarCarta(Mazo mazo, PlayerInfo player)
    {
        mazo.robarCarta();
        //player.numberOfCardsInHand = mazo.cartasHand.Count;
        //player.numberOfCardsInArsenal = mazo.cartasArsenal.Count;
    }
}

