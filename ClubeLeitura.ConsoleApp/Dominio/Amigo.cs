using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeitura.ConsoleApp.Dominio
{
    public class Amigo
    {
        public string nome;
        public string nomeResponsavel;
        public int telefone;
        public string lugar;
        public int id;

        public Amigo()
        {
            id = GeradorId.GerarIdAmigo();
        }

        public Amigo(int idSelecionado)
        {
            id = idSelecionado;
        }

        public string Validar()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(nome))
                resultadoValidacao += "O campo nome é obrigatório \n";

            if (string.IsNullOrEmpty(nomeResponsavel))
                resultadoValidacao += "O campo nome do responsável é obrigatório \n";

            if (string.IsNullOrEmpty(lugar))
                resultadoValidacao += "O campo de onde é o amiguinho é obrigatório \n";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "AMIGO_VALIDO";

            return resultadoValidacao;
        }

        public override bool Equals(object obj)
        {
            Amigo amigo = (Amigo)obj;

            if (id == amigo.id)
                return true;
            else
                return false;
        }
    }
}
