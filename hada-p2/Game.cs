using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Game
    {
        private bool finPartida = false;

        public Game()
        {
            gameLoop();
        }

        private void gameLoop()
        {
            // Generamos primero los tres barcos
            Barco barco1 = new Barco("Fragata Cristobal Colón", 4, 'v', new Coordenada(1, 1));
            Barco barco2 = new Barco("Destructor Imperial", 4, 'v', new Coordenada(1, 2));
            Barco barco3 = new Barco("SES Escudo del Pueblo", 4, 'v', new Coordenada(1, 3));

            Tablero tablero = new Tablero(10, new List<Barco>() { barco1, barco2, barco3 });
            tablero.eventoFinPartida += cuandoEventoFinPartida;

            do {
                Console.Clear();
                Console.WriteLine(tablero.DibujarTablero());
                Console.WriteLine("Introduce las coordenadas de tu disparo (fila,columna) o 's' para salir:");
                string input = Console.ReadLine();

                if (string.Equals(input, "s", StringComparison.OrdinalIgnoreCase))
                {
                    finPartida = true;
                    Console.WriteLine("Partida finalizada por el usuario.");
                    continue;
                }

                string[] partes = input.Split(',');
                if (partes.Length != 2)
                {
                    Console.WriteLine("Entrada no válida. Por favor, introduce dos números separados por un espacio.");
                    continue;
                }
                if (!int.TryParse(partes[0], out int fila) || !int.TryParse(partes[1], out int columna))
                {
                    Console.WriteLine("Entrada no válida. Por favor, introduce números enteros.");
                    continue;
                }
                Coordenada disparo = new Coordenada(fila, columna);
                tablero.Disparar(disparo);
            } while (!finPartida);
        }

        private void cuandoEventoFinPartida(object sender, EventArgs e)
        {
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }
    }
}
