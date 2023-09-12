using System.ComponentModel.Design;
using RawDealView;

namespace RawDeal;

public class Game
{
    private View _view;
    private string _deckFolder;
    private Logica_Juego _logicaJuego = new Logica_Juego();
    private ValidarDeck _validarDeck = new ValidarDeck();
    
    
    public Game(View view, string deckFolder)
    {
        _view = view;
        _deckFolder = deckFolder;
    }
    
    // Traer funciones que son de flujo para aca
    public Mazo IniciarMazo(List<CartasJson> totalCartas,List<SuperStarJSON> totalSuperStars) 
    {
        string stringMazo = _view.AskUserToSelectDeck(_deckFolder);
        List<Carta> listaCartasMazo = _logicaJuego.CrearCartas(stringMazo, totalCartas);
        SuperStar superStarMazo = _logicaJuego.CrearSuperStar(stringMazo, totalSuperStars);
        
        Mazo mazoReturn = new Mazo(listaCartasMazo, superStarMazo);
        
        return mazoReturn;
    }
    
    public (List<CartasJson>, List<SuperStarJSON>) ObtenerTotalCartasYSuperStars() 
    {
        List<CartasJson> totalCartas = _logicaJuego.DescerializarJsonCartas();
        List<SuperStarJSON> totalSuperStars = _logicaJuego.DescerializarJsonSuperStar();
    
        return (totalCartas, totalSuperStars);
    }

    public Mazo CrearMazo()
    {
        var (totalCartas, totalSuperStars) = ObtenerTotalCartasYSuperStars();
        Mazo mazo = IniciarMazo(totalCartas, totalSuperStars);
        if (!_validarDeck.EsValidoMazo(mazo))
        {
            throw new ExcepcionMazoNoValido("El mazo no es valido");
        }
        return mazo;
    }
    
    public void IniciarVariablesLogicaJuego(Mazo mazoUno, Mazo mazoDos)
    {
        _logicaJuego.MazoUno = mazoUno;
        _logicaJuego.MazoDos = mazoDos;
        _logicaJuego.view = _view;
        
        _logicaJuego.JugadorInicioJuego();
        _logicaJuego.CrearListaMazos();
    }

    public void InicializarHandsMazos()
    {
        _logicaJuego.MazoUno.RobarCartasHandInicial();
        _logicaJuego.MazoDos.RobarCartasHandInicial();
    }
    
    public void Play() 
    {
        try
        {
            Mazo mazoUno = CrearMazo();
            Mazo mazoDos = CrearMazo();
            IniciarVariablesLogicaJuego(mazoUno, mazoDos);
            InicializarHandsMazos();
            JuegoDadoQueLosMazosSonValido();
        }
        catch (ExcepcionMazoNoValido e)
        {
            _view.SayThatDeckIsInvalid();
        }
    }
    
    public void JuegoDadoQueLosMazosSonValido() // Aplicar Clean Code
    {
        while (_logicaJuego.SigueJuego()) 
        {
            _logicaJuego.listaMazos[_logicaJuego.numJugadorActual].RobarCarta();
            _logicaJuego.DeclararInicioTurno();
            _view.SayThatATurnBegins(_logicaJuego.listaMazos[_logicaJuego.numJugadorActual].superestar.Name);

            while (_logicaJuego.SigueTurno())
            {   
                List<PlayerInfo> listaPlayers = _logicaJuego.CrearListaJugadores();
                _view.ShowGameInfo(listaPlayers[_logicaJuego.numJugadorActual], listaPlayers[_logicaJuego.numJugadorDos]);
                _logicaJuego.AccionSeleccionadaJugador();
            }
        }
        _view.CongratulateWinner(_logicaJuego.listaMazos[_logicaJuego.numJugadorGanador].superestar.Name);
    }
}