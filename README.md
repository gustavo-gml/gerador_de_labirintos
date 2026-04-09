# Gerador de Labirintos e Cavernas (C#)

Uma ferramenta desenvolvida em C# para geração de grafos em representação matricial, focada em análise de performance e estruturas de mapas para algoritmos de busca.

## 🚀 Sobre o Projeto

Esta aplicação gera cenários baseados em matrizes de tamanhos variáveis, utilizando algoritmos clássicos para criar caminhos complexos. O objetivo principal é fornecer uma base sólida para testes de performance e validação de rotas entre diferentes tipos de topologias.

## 🛠️ Algoritmos Implementados

A ferramenta utiliza duas abordagens distintas para a geração dos mapas:

* **Labirinto Perfeito (Recursive Backtracker):** Gera um labirinto onde existe exatamente um caminho entre qualquer par de pontos, garantindo que não existam áreas isoladas ou ciclos (loops).
* **Mapa de Caverna (Automata Celular):** Utiliza regras de vizinhança e sobrevivência de células para criar estruturas orgânicas, simulando formações rochosas naturais.

## 📋 Representação dos Dados

Os mapas exportados seguem a seguinte convenção de caracteres para garantir a compatibilidade entre diferentes sistemas:

| Caractere | Significado |
| :---: | :--- |
| `0` | **Obstáculo** (Parede) |
| `1` | **Caminho Livre** (Passagem) |
| `E` | **Entrada** (Point of Entry) |
| `S` | **Saída** (Point of Exit) |

## 🔍 Validação Lógica

Após o processo de geração, a ferramenta executa uma etapa de validação para assegurar que o cenário é funcional. 
- Realiza uma busca lógica para confirmar a existência de, pelo menos, uma **rota viável** entre os pontos **E** e **S**.
- Garante que o arquivo exportado está íntegro e segue o padrão matricial definido.

## 💾 Exportação

Os cenários finais são exportados para arquivos de texto (** .txt **) padronizados, permitindo que os dados sejam facilmente consumidos por outras aplicações ou scripts de análise de dados.

---
*Projeto desenvolvido para fins de estudo de algoritmos de grafos*