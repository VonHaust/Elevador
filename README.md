# Simulador de Elevador

Projeto de estudo que simula as operações de um elevador (embarque, desembarque, seleção de andares e movimentação), construído em C# sobre a plataforma .NET.

## **📖 Sobre**

Voltado para a prática de lógica de programação e modelagem orientada a objetos, o **Simulador de Elevador** reproduz o comportamento de um elevador real: inicia parado no térreo, recebe passageiros e solicitações de andar, monta uma rota e se movimenta seguindo um algoritmo de varredura (sobe até o último andar solicitado antes de inverter o sentido, e vice-versa), sempre respeitando restrições de portas, status e capacidade máxima.

## **⚙️ Arquitetura e Fluxo de Dados**

O ecossistema funciona através da seguinte lógica:

* **`Program` (Console):** Classe principal utilizada para executar e demonstrar o funcionamento do simulador — chamando a fábrica, embarcando passageiros, selecionando andares e movimentando o elevador.
* **`ElevadorFactory`:** Instanciada sem parâmetros no construtor e, para fins de teste, também é responsável pela criação de uma instância pré-configurada de `ElevadorModel` (O método PrototipoAlfa(), que retorna uma instância com capacidade máxima para 5 passageiros).
* **`ElevadorModel`:** Núcleo da simulação. Armazena todo o estado do elevador (ex: andar atual, status, portas, rota e passageiros) e expõe apenas os métodos necessários para manipulá-lo, garantindo que as regras de negócio sejam sempre respeitadas.

## **💼 Funcionalidades**

* **Embarque e desembarque de passageiros:** Só podem ocorrer com o elevador parado e de portas abertas, respeitando a capacidade máxima definida na criação do elevador.
* **Seleção de andares e montagem de rota:** Passageiros selecionam andares de destino (com portas abertas), que são adicionados à rota — ignorando automaticamente o andar atual e andares já presentes na rota.
* **Movimentação inteligente (algoritmo de varredura):** O elevador mantém a direção (subindo/descendo) até esgotar os andares pendentes naquele sentido antes de inverter, evitando trajetos ineficientes.
* **Retorno automático ao térreo:** Ao concluir todas as paradas da rota, o elevador adiciona automaticamente uma viagem de volta ao andar zero, caso não esteja nele.
* **Tratamento de exceções:** Operações inválidas (ex: mover sem rota, selecionar andar com portas fechadas, exceder capacidade) lançam exceções específicas do .NET (`InvalidOperationException`, `ArgumentException`), capturadas e reportadas ao usuário no console.

## **🚀 Como Executar**

Para executar o projeto no seu ambiente, você precisará do **Visual Studio** instalado.

1. Clone este repositório;
2. Abra o projeto diretamente através do Visual Studio, ou pelo terminal na pasta do projeto;
3. Se necessário, faça a restauração das dependências digitando `dotnet restore`;
4. Pressione `F5` no Visual Studio, ou rode `dotnet run` pelo terminal;
5. Acompanhe a simulação pelo console!

*Nota: a sequência de embarques, seleções de andar e movimentações executada na simulação pode ser livremente editada no método `Main`, na classe `Program.cs`, para testar outros cenários de rota.*

## **🧠 Decisões de Implementação**

* **Memória da direção:** A propriedade MemoriaDoStatus foi criada para preservar a direção que o elevador estava seguindo antes de parar. Como o status passa para Parado ao chegar a um andar, essa informação permite que o elevador continue no mesmo sentido enquanto existirem destinos naquela direção, invertendo o percurso quando necessário.

* **Capacidade Útil:** Afim de validar a utilidade do elevador, é definido que sua capacidade não pode ser menor ou igual a zero dentro do próprio construtor.

* **Andares visitados:** Para a interpretação da regra: "A rota deve conter apenas os andares que ainda não foram visitados." Foi considerado que a Rota representa os andares que ainda estão pendentes de atendimento, e não um histórico permanente de todos os andares já visitados. Dessa forma, quando o elevador chega a um destino, o andar é removido da Rota e pode ser selecionado novamente posteriormente. Essa decisão considera que, em uma situação real, é possível solicitar novamente um andar que já tenha sido visitado anteriormente. Assim, a prevenção de duplicidade se aplica aos destinos que ainda estão presentes na rota atual.

* **Retorno ao térreo:** Quando uma rota é concluída em um andar diferente do 0, o sistema adiciona automaticamente o térreo como destino. Essa decisão representa o retorno do elevador ao seu ponto inicial após a conclusão da viagem.

* **Movimentação por etapas:** Cada chamada de MovimentarElevador() leva o elevador até o próximo andar válido da rota. No Program, um while é utilizado para continuar a movimentação até que todos os destinos sejam processados

## **💻 Tecnologias Utilizadas**

* **C# (.NET):** Linguagem e plataforma utilizadas como base do projeto.
* **Console App:** Interface simplificada, focada em expor a lógica da simulação.

<div align="center">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#">
  <img src="https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white" alt=".NET">
</div>
<br>

## **👩🏻‍💻 Autores**

[<img src="https://images.weserv.nl/?url=avatars.githubusercontent.com/u/50738663?v=4&h=125&w=125&fit=cover&mask=circle&maxage=7d" width=115><br><sub>Marcella Portela</sub>](https://github.com/VonHaust)
