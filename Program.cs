using GeradorDeLabirintos.Algorithms;




class Program
{
    public static void Main(String[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Erro! Use: dotnet run -- <tipo> <largura> <altura>");

            Console.WriteLine("Exemplo: dotnet run -- labirinto 60 30");

            Console.WriteLine("Exemplo: dotnet run -- caverna 60 30 valido");

            return; // Encerra o programa se faltarem dados
        }

        // Pegas os argumentos de entrada 
        String tipoAlgoritmo = args[0].ToLower();
        int largura = int.Parse(args[1]);
        int altura = int.Parse(args[2]);

        bool queroValido = true;

        if (args.Length >= 4)
        {
            String modo = args[3].ToLower().Trim();

            if (modo == "valido")
                queroValido = true;
            else if (modo == "invalido")
                queroValido = false;
            else
                Console.WriteLine("Use 'valido' ou 'invalido'");
        }

        string nomeArquivo = $"labirinto_{largura}x{altura}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

        Console.WriteLine($"Gerando labirinto do tipo '{tipoAlgoritmo}' com tamanho {largura}x{altura}...");

        switch (tipoAlgoritmo)
        {
            case "perfeito":
                if (!queroValido)
                {
                    Console.WriteLine("Labirinto perfeito é sempre válido. Ignorando 'invalido'.");
                }

                MazeGenerator labirinto = new MazeGenerator(largura, altura);
                labirinto.Generate();
                MazeExporter.ExportToTxt(labirinto.GetMaze(), nomeArquivo);
                break;

            case "caverna":
                CaveGenerator gerador = new CaveGenerator();
                PathValidator validador = new PathValidator();
                MazeExporter exporter = new MazeExporter();

                ResultadoLabirinto resultado;
                bool temCaminho;

                int tentativas = 0;
                int maxTentativas = 100;

                do
                {
                    resultado = gerador.Gerar(largura, altura);

                    temCaminho = validador.ExisteCaminho(
                        resultado.Mapa,
                        resultado.Entrada,
                        resultado.Saida
                    );

                    tentativas++;

                } while (temCaminho != queroValido && tentativas < maxTentativas);

                if (tentativas == maxTentativas)
                {
                    Console.WriteLine("Não foi possível gerar o tipo desejado.");
                }
                else
                {
                    Console.WriteLine(temCaminho ? "Mapa VÁLIDO" : "Mapa INVÁLIDO");

                    exporter.ExportToTxt(resultado.Mapa, nomeArquivo);

                    Console.WriteLine("Caverna salva com sucesso!");
                }

                break;

            default:
                Console.WriteLine($"Erro: Tipo '{tipoAlgoritmo}' desconhecido.");
                Console.WriteLine("Opções: 'perfeito' ou 'caverna'.");
                break;
        }
    }
}
