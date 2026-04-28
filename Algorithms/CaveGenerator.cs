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
                    caverna[i, j] = (rand.Next(100) < 60) ? 0 : 1;
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

            // definir entrada e saída
            Posicao entrada = new Posicao(1, 1);
            Posicao saida = new Posicao(altura - 2, largura - 2);

            // garantir que são caminhos
            caverna[entrada.Linha, entrada.Coluna] = 1;
            caverna[saida.Linha, saida.Coluna] = 1;

            Console.WriteLine("Mapa Final\n"); // Mostrando a caverna em sua versão final
            for (int i = 0; i < altura; i++)
            {
                for (int j = 0; j < largura; j++)
                {
                    if (i == entrada.Linha && j == entrada.Coluna)
                        Console.Write('E');
                    else if (i == saida.Linha && j == saida.Coluna)
                        Console.Write('S');
                    else
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