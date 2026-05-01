using System;

namespace GeradorDeLabirintos.Core;

public static class InputParser
{
    public static ConfiguracaoMapa Parse(string[] args)
    {
        if (args.Length < 3)
        {
            MostrarAjuda();
            return null;
        }

        var config = new ConfiguracaoMapa { Tipo = args[0].ToLower() };

        // Validação de Tipo
        if (config.Tipo != "perfeito" && config.Tipo != "caverna")
        {
            Console.WriteLine($"Erro: Tipo '{config.Tipo}' inválido. Use 'perfeito' ou 'caverna'.");
            return null;
        }

        // Validação de Dimensões
        if (!int.TryParse(args[1], out int w) || !int.TryParse(args[2], out int h))
        {
            Console.WriteLine("Erro: Largura e Altura devem ser números inteiros.");
            return null;
        }
        config.Largura = w;
        config.Altura = h;

        // Validação de Validade (Opcional)
        config.DeveSerValido = true; // Default
        if (args.Length >= 4)
        {
            string v = args[3].ToLower();
            if (v == "invalido" || v == "false") config.DeveSerValido = false;
        }

        // Regra de Negócio Específica
        if (config.Tipo == "perfeito" && !config.DeveSerValido)
        {
            Console.WriteLine("Erro: Labirintos 'perfeitos' são sempre válidos por definição.");
            return null;
        }

        return config;
    }

    private static void MostrarAjuda()
    {
        Console.WriteLine("Uso: dotnet run -- <tipo> <largura> <altura> [status]");
        Console.WriteLine("Exemplos:");
        Console.WriteLine("  dotnet run -- perfeito 100 100");
        Console.WriteLine("  dotnet run -- caverna 100 100 invalido");
    }
}