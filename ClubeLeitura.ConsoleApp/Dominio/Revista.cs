using System;

namespace ClubeLeitura.ConsoleApp.Dominio
{
    public class Revista
    {
        public DateTime ano;
        public string colecao;
        public int numeroEdicao;
        public int id;
        public int idRevistaEmprestimo;
        public int idCaixaRevista;

        public Revista()
        {
            id = GeradorId.GerarIdRevista();
        }

        public Revista(int idSelecionado)
        {
            id = idSelecionado;
        }

        public string Validar()
        {

            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(colecao))
                resultadoValidacao += "O campo coleção é obrigatório \n";

            if (idCaixaRevista == 0)
                resultadoValidacao += "Registre uma caixa primeiro \n";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "REVISTA_VALIDO";

            return resultadoValidacao;
        }

        public override bool Equals(object obj)
        {
            Revista revista = (Revista)obj;

            if (id == revista.id)
                return true;
            else
                return false;
        }
    }
}
