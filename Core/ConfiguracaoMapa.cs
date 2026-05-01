namespace GeradorDeLabirintos.Core;

public class ConfiguracaoMapa
{
    public string Tipo { get; set; }
    public int Largura { get; set; }
    public int Altura { get; set; }
    public bool DeveSerValido { get; set; }
    public string SufixoArquivo => DeveSerValido ? "valido" : "invalido";
}