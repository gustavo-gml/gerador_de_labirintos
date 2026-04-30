namespace GeradorDeLabirintos.IO;
using System;
using System.IO;

public static class MazeExporter
{
    /// <summary>
    /// Exporta a matriz de char para um arquivo .txt com alta performance.
    /// </summary>
    /// <param name="maze">A matriz do labirinto</param>
    /// <param name="filePath">O caminho e nome do arquivo (ex: "labirinto_10k.txt")</param>
    public static void ExportToTxt(char[,] maze, string filePath)
    {
        int height = maze.GetLength(0);
        int width = maze.GetLength(1);

        Console.WriteLine($"Iniciando exportação para {filePath}...");

        // O StreamWriter abre um "túnel" direto para o arquivo no disco
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int y = 0; y < height; y++)
            {
                // TRUQUE DE PERFORMANCE: 
                // Em vez de gravar caractere por caractere,
                // copiamos a linha inteira para um array temporário e gravamos a linha de uma vez.
                char[] row = new char[width];
                for (int x = 0; x < width; x++)
                {
                    row[x] = maze[y, x];
                }
                
                // Grava a linha inteira no arquivo
                writer.WriteLine(row);
            }
        }

        Console.WriteLine("Exportação concluída com sucesso!");
    }
}