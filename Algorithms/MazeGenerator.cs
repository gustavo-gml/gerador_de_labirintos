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

        // 2. Gera o labirinto iterativamente usando Pilha (Stack)
        CarvePathIterative(1, 1);

        // 3. Define Entrada (E) e Saída (S) de forma aleatória
        SetRandomEntryAndExit();
    }

    private void CarvePathIterative(int startX, int startY)
    {
        // Cria a pilha para substituir a recursão (evita StackOverflow)
        Stack<(int x, int y)> stack = new Stack<(int x, int y)>();
        
        // Marca o ponto inicial e coloca na pilha
        maze[startY, startX] = '1';
        stack.Push((startX, startY));

        // Direções possíveis (andando 2 casas)
        var directions = new List<(int dx, int dy)>
        {
            (0, -2), (0, 2), (-2, 0), (2, 0)
        };

        // O laço roda enquanto houver células na pilha
        while (stack.Count > 0)
        {
            var (cx, cy) = stack.Peek(); // Apenas olha o topo da pilha
            
            // Lista para armazenar vizinhos válidos que ainda são parede '0'
            var validNeighbors = new List<(int nx, int ny, int wallX, int wallY)>();

            foreach (var (dx, dy) in directions)
            {
                int nx = cx + dx;
                int ny = cy + dy;

                if (IsInBounds(nx, ny) && maze[ny, nx] == '0')
                {
                    // Salva o vizinho e também a coordenada da parede que os separa
                    validNeighbors.Add((nx, ny, cx + dx / 2, cy + dy / 2));
                }
            }

            if (validNeighbors.Count > 0)
            {
                // Escolhe um vizinho aleatório
                var next = validNeighbors[rng.Next(validNeighbors.Count)];

                // Remove a parede entre a célula atual e a próxima
                maze[next.wallY, next.wallX] = '1';
                // Marca a próxima célula como caminho
                maze[next.ny, next.nx] = '1';

                // Empilha a nova célula para avançar
                stack.Push((next.nx, next.ny));
            }
            else
            {
                // Chegou num beco sem saída: desempilha e retrocede (Backtracking)
                stack.Pop();
            }
        }
    }

    private void SetRandomEntryAndExit()
    {
        // Coleta todas as coordenadas que representam caminhos ('1')
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

        // Garante que há caminhos suficientes antes de definir entrada e saída
        if (validPaths.Count >= 2)
        {
            // Escolhe aleatoriamente o ponto de Entrada (E)
            int entryIndex = rng.Next(validPaths.Count);
            var entry = validPaths[entryIndex];
            maze[entry.y, entry.x] = 'E';
            
            // Remove esse ponto da lista para que a Saída não caia no mesmo lugar
            validPaths.RemoveAt(entryIndex);

            // Escolhe aleatoriamente o ponto de Saída (S)
            int exitIndex = rng.Next(validPaths.Count);
            var exit = validPaths[exitIndex];
            maze[exit.y, exit.x] = 'S';
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

    public char[,] GetMaze()
    {
        return maze;
    }
}