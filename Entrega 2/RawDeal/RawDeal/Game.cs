using System.ComponentModel.Design;
using RawDealView;

namespace RawDeal;

public class Game
{
    private View _view;
    private string _deckFolder;
    
    
    public Game(View view, string deckFolder)
    {
        _view = view;
        _deckFolder = deckFolder;
    }
    
    // Traer funciones que son de flujo para aca
    public Mazo IniciarDeck() // Aplicar Clean Code
    {
        Logica_Juego logicaJuego = new Logica_Juego(); 
        List<CartasJson> totalCartas = logicaJuego.DescerializarJsonCartas();
        List<SuperStarJSON> totalSuperStars = logicaJuego.DescerializarJsonSuperSta();
        
        string stringDeck = _view.AskUserToSelectDeck(_deckFolder);
        List<Carta> listCartas = logicaJuego.CrearCartas(stringDeck, totalCartas);
        SuperStar superStarDeck = logicaJuego.CrearSuperStar(stringDeck, totalSuperStars);
        
        Mazo mazoReturn = new Mazo(listCartas, superStarDeck);
        
        return mazoReturn;
    }
    
    public void Play() // Aplicar Clean Code
    {
        Mazo mazoUno = IniciarDeck();
        ValidarDeck validarDeck = new ValidarDeck();
        
        if (validarDeck.EsValidoMazo(mazoUno))
        {
            for (int i = 0; i < mazoUno.superestar.HandSize; i++)
            {
                mazoUno.RobarCarta();
            }
                
            Mazo mazoDos = IniciarDeck();
            
            if (validarDeck.EsValidoMazo(mazoDos))
            {
                for (int i = 0; i < mazoDos.superestar.HandSize; i++)
                {
                    mazoDos.RobarCarta();
                }
                JuegoValido(mazoUno, mazoDos);
            }
            else
                _view.SayThatDeckIsInvalid();
        }
        else
            _view.SayThatDeckIsInvalid();
    }
    
    public void JuegoValido(Mazo mazoUno, Mazo mazoDos) // Aplicar Clean Code
    {   
        Logica_Juego logicaJuego = new Logica_Juego();
        logicaJuego.MazoUno = mazoUno;
        logicaJuego.MazoDos = mazoDos;
        logicaJuego.view = _view;
        
        logicaJuego.JugadorInicioJuego();
        logicaJuego.CrearListaMazos();
        
        
        while (logicaJuego.SigueJuego()) 
        {
            logicaJuego.listaMazos[logicaJuego.numJugadorActual].RobarCarta();
            logicaJuego.DeclararInicioTurno();
            _view.SayThatATurnBegins(logicaJuego.listaMazos[logicaJuego.numJugadorActual].superestar.Name);

            while (logicaJuego.SigueTurno())
            {   
                List<PlayerInfo> listaPlayers = logicaJuego.CrearListaJugadores();
                _view.ShowGameInfo(listaPlayers[logicaJuego.numJugadorActual], listaPlayers[logicaJuego.numJugadorDos]);
                logicaJuego.AccionSeleccionadaJugador();
            }
            
        }
        
        _view.CongratulateWinner(logicaJuego.listaMazos[logicaJuego.numJugadorGanador].superestar.Name);
    }
}