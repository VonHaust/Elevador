namespace Elevador
{
    internal class Program
    {
        // 0. CLASSE PRINCIPAL.
        static void Main(string[] args)
        {
            // 1. CHAMADA DAS CLASSES E MÉTODOS:
            // P.S: Poderíamos chamar a classe ElevadorModel e definir sua capacidade máxima aqui mesmo, mas escolhi manter
            // o raciocínio de "saído da fábrica" e fazer com que a ela defina esse detalhe.
            ElevadorFactory Fabricar = new ElevadorFactory();
            ElevadorModel Serie1 = Fabricar.PrototipoAlfa();

            Console.WriteLine("##### INICIANDO A PRIMEIRA VIAGEM #####");

            // Adicionamos um bloco try-catch para caso algo falhe dentro dos métodos.
            try
            {
                // 1.1. O primeiro passageiro embarca e seleciona o andar desejado:
                Serie1.EmbarcarPassageiros(1);
                Serie1.SelecionarAndar(-3);

                // 1.2. Outro passageiro, lá em cima, chamou o elevador para poder embarcar:
                Serie1.SelecionarAndar(5);

                // 1.3. O elevador sobe ATÉ chegar no andar 5:
                Serie1.MovimentarElevador();

                // 1.4. O elevador chegou no andar 5. Os novos passageiros entraram e selecionaram novos andares:
                Serie1.EmbarcarPassageiros(2);
                Serie1.SelecionarAndar(-2);
                Serie1.SelecionarAndar(12);

                /* 1.5. Aplicamos um "while" para fazer com que o elevador complete todas as viagens de uma vez:
                   P.S: Poderíamos implementar uma "execução automática" no próprio método, porém isso impediria de fazer
                   simulações onde um passageiro embarca e adiciona um andar à Rota DURANTE o pecorrimento da lista. */
                while (Serie1.Rota.Count > 0)
                {
                    Serie1.MovimentarElevador();
                }
            }
            // 2. CASO ALGUM CENÁRIO DE EXCEÇÃO SEJA ACIONADO *OU* CASO OCORRA ALGUM ERRO INESPERADO:
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO DE SISTEMA: " + ex.Message);
            }
        }
    }
}
