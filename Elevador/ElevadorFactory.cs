using System;
using System.Collections.Generic;
using System.Text;

namespace Elevador
{
    // 0. CLASSE DA FÁBRICA DO ELEVADOR.
    // P.S: Definida como
    public class ElevadorFactory
    {
        // 1. CONSTRUTOR SEM PARÂMETROS:
        public ElevadorFactory() { }

        // 2. MÉTODO PARA TESTAR A CRIAÇÃO DE UM ELEVADOR:
        // Definimos a Capacidade Máxima desse protótipo como 5.
        public ElevadorModel PrototipoAlfa()
        {
            return new ElevadorModel(5);
        }
    }
}
