using System;
using ClubeLeitura.ConsoleApp.Dominio;

namespace ClubeLeitura.ConsoleApp.Controlador
{
    public class ControladorRevista : ControladorBase
    {
        public string RegistrarRevista(int id, int idCaixaRevista, DateTime ano, string colecao, int numeroEdicao)
        {
            Revista revista;

            int posicao;

            if (id == 0)
            {
                revista = new Revista();
                posicao = ObterPosicaoVaga();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Revista(id));
                revista = (Revista)registros[posicao];
            }

            revista.idCaixaRevista = idCaixaRevista;
            revista.ano = ano;
            revista.colecao = colecao;
            revista.numeroEdicao = numeroEdicao;

            string resultadoValidacao = revista.Validar();

            if (resultadoValidacao == "REVISTA_VALIDO")
                registros[posicao] = revista;

            return resultadoValidacao;
        }

        public Revista SelecionarRevistaPorId(int id)
        {
            return (Revista)SelecionarRegistroPorId(new Revista(id));
        }

        public bool ExcluirRevista(int idSelecionado)
        {
            return ExcluirRegistro(new Revista(idSelecionado));
        }

        public Revista[] SelecionarTodosRevistas()
        {
            Revista[] revistasAux = new Revista[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), revistasAux, revistasAux.Length);

            return revistasAux;
        }
    }
}
