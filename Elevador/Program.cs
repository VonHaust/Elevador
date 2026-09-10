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

            // Adicionamos um bloco try-catch para caso algo falhe dentro dos métodos.
            try
            {
                ElevadorFactory Fabricar = new ElevadorFactory();
                ElevadorModel Serie1 = Fabricar.PrototipoAlfa();

                Console.WriteLine("##### INICIANDO A PRIMEIRA VIAGEM #####");

                // 1.1. O primeiro passageiro embarca e seleciona o andar desejado:
                Serie1.EmbarcarPassageiros(1);
                Serie1.SelecionarAndar(-3);

                // 1.2. Outro passageiro, lá em cima, chamou o elevador para poder embarcar:
                Serie1.SelecionarAndar(5);

                // 1.3. O elevador desce até o -3º, o primeiro passageiro desembarca e o elevador depois sobe
                // até o 5º andar:
                Serie1.MovimentarElevador();
                Serie1.DesembarcarPassageiros(1);
                Serie1.MovimentarElevador();

                // 1.4. O elevador chegou no 5º andar. Os novos passageiros entram e selecionam os novos andares:
                Serie1.EmbarcarPassageiros(2);
                Serie1.SelecionarAndar(-2);
                Serie1.SelecionarAndar(12);

                /* 1.5. Aplicamos um "while" para fazer com que o elevador complete todas as viagens de uma vez:
                   P.S: Poderíamos fazer o mesmo lá no próprio método, porém isso atrapalharia na hora de fazer 
                   simulações onde um passageiro embarca/desembarca DURANTE o pecorrimento da lista. */
                while (Serie1.Rota.Count > 0)
                {
                    Serie1.MovimentarElevador();
                }

                /* 1.6. Os últimos passageiros desembarcam do elevador e ele retorna ao térreo:
                   P.S: Para demonstrar o uso do "while", simulamos que ninguém desembarcou no 12º andar (apertou por 
                   acidente) e todos saíram no -2º. Ademais, se adicionarmos essa linha dentro do "while", ele exibirá 
                   uma mensagem de erro no final ao tentar subtrair um passageiro do elevador que não tem mais ninguém 
                   dentro. */
                Serie1.DesembarcarPassageiros(2);
                Serie1.MovimentarElevador();
            }
            // 2. CASO ALGUM CENÁRIO DE EXCEÇÃO SEJA ACIONADO *OU* CASO OCORRA ALGUM ERRO INESPERADO:
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO DE SISTEMA: " + ex.Message);
            }
        }
    }
}
