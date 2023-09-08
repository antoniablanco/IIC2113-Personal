using System.Security.Cryptography;
//using System.Collections.Generic;

namespace Actividad_03;

public class Jugador
{
    private int jugador;
    private List<List<int>> fichas = new List<List<int>>();

    public Jugador(int jugador, List<List<int>> fichaEntregadas)
    {
        //Console.Write("Las fichas que llegan son: ");
            
        foreach (List<int> ficha in fichaEntregadas)
        {
            //Console.Write("[" + ficha[0] + ", " + ficha[1] + "] ");

            List<int> numList = new List<int> { ficha[0], ficha[1] };
            fichas.Add(numList);
        }
            
        Console.WriteLine();;

        this.jugador = jugador;
    }

    public int ObtenerJugador() => jugador;

    public List<List<int>> ObtenerFichas() => fichas;

    public void EliminarFicha(int indice)
    {
        if (indice >= 0 && indice < fichas.Count())
        {
            fichas.RemoveAt(indice);
        }
    }

}