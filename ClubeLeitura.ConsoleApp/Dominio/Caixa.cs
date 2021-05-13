using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeitura.ConsoleApp.Dominio
{
    public class Caixa
    {
        public string cor;
        public string etiqueta;
        public int id;

        public Caixa()
        {
            id = GeradorId.GerarIdCaixa();
        }

        public Caixa(int idSelecionado)
        {
            id = idSelecionado;
        }

        public string Validar()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(cor))
                resultadoValidacao += "O campo cor é obrigatório \n";

            if (string.IsNullOrEmpty(etiqueta))
                resultadoValidacao += "O campo etiqueta é obrigatório \n";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "CAIXA_VALIDO";

            return resultadoValidacao;
        }

        public override bool Equals(object obj)
        {
            Caixa caixa = (Caixa)obj;

            if (id == caixa.id)
                return true;
            else
                return false;
        }
    }
}
