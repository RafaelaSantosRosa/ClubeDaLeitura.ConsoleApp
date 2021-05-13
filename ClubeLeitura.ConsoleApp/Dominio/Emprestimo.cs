using ClubeLeitura.ConsoleApp.Controlador;
using System;

namespace ClubeLeitura.ConsoleApp.Dominio
{
    public class Emprestimo
    {
        ControladorRevista revista;
        public DateTime dataEmprestimo;
        public DateTime dataDevolucao;
        public int id;
        public string nomeAmigo;
        public string colecao;

        public Emprestimo()
        {
            id = GeradorId.GerarIdEmprestimo();
        }

        public Emprestimo(int idSelecionado)
        {
            id = idSelecionado;
        }

        public string Validar()
        {
            string resultadoValidacao = "";

            if (dataEmprestimo == DateTime.Now)
                resultadoValidacao += "Empréstimo aberto no dia";

            //if(colecao = revista.RegistrarRevista().CompareTo(colecao))
            // resultadoValidacao += "Revista não disponível";

            Console.WriteLine();

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "EMPRESTIMO_VALIDO";

            return resultadoValidacao;
        }

        public override bool Equals(object obj)
        {
            Emprestimo emprestimo = (Emprestimo)obj;

            if (id == emprestimo.id)
                return true;
            else
                return false;
        }

    }
}
