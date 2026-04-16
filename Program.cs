using GeradorDeLabirintos.Algorithms;
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

        // Pegas os argumentos de entrada 
        String tipoAlgoritmo = args[0];
        int largura = int.Parse(args[1]);
        int altura = int.Parse(args[2]);

        Console.WriteLine($"Gerando labirinto do tipo '{tipoAlgoritmo}' com tamanho {largura}x{altura}...");

        if (tipoAlgoritmo.Equals("a"))
        {
            MazeGenerator teste = new MazeGenerator(largura, altura);
            teste.Generate();
            teste.Display();
        }
        //Lógicas de chamadas para recursive ou autômato celular 
        
    }
}
