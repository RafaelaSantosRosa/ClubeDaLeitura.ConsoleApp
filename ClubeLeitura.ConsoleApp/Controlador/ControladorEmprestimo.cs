using ClubeLeitura.ConsoleApp.Dominio;
using System;

namespace ClubeLeitura.ConsoleApp.Controlador
{
    public class ControladorEmprestimo : ControladorBase
    {
        public string RegistrarEmprestimo(int id, string nomeAmigo, string colecao, DateTime dataEmprestimo, DateTime dataDevolucao)
        {
            Emprestimo emprestimo;

            int posicao;

            if (id == 0)
            {
                emprestimo = new Emprestimo();
                posicao = ObterPosicaoVaga();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Emprestimo(id));
                emprestimo = (Emprestimo)registros[posicao];
            }
            emprestimo.nomeAmigo = nomeAmigo;
            emprestimo.colecao = colecao;
            emprestimo.dataEmprestimo = dataEmprestimo;
            emprestimo.dataDevolucao = dataDevolucao;

            string resultadoValidacao = emprestimo.Validar();
             
            if (resultadoValidacao == "EMPRESTIMO_VALIDO")
                registros[posicao] = emprestimo;

            return resultadoValidacao;
        }

        public string RegistrarDevolucao(int id, int idRevistaEmprestimo, DateTime dataDevolucao)
        {
            Emprestimo emprestimo;

            int posicao;

            if (id == 0)
            {
                emprestimo = new Emprestimo();
                posicao = ObterPosicaoVaga();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Emprestimo(id));
                emprestimo = (Emprestimo)registros[posicao];
            }

            Revista revista = new Revista();
            revista.idRevistaEmprestimo = idRevistaEmprestimo;
            emprestimo.dataDevolucao = dataDevolucao;

            string resultadoValidacao = emprestimo.Validar();

            if (resultadoValidacao == "EMPRESTIMO_VALIDO")
                registros[posicao] = emprestimo;

            return resultadoValidacao;
        }

        public Emprestimo SelecionarEmprestimoPorId(int id)
        {
            return (Emprestimo)SelecionarRegistroPorId(new Emprestimo(id));
        }

        public bool ExcluirEmprestimo(int idSelecionado)
        {
            return ExcluirRegistro(new Emprestimo(idSelecionado));
        }

        public Emprestimo[] SelecionarTodosEmprestimo()
        {
            Emprestimo[] emprestimoAux = new Emprestimo[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), emprestimoAux, emprestimoAux.Length);

            return emprestimoAux;
        }
    }
}
