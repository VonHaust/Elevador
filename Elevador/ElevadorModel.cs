namespace Elevador
{
    // Auth: March.

    // 0. CLASSE DA CONSTRUÇÃO DO ELEVADOR.
    public class ElevadorModel
    {
        // 1. DECLARAÇÃO DE VARIÁVEIS:
        // Todas as variáveis são encapsuladas para garantir que as outras classes possam somente visualizar os valores, sem alterar.
        public string StatusElevador { get; private set; }
        public int AndarAtual { get; private set; }
        public Boolean PortasAbertas { get; private set; }
        public List<int> Rota { get; private set; }
        public int QuantPassageiros { get; private set; }
        public int CapacidadeMax { get; private set; }
        /* P.S: Além das características (variáveis) mencionadas no texto, criamos uma variável extra para armazenar o Status do 
           elevador ANTES dele chegar ao andar desejado, parar e abrir as portas. Assim, conseguimos evitar que ele esqueça a
           direção que estava seguindo e fazer com que suba/desça até o fim dos andares maiores/menores, invertendo ao final.  */
        public string MemoriaDoStatus { get; private set; }

        // 2. CONSTRUTOR:
        // Define os valores iniciais das variáveis (quando o elevador está parado) e recebe a capacidade máxima de passageiros.
        public ElevadorModel(int limitePassageiros)
        {
            // Adicionamos uma exceção para se a capacidade máxima do elevador for informada como "0" ou menos:
            if (limitePassageiros <= 0)
            {
                throw new ArgumentException("A capacidade máxima do elevador deve ser maior que zero.");
            }

            StatusElevador = "Parado";
            AndarAtual = 0;
            PortasAbertas = true;
            Rota = new List<int>();
            QuantPassageiros = 0;
            CapacidadeMax = limitePassageiros;
            MemoriaDoStatus = "Point Zero";
        }

        // 3. EMBARCAR PASSAGEIROS:
        // Realiza a primeira função de um elevador: embarcar. Recebe a quantidade de passageiros que está embarcando.
        public void EmbarcarPassageiros(int passageiroEntrando)
        {
            // 3.1 RESTRIÇÕES:
            if (PortasAbertas != true || StatusElevador != "Parado")
            {
                throw new InvalidOperationException("Aguarde o elevador parar e as portas abrirem para realizar o embarque.");
            }

            if (passageiroEntrando < 0)
            {
                throw new ArgumentException("Por favor, informe um número válido de passageiro(s).");
            }
            /* Regra 8: "Uma rota não pode ser feita com um número superior à capacidade máxima de pessoas". 
               P.S: Para que a capacidade máxima seja atingida, é NECESSÁRIO embarcar passageiros, então implementamos essa regra nesse método. */
            if (QuantPassageiros + passageiroEntrando > CapacidadeMax)
            {
                throw new InvalidOperationException("A capacidade máxima foi atingida, por favor aguarde a próxima viagem.");
            }
            // 3.2 FUNÇÃO:
            else
            {
                QuantPassageiros += passageiroEntrando;
                Console.WriteLine("\nAtualmente, o elevador conta com " + QuantPassageiros + " passageiro(s).\n");
            }
        }

        // 4. SELECIONAR ANDAR:
        // Permite que o passageiro adicione o andar desejado à rota. Recebe o andar desejado.
        public void SelecionarAndar(int andarDesejado)
        {
            // 4.1 RESTRIÇÕES:
            if (PortasAbertas != true)
            {
                throw new InvalidOperationException("Um andar só pode ser selecionado com as portas abertas.");
            }

            if (andarDesejado == AndarAtual)
            {
                // Ignora o mesmo andar selecionado.
                return;
            }

            /* Regra 5: "A rota deve conter apenas os andares que ainda não foram visitados;"
               Deixa aberto à interpretação se o andar selecionado NUNCA deve ser adicionado novamente ou se ele só não deve ser adicionado se já estiver na lista.
               Nesse projeto, seguimos com a lógica do elevador real: um andar PODE ser solicitado novamente depois de já ter sido visitado, pois ao sair da Rota, 
               ele já não seria um andar "repetido". 
               P.S: Porém, se fôssemos limitar um andar para nunca aparecer novamente, criaríamos uma nova lista para guardar os andares que já foram visitados.*/
            if (Rota.Contains(andarDesejado))
            {
                // Recusa o andar já inserido na Rota.
                return;
            }
            // 4.2 FUNÇÃO:
            else
            {
                Rota.Add(andarDesejado);
                Console.WriteLine("O andar " + andarDesejado + " foi adicionado à Rota.");
            }
        }

        // 5. PREPARAR PARTIDA:
        // Realiza tanto a ação de fechar as portas do elevador, quanto a de decidir a direção da rota que ele irá seguir e manter.
        public void PrepararPartida()
        {
            // 5.1 RESTRIÇÃO:
            // Se não houver andares inseridos na rota:
            if (Rota.Count == 0)
            {
                throw new InvalidOperationException("Por favor, informe o andar de destino.");
            }
            // 5.2 PRIMEIRA FUNÇÃO:
            else
            {
                PortasAbertas = false;
                Console.WriteLine("As portas estão fechando. Por favor, mantenha a distância.");
            }

            // 5.3 SEGUNDA FUNÇÃO:
            // Aqui, finalmente fazemos uso da variável MemoriaDoStatus, afim de lembrar a direção da rota.
            if (MemoriaDoStatus == "Point Zero")
            {
                /* Se estamos iniciando a rota com o elevador "zerado", pegamos o primeiro andar selecionado
                   para definir a direção da Rota.
                   P.S: Por questões de economia de tempo, isso não significa que o primeiro andar selecionado será o primeiro
                   em que o elevador irá parar. Os que estiverem no meio dessa rota serão prioridade. */
                int primeiroAndarSelecionado = Rota[0];
                if (primeiroAndarSelecionado > AndarAtual)
                {
                    MemoriaDoStatus = "Subindo";
                }
                else if (primeiroAndarSelecionado < AndarAtual)
                {
                    MemoriaDoStatus = "Descendo";
                }
            }
            // Se já estamos no meio da viagem e o andar selecionado foi MAIOR que o atual:
            else if (MemoriaDoStatus == "Subindo")
            {
                // Criamos uma "bandeirinha" para nos avisar se ainda iremos subir. Se sim, retorna "true" e NÃO muda a direção.
                Boolean temComoSubir = false;
                foreach (int andar in Rota)
                {
                    if (andar > AndarAtual)
                    {
                        temComoSubir = true;
                        break;
                    }
                }
                if (temComoSubir == false)
                {
                    MemoriaDoStatus = "Descendo";
                }
            }
            // Se já estamos no meio da viagem e o andar selecionado foi MENOR que o atual:
            else if (MemoriaDoStatus == "Descendo")
            {
                // Criamos outra "bandeirinha", mas para avisar se vamos continuar descendo.
                Boolean temComoDescer = false;
                foreach (int andar in Rota)
                {
                    if (andar < AndarAtual)
                    {
                        temComoDescer = true;
                        break;
                    }
                }
                if (temComoDescer == false)
                {
                    MemoriaDoStatus = "Subindo";
                }
            }
            // Por fim, armazenamos o resultado da variável MemoriaDoStatus dentro da variável StatusElevador.
            StatusElevador = MemoriaDoStatus;
            Console.WriteLine("O elevador está: " + StatusElevador);
            Console.WriteLine("-------------------------");
        }

        // 6. MOVIMENTAR ELEVADOR:
        // Realiza tanto a ação de levar o elevador até o andar desejado, quanto a de "encerrar" o deslocamento.
        public void MovimentarElevador()
        {
            // 6.1 RESTRIÇÕES:
            if (PortasAbertas == true)
            {
                this.PrepararPartida();
            }

            if (Rota.Count == 0)
            {
                throw new InvalidOperationException("Ainda não há uma rota para seguir.");
            }

            // 6.2 PRIMEIRA FUNÇÃO:
            // Verifica se o elevador estava subindo ou descendo e decide o próximo andar com base nisso.

            // P.S: Criamos uma variável nula para agir como "trava de segurança" caso os métodos sejam executados fora de ordem.
            int? andarValido = null;

            // Se o elevador estava subindo, pega o próximo maior andar na lista e usa seu valor como substituto para o "nulo" acima.
            if (MemoriaDoStatus == "Subindo")
            {
                /* Aplicamos uma ordem crescente na lista da Rota para fazer com que, ao elevador chegar ao maior andar e começar a 
                   descer, ele pare nos andares mais próximos do último alcançado antes de ir até o menor.
                   Isso economiza tempo e energia, ao invés de, EX: sobe ao 9, desce ao -5, sobe ao 3. */
                Rota.Sort();
                foreach (int andar in Rota)
                {
                    if (andar > AndarAtual)
                    {
                        andarValido = andar;
                        break;
                    }
                }

            }
            // Faz o mesmo, só que se estivesse descendo e pega o menor andar.
            else if (MemoriaDoStatus == "Descendo")
            {
                // No sentido oposto, organizamos a lista e invertemos o resultado.
                Rota.Sort();
                Rota.Reverse();
                foreach (int andar in Rota)
                {
                    if (andar < AndarAtual)
                    {
                        andarValido = andar;
                        break;
                    }
                }
            }

            // 6.3 SEGUNDA FUNÇÃO:
            // Ao alcançar o andar desejado, prepara o elevador para o desembarque.
            if (andarValido != null)
            {
                // P.S: para atribuir o valor da variável nula, precisamos forçar a "constatação" desse valor com o comando ".Value".
                AndarAtual = andarValido.Value;
                // Removemos o andar alcançado da Rota.
                Rota.Remove(AndarAtual);
                // Paramos o elevador e abrimos as portas.
                StatusElevador = "Parado";
                PortasAbertas = true;

                Console.WriteLine("Chegamos ao andar " + AndarAtual + ". As portas estão abertas.");
            }
            else
            {
                // P.S: Trava extra de segurança:
                // Se chegou aqui e a variável andarValido continua nula, significa que ocorreu algum erro de lógica.
                throw new InvalidOperationException("Erro: não existe um andar válido na direção atual.");
            }
        }

        // 7. DESEMBARCAR PASSAGEIROS:
        // Realiza a função de desembarcar passageiros e avaliar se a rota foi concluída. Recebe a quantidade de passageiros que estão saindo.
        public void DesembarcarPassageiros(int passageiroSaindo)
        {
            // 7.1 RESTRIÇÕES DE SEGURANÇA:
            // P.S: passageiroSaindo 
            if (PortasAbertas != true || StatusElevador != "Parado")
            {
                throw new InvalidOperationException("Aguarde o elevador parar e as portas abrirem para realizar o desembarque.");
            }

            if (passageiroSaindo < 0)
            {
                throw new ArgumentException("Por favor, informe um número válido de passageiros.");
            }

            if (passageiroSaindo > QuantPassageiros)
            {
                throw new InvalidOperationException("Não é possível desembarcar mais passageiros do que os presentes no elevador.");
            }

            QuantPassageiros -= passageiroSaindo;
            Console.WriteLine(passageiroSaindo + " passageiro(s) desembarcaram. O elevador agora conta com " + QuantPassageiros + " passageiro(s).");

            // 7.2 FUNÇÃO DE DESEMBARQUE:

            // 7.3 VERIFICAÇÃO DE CONCLUSÃO DE ROTA
            if (Rota.Count == 0)
            {
                Console.WriteLine("\nRota concluída com sucesso.");
                Console.WriteLine("¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨");

                // Se o elevador não tiver encerrado a rota no térreo:
                if (AndarAtual != 0)
                {
                    Console.WriteLine("Retornando automaticamente ao térreo.");
                    // Adiciona o andar do térreo como o próximo destino.
                    Rota.Add(0);
                }
                else
                {
                    MemoriaDoStatus = "Point Zero";
                    Console.WriteLine("\nViagem finalizada. Aguardando no térreo.");
                    Console.WriteLine("========================================");
                }
            }
        }
    }
}
