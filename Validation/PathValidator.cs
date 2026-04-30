using GeradorDeLabirintos.Algorithms;
using System.Collections.Generic;
public class PathValidator
{
    public bool ExisteCaminho(int[,] mapa, Posicao inicio, Posicao fim)
    {
        int altura = mapa.GetLength(0);
        int largura = mapa.GetLength(1);
        
        // Marca quais posições já foram visitadas
        bool[,] visitado = new bool[altura, largura];

        // Fila
        Queue<Posicao> fila = new Queue<Posicao>();
        fila.Enqueue(inicio);
        visitado[inicio.Linha, inicio.Coluna] = true;

        // Movimentos possíveis (cima, baixo, esquerda, direita)
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        while (fila.Count > 0)
        {
            Posicao atual = fila.Dequeue();

            // Se chegou ao destino, existe caminho
            if (atual.Linha == fim.Linha && atual.Coluna == fim.Coluna)
            {
                return true;
            }

            // Explora os vizinhos
            for (int i = 0; i < 4; i++)
            {
                int nx = atual.Linha + dx[i];
                int ny = atual.Coluna + dy[i];

                // Verifica se está dentro do mapa
                if (nx >= 0 && ny >= 0 && nx < altura && ny < largura)
                {   
                    // Só anda em células de caminho (1) e não visitadas
                    if (!visitado[nx, ny] && mapa[nx, ny] == 1)
                    {
                        fila.Enqueue(new Posicao(nx, ny));
                        visitado[nx, ny] = true;
                    }
                }
            }

        }
         // Se esgotou a busca, não existe caminho
        return false;
    }
}