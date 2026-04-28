using System;


namespace GeradorDeLabirintos.Algorithms
{
    public class CaveGenerator
    {
        public void Gerar(int largura, int altura)
        {
            int[,] caverna = new int[altura, largura]; // criando a Matriz
            Random rand = new Random();

            for (int i = 0; i < altura; i++)
            {
                for (int j = 0; j < largura; j++)
                {
                    caverna[i, j] = (rand.Next(100) < 55) ? 0 : 1;
                }
            }

            for (int i = 0; i < altura; i++)
            {
                for (int j = 0; j < largura; j++)
                {
                    Console.Write(caverna[i, j] == 0 ? '#' : '.');
                }
                Console.WriteLine();
            }

            for (int k = 0; k < 6; k++)
            {
                caverna = AplicarAutomato(caverna, altura, largura);
            }
            Console.WriteLine("Mapa Final\n");
            for (int i = 0; i < altura; i++)
            {
                for (int j = 0; j < largura; j++)
                {
                    Console.Write(caverna[i, j] == 0 ? '#' : '.');
                }
                Console.WriteLine();
            }
        }

        public int ContarVizinhos(int[,] mapa, int x, int y, int altura, int largura)
        {
            int contador = 0
            ;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue; // ignora a própria célula

                    int nx = x + i;
                    int ny = y + j;

                    if (nx < 0 || ny < 0 || nx >= altura || ny >= largura)
                    {
                        contador++;
                    }
                    else if (mapa[nx, ny] == 0)
                    {
                        contador++;
                    }

                }
            }
            return contador;
        }

        public int[,] AplicarAutomato(int[,] mapa, int altura, int largura)
        {
            int[,] novoMapa = new int[altura, largura];

            for (int i = 0; i < altura; i++)
            {
                for (int j = 0; j < largura; j++)
                {
                    int vizinhos = ContarVizinhos(mapa, i, j, altura, largura);

                    if (vizinhos >= 5)
                    {
                        novoMapa[i, j] = 0;
                    }
                    else
                    {
                        novoMapa[i, j] = 1;
                    }
                }
            }

            return novoMapa;
        }
    }
}