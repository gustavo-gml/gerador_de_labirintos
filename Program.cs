using GeradorDeLabirintos.Algorithms;
using GeradorDeLabirintos.IO;
class Program
{
    public static void Main(String[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Erro! Use: dotnet run -- <tipo> <largura> <altura>");
            Console.WriteLine("Exemplo: dotnet run -- caverna 60 30");
            return; // Encerra o programa se faltarem dados
        }

        // Pega os argumentos de entrada 
        string tipoAlgoritmo = args[0];
        int largura = int.Parse(args[1]);
        int altura = int.Parse(args[2]);

        string nomeArquivo = $"labirinto_{largura}x{altura}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

        Console.WriteLine($"Gerando labirinto do tipo '{tipoAlgoritmo}' com tamanho {largura}x{altura}...");

        switch (tipoAlgoritmo.ToLower())
        {
            case "perfeito":
                MazeGenerator labirinto = new MazeGenerator(largura, altura);
                labirinto.Generate();
                MazeExporter.ExportToTxt(labirinto.GetMaze(), nomeArquivo);
                //labirinto.Display(); não usual para labirintos massivos 
                break;


            //Lógicas de chamada para autômato celular 
            case "caverna": 
                Console.WriteLine("Em desenvolvimento...");
                break;

            default:
            Console.WriteLine($"Erro: Tipo de labirinto '{tipoAlgoritmo}' desconhecido.");
            Console.WriteLine("Opções válidas: 'perfeito' ou 'caverna'.");
            break;
        }
    }
}
