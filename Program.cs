using System;
using GeradorDeLabirintos.Core;
using GeradorDeLabirintos.Algorithms;
using GeradorDeLabirintos.IO;

class Program
{
    public static void Main(string[] args)
    {
        // 1. Tenta interpretar os argumentos
        var config = InputParser.Parse(args);
        if (config == null) return; // Erro já tratado no Parser

        // 2. Prepara o nome do arquivo
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string nomeArquivo = $"mapa_{config.Tipo}_{config.Largura}x{config.Altura}_{config.SufixoArquivo}_{timestamp}.txt";

        // 3. Execução
        Console.WriteLine($"Iniciando geração: {config.Tipo} ({config.Largura}x{config.Altura})");

        if (config.Tipo == "perfeito")
        {
            var gen = new MazeGenerator(config.Largura, config.Altura);
            gen.Generate();
            MazeExporter.ExportToTxt(gen.GetMaze(), nomeArquivo);
        }
        else
        {
            var gen = new CaveGenerator(config.Largura, config.Altura, config.DeveSerValido);
            gen.Generate();
            MazeExporter.ExportToTxt(gen.GetMaze(), nomeArquivo);
        }

        Console.WriteLine("Processo finalizado com sucesso.");
    }
}