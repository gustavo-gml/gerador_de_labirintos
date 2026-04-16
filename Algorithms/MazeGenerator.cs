namespace GeradorDeLabirintos.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;

class MazeGenerator
{
    private int width, height;
    private char[,] maze;
    private Random rng = new Random();

    public MazeGenerator(int width, int height)
    {
        // Garante que as dimensões sejam ímpares para o layout parede-caminho-parede
        this.width = width % 2 == 0 ? width + 1 : width;
        this.height = height % 2 == 0 ? height + 1 : height;
        maze = new char[this.height, this.width];
    }

    public void Generate()
    {
        // 1. Inicializa tudo como parede (0)
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                maze[y, x] = '0';

        // 2. Inicia a recursão a partir do ponto (1, 1)
        CarvePath(1, 1);

        // 3. Define Entrada (E) e Saída (S)
        maze[1, 1] = 'E';
        maze[height - 2, width - 2] = 'S';
    }

    private void CarvePath(int x, int y)
    {
        maze[y, x] = '1'; // Marca como caminho livre

        // Direções possíveis: Cima, Baixo, Esquerda, Direita (andando 2 casas)
        var directions = new List<(int dx, int dy)>
        {
            (0, -2), (0, 2), (-2, 0), (2, 0)
        };

        // Embaralha as direções para o labirinto ser aleatório
        directions = directions.OrderBy(d => rng.Next()).ToList();

        foreach (var (dx, dy) in directions)
        {
            int nx = x + dx;
            int ny = y + dy;

            if (IsInBounds(nx, ny) && maze[ny, nx] == '0')
            {
                // Remove a parede entre a célula atual e a próxima
                maze[y + dy / 2, x + dx / 2] = '1';
                
                // Recursão para a nova célula
                CarvePath(nx, ny);
            }
        }
    }

    private bool IsInBounds(int x, int y)
    {
        return x > 0 && x < width - 1 && y > 0 && y < height - 1;
    }

    public void Display()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(maze[y, x] + " ");
            }
            Console.WriteLine();
        }
    }
}