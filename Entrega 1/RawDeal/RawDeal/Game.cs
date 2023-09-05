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

    public Mazo IniciarDeck()
    {
        Logica_Juego logicaJuego = new Logica_Juego(); 
        List<CartasJson> totalCartas = logicaJuego.DescerializarJsonCartas();
        List<SuperStarJSON> totalSuperStars = logicaJuego.DescerializarJsonSuperSta();
        
        string stringDeck = _view.AskUserToSelectDeck(_deckFolder);
        List<Cartas> listCartas = logicaJuego.CrearCartas(stringDeck, totalCartas);
        SuperStar superStarDeck = logicaJuego.CrearSuperStar(stringDeck, totalSuperStars);
        
        Mazo mazoReturn = new Mazo(listCartas, superStarDeck);
        
        return mazoReturn;
    }
        
    
    public void Play()
    {
        Mazo mazoUno = IniciarDeck();
        
        if (mazoUno.IsValid())
        {
            for (int i = 0; i < mazoUno.superestar.HandSize; i++)
            {
                mazoUno.robarCarta();
            }
                
            Mazo mazoDos = IniciarDeck();
            
            if (mazoDos.IsValid())
            {
                for (int i = 0; i < mazoDos.superestar.HandSize; i++)
                {
                    mazoDos.robarCarta();
                }
                JuegoValido(mazoUno, mazoDos);
            }
            else
                _view.SayThatDeckIsInvalid();
        }
        else
            _view.SayThatDeckIsInvalid();
    }
    
    public void JuegoValido(Mazo mazoUno, Mazo mazoDos) 
    {   
        Logica_Juego logicaJuego = new Logica_Juego();
        logicaJuego.MazoUno = mazoUno;
        logicaJuego.MazoDos = mazoDos;
        logicaJuego.view = _view;
        
        logicaJuego.JugadorInicioJuego();
        logicaJuego.CrearListaMazos();
        
        
        while (logicaJuego.SigueJuego())
        {
            logicaJuego.listaMazos[logicaJuego.numJugadorActual].robarCarta();
            List<PlayerInfo> listaPlayers = logicaJuego.CrearListaJugadores();
            _view.SayThatATurnBegins(logicaJuego.listaMazos[logicaJuego.numJugadorActual].superestar.Name);
            _view.ShowGameInfo(listaPlayers[logicaJuego.numJugadorActual], listaPlayers[logicaJuego.numJugadorDos]);
            
            //logicaJuego.AccionSeleccionadaJugador();
        }
        
        _view.CongratulateWinner(logicaJuego.listaMazos[logicaJuego.numJugadorGanador].superestar.Name);
    }
}