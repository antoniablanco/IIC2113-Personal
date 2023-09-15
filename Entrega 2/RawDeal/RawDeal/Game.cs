using System.ComponentModel.Design;
using RawDealView;
using RawDealView.Options;
using RawDeal.SuperStarClases;

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
    public Player IniciarPlayer(List<CartasJson> totalCartas,List<SuperStarJSON> totalSuperStars) 
    {
        string stringPlayer = _view.AskUserToSelectDeck(_deckFolder);
        _logicaJuego.view = _view;
        List<Carta> listaCartasPlayer = _logicaJuego.CrearCartas(stringPlayer, totalCartas);
        SuperStar superStarPlayer = _logicaJuego.CrearSuperStar(stringPlayer, totalSuperStars);
        
        Player playerReturn = new Player(listaCartasPlayer, superStarPlayer);
        
        return playerReturn;
    }
    
    public (List<CartasJson>, List<SuperStarJSON>) ObtenerTotalCartasYSuperStars() 
    {
        List<CartasJson> totalCartas = _logicaJuego.DescerializarJsonCartas();
        List<SuperStarJSON> totalSuperStars = _logicaJuego.DescerializarJsonSuperStar();
    
        return (totalCartas, totalSuperStars);
    }

    public Player CrearPlayer()
    {
        var (totalCartas, totalSuperStars) = ObtenerTotalCartasYSuperStars();
        Player player = IniciarPlayer(totalCartas, totalSuperStars);
        if (!_validarDeck.EsValidoMazo(player))
        {
            throw new ExcepcionMazoNoValido("El mazo no es valido");
        }
        return player;
    }
    
    public void IniciarVariablesLogicaJuego(Player playerUno, Player playerDos)
    {
        _logicaJuego.PlayerUno = playerUno;
        _logicaJuego.PlayerDos = playerDos;
        
        _logicaJuego.JugadorInicioJuego();
        _logicaJuego.CrearListaPlayers();
    }

    public void InicializarHandsPlayers()
    {
        _logicaJuego.PlayerUno.RobarCartasHandInicial();
        _logicaJuego.PlayerDos.RobarCartasHandInicial();
    }
    
    public void Play() 
    {
        try
        {
            Player playerUno = CrearPlayer();
            Player playerDos = CrearPlayer();
            IniciarVariablesLogicaJuego(playerUno, playerDos);
            InicializarHandsPlayers();
            JuegoDadoQueLosMazosSonValido();
        }
        catch (ExcepcionMazoNoValido e)
        {
            _view.SayThatDeckIsInvalid();
        }
    }
    
    public void JuegoDadoQueLosMazosSonValido()
    {
        while (_logicaJuego.SigueJuego())
        {
            SeJuegaUnTurno();
        }
        _view.CongratulateWinner(_logicaJuego.listaPlayers[_logicaJuego.numJugadorGanador].superestar.Name);
    }

    public void SeJuegaUnTurno()
    {
        _logicaJuego.SeSeteaInformacionInicioTurno();

        while (_logicaJuego.SigueTurno())
        {
            _logicaJuego.MostrarInformacionJugadores();
            AccionSeleccionadaJugador();
        }
    }
    
    public void AccionSeleccionadaJugador()
    {
        var actividadRealizar = ObtenerProximaJugada();

        switch (actividadRealizar)
        {
            case NextPlay.UseAbility:
                _logicaJuego.AccionUtilizarSuperHabilidad();
                break;
            case NextPlay.ShowCards:
                _logicaJuego.SeleccionarCartasVer();
                break;
            case NextPlay.PlayCard:
                _logicaJuego.AccionJugarCarta();
                break;
            case NextPlay.EndTurn:
                _logicaJuego.ActualizarVariablesPorFinTurno();
                break;
            case NextPlay.GiveUp:
                _logicaJuego.SetearVariablesTrasPerder();
                break;
        }
    }
    
    public NextPlay ObtenerProximaJugada()
    {   
        NextPlay actividadRealizar;
        
        if (_logicaJuego.JugadorPuedeUtilizarHabilidadSuperStar())
            actividadRealizar = _view.AskUserWhatToDoWhenUsingHisAbilityIsPossible();
        else
            actividadRealizar = _view.AskUserWhatToDoWhenHeCannotUseHisAbility();

        return actividadRealizar;
    }
    
}