// See https://aka.ms/new-console-template for more information

using System.Xml;
using Actividad_03;

// Funciones utiles
bool ListaIgual(List<List<int>> lista1, List<int> ficha)
{
      for (int i = 0; i < lista1.Count; i++)
      {   
            //Console.WriteLine($"Los valores que se estan comparando son [{lista1[i][0]},{lista1[i][1]}] con ficha [{ficha[0]},{ficha[1]}]");
            if (lista1[i][0] == ficha[0] && lista1[i][1] == ficha[1])
            {
                  return true;
            }
      }

      return false;
}

List<int> VerificarFicha(List<List<int>> lista1, int valor)
{     
      List<int> respuesta = new List<int> {8,8};
      for (int i = 0; i < lista1.Count; i++)
      {   
            if (lista1[i][0] == valor)
            {
                  respuesta[0] = i;
                  respuesta[1] = 0;
            }
            else if (lista1[i][1] == valor)
            {
                  respuesta[0] = i;
                  respuesta[1] = 1;
            }
      }

      return respuesta;
}

int EncontrarPosicion(List<List<int>> lista1, List<int> ficha)
{     
      for (int i = 0; i < lista1.Count; i++)
      {   
            //Console.WriteLine($"Los valores que se estan comparando son [{lista1[i][0]},{lista1[i][1]}] con ficha [{ficha[0]},{ficha[1]}]");
            if (lista1[i][0] == ficha[0] && lista1[i][1] == ficha[1])
            {
                  return i;
            }
      }

      return -1;
}

Console.WriteLine("Iniciando Juego Domino");

// Creo todas las fichas que tiene el juego 
List<List<int>> fichasInicio = new List<List<int>>
{
      new List<int> {0, 0},
      new List<int> {0, 1},
      new List<int> {0, 2},
      new List<int> {0, 3},
      new List<int> {0, 4},
      new List<int> {0, 5},
      new List<int> {0, 6},
      new List<int> {1, 1},
      new List<int> {1, 2},
      new List<int> {1, 3},
      new List<int> {1, 4},
      new List<int> {1, 5},
      new List<int> {1, 6},
      new List<int> {2, 2},
      new List<int> {2, 3},
      new List<int> {2, 4},
      new List<int> {2, 5},
      new List<int> {2, 6},
      new List<int> {3, 3},
      new List<int> {3, 4},
      new List<int> {3, 5},
      new List<int> {3, 6},
      new List<int> {4, 4},
      new List<int> {4, 5},
      new List<int> {4, 6},
      new List<int> {5, 5},
      new List<int> {5, 6},
      new List<int> {6, 6}
};

Random rnd = new Random () ;
var jugadores = new Jugador[4];

// Inicializo a los jugadores con sus fichas
for (int i = 0; i < 4; i++)
{     
      //Console.WriteLine("Inicializando al jugador " + i);
      List<List<int>> inicializandoFichas = new List<List<int>>();

      for (int j = 0; j < 7; j++)
      {
            int index = rnd.Next(fichasInicio.Count);
            List<int> fichaSeleccionada = fichasInicio[index];
            fichasInicio.Remove(fichaSeleccionada);
            inicializandoFichas.Add(fichaSeleccionada);
            //Console.WriteLine("Ficha seleccionada: [" + fichaSeleccionada[0] + ", " + fichaSeleccionada[1] + "]"); 
      }

      jugadores[i] = new Jugador(i,inicializandoFichas);
}

// Imprimo las fichas de cada jugador
for (int i = 0; i < 4; i++)
{     
      Console.Write("Es el jugador " + jugadores[i].ObtenerJugador() + " y sus fichas son:");
      List<List<int>> listaDeFichas = jugadores[i].ObtenerFichas();
      foreach (List<int> ficha in listaDeFichas)
      {
            Console.Write("[" + ficha[0] + ", " + ficha[1] + "]");
      }
      Console.WriteLine();
}



// Se verifica cual es el jugador que comienza

var seEscogio = false;
int ctd = 0;
int num1 = 6;
int num2 = 6;
int jugadorInicio = 0;
while (!seEscogio)
{     
      //Console.WriteLine($"Probando al jugador numero {ctd} con la ficha [{num1},{num2}]");
      List<List<int>> listaDeFichas = jugadores[ctd].ObtenerFichas();
      List<int> ficha = new List<int> { num1, num2 };
      if (ListaIgual(listaDeFichas, ficha))
      {
            seEscogio = true;
            jugadorInicio = ctd;
            Console.WriteLine();
            Console.WriteLine($"El jugador que inicia es {jugadorInicio} dado que tiene la mayor carta que es [{num1},{num2}]");
      }

      ctd++;
      if (ctd > 3 && seEscogio==false)
      {
            ctd = 0;
            num2 -= 1;
            if (num2 < 0)
            {
                  num2 = 6;
                  num1 -= 1;
                  if (num1 == 0)
                  {
                        seEscogio = true;
                        Console.WriteLine("No se pudo escoger");
                  }
            }
      }

      //Console.ReadLine();
      
}



// Comenzando el juego (Supuesto de que siempre el jugador que si juega es el 0)
List<List<int>> fichasJugadas = new List<List<int>>();

var seguir = true;
var contadorRondas = 1;
int jugadorTurno = jugadorInicio;
var pasan = 0;

while (seguir)
{     
      Console.WriteLine();
      Console.WriteLine($"Ronda {contadorRondas} es turno del jugador {jugadorTurno}");
      Console.WriteLine("Posee las siguientes fichas:");
      List<List<int>> listaFichasTurno = jugadores[jugadorTurno].ObtenerFichas();
      foreach (List<int> ficha in listaFichasTurno)
      {
            Console.Write("[" + ficha[0] + ", " + ficha[1] + "]");
      }
      Console.WriteLine();
      Console.WriteLine($"Las fichas en la mesa se ven de la siguiente forma:");
      Console.WriteLine();
      foreach (List<int> ficha in fichasJugadas)
      {
            Console.Write("[" + ficha[0] + ", " + ficha[1] + "]");
      }
      
      // En caso de que sea el jugador interactivo
      if (jugadorTurno == 0)
      {     
            Console.WriteLine();
            Console.WriteLine("Indique la posición de la ficha a jugar comenzando desde el 0, si prefiere pasar escriba p");
            string str = Console.ReadLine();
            if (str != "p")
            {
                  pasan = 0;
                  int fichaJugada = Convert.ToInt32 (str) ;
                  Console.WriteLine("Escriba 0 si decide agregar la ficha a la izquierda de la lista, y 1 si es a la derecha");
                  string ubicacion = Console.ReadLine();
                  if (ubicacion == "0" || ubicacion == "1")
                  {
                        
                        Console.WriteLine("Si la ficha esta en el sentido correcto escriba 0, si hay que darla vuelta escriba 1");
                        string sentido = Console.ReadLine();
                        if (sentido == "0" || sentido == "1")
                        {
                              if (sentido == "0")
                              {
                                    List<int> fichaAgregada = new List<int>
                                          { listaFichasTurno[fichaJugada][0], listaFichasTurno[fichaJugada][1] };
                                    if (ubicacion == "0")
                                    {     
                                          if (listaFichasTurno[fichaJugada][1]==fichasJugadas[0][0])
                                          {
                                                //Console.WriteLine("Esta bien ubicada");
                                                //Console.WriteLine($"1{listaFichasTurno[fichaJugada][1]},{fichasJugadas[0][0]}");
                                                fichasJugadas.Insert(0, fichaAgregada); // Agregar al inicio de la lista
                                                jugadores[jugadorTurno].EliminarFicha(fichaJugada);
                                          }
                                          else
                                          {
                                                Console.WriteLine("La ficha en el sentido y ubicación indicado no es correcto, se ha perdido el turno");
                                                //Console.WriteLine($"1{listaFichasTurno[fichaJugada][1]},{fichasJugadas[0][0]}");
                                          }
                                                      
                                    }
                                    else
                                    {     
                                          if (listaFichasTurno[fichaJugada][0]==fichasJugadas[fichasJugadas.Count-1][1])
                                          {     
                                                fichasJugadas.Add(fichaAgregada); // Agregar al final de la lista 
                                                jugadores[jugadorTurno].EliminarFicha(fichaJugada);
                                                //Console.WriteLine("Esta bien ubicada");
                                                //Console.WriteLine($"2{listaFichasTurno[fichaJugada][0]},{fichasJugadas[fichasJugadas.Count-1][1]}");
                                          }
                                          else
                                          {
                                                Console.WriteLine("La ficha en el sentido y ubicación indicado no es correcto, se ha perdido el turno");
                                                //Console.WriteLine($"2{listaFichasTurno[fichaJugada][0]},{fichasJugadas[fichasJugadas.Count-1][1]}");
                                          }
                                        
                                    }
                              }
                              else
                              {
                                    List<int> fichaAgregada = new List<int>
                                          { listaFichasTurno[fichaJugada][1], listaFichasTurno[fichaJugada][0] };
                                    if (ubicacion == "0")
                                    {     
                                          if (listaFichasTurno[fichaJugada][0]==fichasJugadas[0][0])
                                          {
                                                //Console.WriteLine("Esta bien ubicada");
                                                //Console.WriteLine($"3{listaFichasTurno[fichaJugada][0]},{fichasJugadas[0][0]}");
                                                fichasJugadas.Insert(0, fichaAgregada); // Agregar al inicio de la lista
                                                jugadores[jugadorTurno].EliminarFicha(fichaJugada);
                                          }
                                          else
                                          {
                                                Console.WriteLine("La ficha en el sentido y ubicación indicado no es correcto, se ha perdido el turno");
                                                //Console.WriteLine($"3{listaFichasTurno[fichaJugada][0]},{fichasJugadas[0][0]}");
                                          }
                                    }
                                    else
                                    {     
                                          if (listaFichasTurno[fichaJugada][1]==fichasJugadas[fichasJugadas.Count-1][1])
                                          {     
                                                fichasJugadas.Add(fichaAgregada); // Agregar al final de la lista 
                                                jugadores[jugadorTurno].EliminarFicha(fichaJugada);
                                                //Console.WriteLine("Esta bien ubicada");
                                                //Console.WriteLine($"4{listaFichasTurno[fichaJugada][1]},{fichasJugadas[fichasJugadas.Count-1][1]}");
                                          }
                                          else
                                          {
                                                Console.WriteLine("La ficha en el sentido y ubicación indicado no es correcto, se ha perdido el turno");
                                                //Console.WriteLine($"4{listaFichasTurno[fichaJugada][1]},{fichasJugadas[fichasJugadas.Count-1][1]}");
                                          }
                                    }
                              }
                        }
                        else
                        {
                              Console.WriteLine("Escribio mal y perdio el turno");
                        }

                  }
                  else
                  {
                        Console.WriteLine("Escribio mal y perdio el turno");
                  }
                  
            }
            else
            {
                  pasan++;
                  Console.WriteLine("Ha decidido pasar");
            }

      }
      else
      {
            // Para el resto de los jugadores que se realiza automatico
            if (contadorRondas == 1)
            {     
                  List<int> NumBase = new List<int>{ num1, num2};
                  int pos_6 = EncontrarPosicion(jugadores[jugadorTurno].ObtenerFichas(), NumBase);
                  fichasJugadas.Add(NumBase); 
                  jugadores[jugadorTurno].EliminarFicha(pos_6);
                  Console.WriteLine($"Se agrego la ficha [{NumBase[0]},{NumBase[1]}]");
                  
            }
            else
            {     
                  Console.WriteLine();
                  // Console.WriteLine("Empezando el turno automatico"); // Eliminar
                  int op1 = fichasJugadas[0][0];
                  int op2 = fichasJugadas[fichasJugadas.Count - 1][1];
                  List<int> jugada1 = VerificarFicha(jugadores[jugadorTurno].ObtenerFichas(),op1);
                  List<int> jugada2 = VerificarFicha(jugadores[jugadorTurno].ObtenerFichas(),op2);
                  // Console.WriteLine($"Las opciones para jugar son [{jugada1[0]},{jugada1[1]}], [{jugada2[0]},{jugada2[1]}]"); // Eliminar
                  if (jugada1[0] == 8 && jugada2[0] == 8)
                  {
                        Console.WriteLine("No hay cartas para poder jugar y se paso el turno");
                        pasan++;
                  }
                  else
                  {
                        pasan = 0;
                        if (jugada1[0] != 8)
                        {     
                              // Console.WriteLine("Se entra en la jugada 1"); // Eliminar
                              if (jugada1[1] == 0)
                              {
                                    List<int> fichaAgregada = new List<int>
                                          { listaFichasTurno[jugada1[0]][1], listaFichasTurno[jugada1[0]][0] };
                                    fichasJugadas.Insert(0,fichaAgregada); // Agregar al inicio de la lista
                                    Console.WriteLine($"Se agrego la ficha [{ listaFichasTurno[jugada1[0]][1]}, {listaFichasTurno[jugada1[0]][0] }]");
                                    jugadores[jugadorTurno].EliminarFicha(jugada1[0]);
                              }
                              else
                              {
                                    List<int> fichaAgregada = new List<int>
                                          { listaFichasTurno[jugada1[0]][0], listaFichasTurno[jugada1[0]][1] };
                                    fichasJugadas.Insert(0,fichaAgregada); // Agregar al inicio de la lista
                                    Console.WriteLine($"Se agrego la ficha [{ listaFichasTurno[jugada1[0]][0]}, {listaFichasTurno[jugada1[0]][1] }]");
                                    jugadores[jugadorTurno].EliminarFicha(jugada1[0]);
                              }
                        }
                        else
                        {     
                              // Console.WriteLine("Se entra en la jugada 2");
                              if (jugada2[1] == 0)
                              {
                                    List<int> fichaAgregada = new List<int>
                                          { listaFichasTurno[jugada2[0]][0], listaFichasTurno[jugada2[0]][1] };
                                    fichasJugadas.Add(fichaAgregada); // Agregar al inicio de la lista
                                    Console.WriteLine($"Se agrego la ficha [{ listaFichasTurno[jugada2[0]][0]}, {listaFichasTurno[jugada2[0]][1] }]");
                                    jugadores[jugadorTurno].EliminarFicha(jugada2[0]);
                              }
                              else
                              {
                                    List<int> fichaAgregada = new List<int>
                                          { listaFichasTurno[jugada2[0]][1], listaFichasTurno[jugada2[0]][0] };
                                    fichasJugadas.Add(fichaAgregada); // Agregar al inicio de la lista
                                    Console.WriteLine($"Se agrego la ficha [{ listaFichasTurno[jugada2[0]][1]}, {listaFichasTurno[jugada2[0]][0] }]");
                                    jugadores[jugadorTurno].EliminarFicha(jugada2[0]);
                              }
                        }
                  }
            }
          
      }
      
      
      // Verifico las condiciones de termino del juego
      if (pasan > 3)
      {
            seguir = false;
            Console.WriteLine($"El juego a terminado por empate");
      }

      for (int i = 0; i < 4; i++)
      {
            if (jugadores[i].ObtenerFichas().Count() == 0)
            {     
                  seguir = false;
                  Console.WriteLine();
                  Console.WriteLine($"Ha ganado el jugador {i}!!!");
            }
      }

      //seguir = false;
      jugadorTurno++;
      contadorRondas++;
      if (jugadorTurno > 3)
      {
            jugadorTurno = 0;
      }
}
Console.WriteLine();
Console.WriteLine($"La cantidad de cartas que tenia el jugador:");
Console.WriteLine($"jugador 0 es {jugadores[0].ObtenerFichas().Count()}");
Console.WriteLine($"jugador 1 es {jugadores[1].ObtenerFichas().Count()}");
Console.WriteLine($"jugador 2 es {jugadores[2].ObtenerFichas().Count()}");
Console.WriteLine($"jugador 3 es {jugadores[3].ObtenerFichas().Count()}");
