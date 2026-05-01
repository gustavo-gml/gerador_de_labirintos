namespace GeradorDeLabirintos.Validation;
using System.Collections.Generic;

public static class PathValidator
{
    /// <summary>
    /// Verifica usando Busca em Largura (BFS) se existe uma rota viável entre 'E' e 'S'.
    /// </summary>
    public static bool HasValidPath(char[,] maze)
    {
        int height = maze.GetLength(0);
        int width = maze.GetLength(1);

        int startX = -1, startY = -1;
        int endX = -1, endY = -1;

        // 1. Encontra automaticamente a Entrada (E) e Saída (S) na matriz
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (maze[y, x] == 'E') { startX = x; startY = y; }
                if (maze[y, x] == 'S') { endX = x; endY = y; }
            }
        }

        // Se por algum erro a matriz não tiver 'E' ou 'S', já bloqueia
        if (startX == -1 || endX == -1) return false;

        // 2. Configura a Busca em Largura (BFS)
        bool[,] visited = new bool[height, width];
        Queue<(int x, int y)> queue = new Queue<(int x, int y)>();

        queue.Enqueue((startX, startY));
        visited[startY, startX] = true;

        // Vetores de movimento: Cima, Baixo, Esquerda, Direita
        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { -1, 1, 0, 0 };

        // 3. Executa o BFS
        while (queue.Count > 0)
        {
            var (cx, cy) = queue.Dequeue();

            // Chegou no destino?
            if (cx == endX && cy == endY) return true;

            // Explora os 4 vizinhos
            for (int i = 0; i < 4; i++)
            {
                int nx = cx + dx[i];
                int ny = cy + dy[i];

                // Verifica se está dentro dos limites da matriz
                if (nx >= 0 && ny >= 0 && nx < width && ny < height)
                {
                    // Anda apenas por caminhos livres ('1') ou pela Saída ('S')
                    if (!visited[ny, nx] && (maze[ny, nx] == '1' || maze[ny, nx] == 'S'))
                    {
                        visited[ny, nx] = true;
                        queue.Enqueue((nx, ny));
                    }
                }
            }
        }

        // Se a fila esvaziou e não retornou true, é porque o caminho está bloqueado
        return false;
    }
}