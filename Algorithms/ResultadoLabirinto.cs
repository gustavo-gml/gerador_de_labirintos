using GeradorDeLabirintos.Algorithms;


public class ResultadoLabirinto // Classe simples para agrupar os dados do labirinto gerado
{
    public int[,] Mapa; // matriz do labirinto (0 = parede, 1 = caminho)
    public Posicao Entrada; // posição inicial
    public Posicao Saida; // posição final
}