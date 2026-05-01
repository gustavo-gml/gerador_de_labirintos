namespace GeradorDeLabirintos.Algorithms;

using System;
using System.Collections.Generic;

public class CaveGenerator
{
    private int width, height;
    private char[,] maze;

    private bool validade;
    private Random rng = new Random();

    public CaveGenerator(int width, int height, bool validade)
    {
        this.width = width;
        this.height = height;
        this.validade = validade;
        this.maze = new char[height, width];
    }

    public void Generate()
    {

        while (true)
        {
            // 1. Preenchimento inicial aleatório (50% parede '0', 50% caminho '1')
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    maze[y, x] = (rng.Next(100) < 50) ? '0' : '1';
                }
            }

            // 2. Aplica o autômato celular (Aumentado em iterações)
            for (int k = 0; k < 2; k++)
            {
                AplicarAutomato();
            }

            // 3. Define Entrada (E) e Saída (S) de forma aleatória
            SetRandomEntryAndExit();

            //Gera algoritimo valido ou invalido
            bool status = GeradorDeLabirintos.Validation.PathValidator.HasValidPath(maze);
            if(status == validade)
            {
                break;
            }
        }
    }

    private void AplicarAutomato()
    {
        char[,] novoMapa = new char[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int vizinhosParede = ContarVizinhos(x, y);

                // Regra do Autômato Celular:
                // Se tiver 5 ou mais vizinhos que são parede, vira/continua parede.
                if (vizinhosParede >= 5)
                {
                    novoMapa[y, x] = '0';
                }
                else
                {
                    novoMapa[y, x] = '1';
                }
            }
        }

        // Atualiza o mapa principal
        maze = novoMapa;
    }

    private int ContarVizinhos(int x, int y)
    {
        int contador = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue; // Ignora a própria célula

                int nx = x + j;
                int ny = y + i;

                // Fora dos limites é considerado parede (ajuda a fechar as bordas do mapa)
                if (nx < 0 || ny < 0 || nx >= width || ny >= height)
                {
                    contador++;
                }
                else if (maze[ny, nx] == '0')
                {
                    contador++;
                }
            }
        }
        return contador;
    }

    private void SetRandomEntryAndExit()
    {
        // Mesma lógica utilizada no MazeGenerator
        List<(int x, int y)> validPaths = new List<(int x, int y)>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (maze[y, x] == '1')
                {
                    validPaths.Add((x, y));
                }
            }
        }

        if (validPaths.Count >= 2)
        {
            int entryIndex = rng.Next(validPaths.Count);
            var entry = validPaths[entryIndex];
            maze[entry.y, entry.x] = 'E';

            validPaths.RemoveAt(entryIndex);

            int exitIndex = rng.Next(validPaths.Count);
            var exit = validPaths[exitIndex];
            maze[exit.y, exit.x] = 'S';
        }
    }

    public char[,] GetMaze()
    {
        return maze;
    }

    public void Display()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Substituindo visualmente só no console para ficar mais legível, mas no txt vai como '0' e '1'
                if (maze[y, x] == '0') Console.Write("# ");
                else if (maze[y, x] == '1') Console.Write(". ");
                else Console.Write(maze[y, x] + " ");
            }
            Console.WriteLine();
        }
    }
}