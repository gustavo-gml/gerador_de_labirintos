using System;


namespace GeradorDeLabirintos.Algorithms
{
    public class CaveGenerator
    {
        private Random rand = new Random(); // Gerador de números aleatórios reutilizado
        public ResultadoLabirinto Gerar(int largura, int altura)
        {
            int[,] caverna = new int[altura, largura]; // Criando a matriz da caverna (0 = parede, 1 = caminho)

            // Preenchimento inicial aleatório
            // Aproximadamente 50% parede e 50% caminho
            for (int i = 0; i < altura; i++)
            {
                for (int j = 0; j < largura; j++)
                {
                    caverna[i, j] = (rand.Next(100) < 50) ? 0 : 1;
                }
            }

            // Exibe o mapa inicial (antes do autômato)
            for (int i = 0; i < altura; i++)
            {
                for (int j = 0; j < largura; j++)
                {
                    Console.Write(caverna[i, j] == 0 ? '#' : '.');
                }
                Console.WriteLine();
            }


             // Aplica o autômato celular algumas vezes
            // Isso suaviza o mapa, formando regiões mais naturais
            for (int k = 0; k < 2; k++)
            {
                caverna = AplicarAutomato(caverna, altura, largura);
            }

            // definir entrada e saída
            Posicao entrada = new Posicao(1, 1);
            Posicao saida = new Posicao(altura - 2, largura - 2);

            // garantir que são caminhos
            caverna[entrada.Linha, entrada.Coluna] = 1;
            caverna[saida.Linha, saida.Coluna] = 1;

            // Exibe o mapa final com marcação de entrada (E) e saída (S)
            Console.WriteLine("Mapa Final\n");
            for (int i = 0; i < altura; i++)
            {
                for (int j = 0; j < largura; j++)
                {
                    if (i == entrada.Linha && j == entrada.Coluna)
                        Console.Write('E');
                    else if (i == saida.Linha && j == saida.Coluna)
                        Console.Write('S');
                    else
                        Console.Write(caverna[i, j] == 0 ? '#' : '.');
                }
                Console.WriteLine();
            }

            // Retorna o resultado completo (mapa + posições)
            return new ResultadoLabirinto
            {
                Mapa = caverna,
                Entrada = entrada,
                Saida = saida
            };
        }

        public int ContarVizinhos(int[,] mapa, int x, int y, int altura, int largura)
        {
            int contador = 0
            ;

            // Percorre os 8 vizinhos ao redor da célula
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {   

                    if (i == 0 && j == 0) continue; // ignora a própria célula

                    int nx = x + i;
                    int ny = y + j;

                    // Se estiver fora do mapa, considera como parede
                    if (nx < 0 || ny < 0 || nx >= altura || ny >= largura)
                    {
                        contador++;
                    }
                    // Se for parede (0), incrementa contador
                    else if (mapa[nx, ny] == 0)
                    {
                        contador++;
                    }

                }
            }
            return contador;
        }

        public int[,] AplicarAutomato(int[,] mapa, int altura, int largura)
        {
            int[,] novoMapa = new int[altura, largura];

            for (int i = 0; i < altura; i++)
            {
                for (int j = 0; j < largura; j++)
                {
                    int vizinhos = ContarVizinhos(mapa, i, j, altura, largura);
                    
                    // Regra:
                    // Se tiver muitos vizinhos parede, vira parede
                    if (vizinhos >= 5)
                    {
                        novoMapa[i, j] = 0;
                    }
                    else
                    {
                        novoMapa[i, j] = 1;
                    }
                }
            }

            return novoMapa;
        }
    }
}