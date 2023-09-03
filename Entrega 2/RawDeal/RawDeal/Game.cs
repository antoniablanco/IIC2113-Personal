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
        List<CartasJson> totalCartas = logicaJuego.DescerializarJSONCartas();
        List<SuperStarJSON> totalSuperStars = logicaJuego.DescerializarJSONSuperStar();
        
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
        int numjugadorInicio = logicaJuego.jugadorInicioJuego(mazoUno, mazoDos);
        List<Mazo> listaMazos = logicaJuego.crearListaMazos(numjugadorInicio, mazoUno, mazoDos);
        // List<PlayerInfo> listaPlayers = logicaJuego.crearListaJugadores(numjugadorInicio, mazoUno, mazoDos);
        
        bool sigueJuego = true;
        int numJugadorActual = 0;
        int numJugadorDos = 1;
        
        while (sigueJuego)
        {   
            //logicaJuego.RobarCarta(listaMazos[numJugadorActual],listaPlayers[numJugadorActual]);
            
            listaMazos[numJugadorActual].robarCarta();
            List<PlayerInfo> listaPlayers = logicaJuego.crearListaJugadores(numjugadorInicio, mazoUno, mazoDos);
            _view.SayThatATurnBegins(listaMazos[numJugadorActual].superestar.Name);
            _view.ShowGameInfo(listaPlayers[numJugadorActual], listaPlayers[numJugadorDos]);
            
            // Después se debe cambiar desde aqui
            //_view.AskUserWhatToDoWhenItIsNotPossibleToUseItsAbility();
            
            // Cuando se rinde
            _view.CongratulateWinner(listaMazos[numJugadorDos].superestar.Name);
            sigueJuego = false;
        }
    }
}