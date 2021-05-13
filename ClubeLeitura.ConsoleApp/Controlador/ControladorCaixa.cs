using ClubeLeitura.ConsoleApp.Dominio;
using System;


namespace ClubeLeitura.ConsoleApp.Controlador
{
   public class ControladorCaixa : ControladorBase
    {
        public string RegistrarCaixa(int id, string etiqueta, string cor)
        {
            Caixa caixa;

            int posicao;

            if (id == 0)
            {
                caixa = new Caixa();
                posicao = ObterPosicaoVaga();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Caixa(id));
                caixa = (Caixa)registros[posicao];
            }

            caixa.etiqueta = etiqueta;
            caixa.cor = cor;


            string resultadoValidacao = caixa.Validar();

            if (resultadoValidacao == "CAIXA_VALIDO")
                registros[posicao] = caixa;

            return resultadoValidacao;
        }

        public Caixa SelecionarCaixaPorId(int id)
        {
            return (Caixa)SelecionarRegistroPorId(new Caixa(id));
        }

        public bool ExcluirCaixa(int idSelecionado)
        {
            return ExcluirRegistro(new Caixa(idSelecionado));
        }

        public Caixa[] SelecionarTodosCaixas()
        {
            Caixa[] caixasAux = new Caixa[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), caixasAux, caixasAux.Length);

            return caixasAux;
        }
    }
}
