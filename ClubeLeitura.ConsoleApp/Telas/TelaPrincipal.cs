using ClubeLeitura.ConsoleApp.Controlador;
using System;


namespace ClubeLeitura.ConsoleApp.Telas
{
    class TelaPrincipal
    {
        private readonly ControladorAmigo controladorAmigo;
        private readonly ControladorCaixa controladorCaixa;
        private readonly ControladorRevista controladorRevista;
        private readonly ControladorEmprestimo controladorEmprestimo;

        public TelaPrincipal(ControladorCaixa ctlrCaixa, ControladorRevista ctlrRevista,
            ControladorAmigo ctlrAmigo, ControladorEmprestimo ctlrEmprestimo)
        {
            controladorCaixa = ctlrCaixa;
            controladorAmigo = ctlrAmigo;
            controladorRevista = ctlrRevista;
            controladorEmprestimo = ctlrEmprestimo;
        }

        public ICadastravel ObterOpcao()
        {
            TelaBase telaSelecionada = null;
            string opcao;
            do
            {
                Console.WriteLine("Digite 1 para o Cadastro de Caixas");
                Console.WriteLine("Digite 2 para o Cadastro de Revistas");
                Console.WriteLine("Digite 3 para o Cadastro de Amigos");
                Console.WriteLine("Digite 4 para o Controle de Empréstimos");

                Console.WriteLine("Digite S para Sair");

                opcao = Console.ReadLine();

                if (opcao == "1")
                    telaSelecionada = new TelaCaixa(controladorCaixa, controladorRevista);

                else if (opcao == "2")
                        telaSelecionada = new TelaRevista(controladorRevista, controladorCaixa);

                else if (opcao == "3")
                    telaSelecionada = new TelaAmigo(controladorAmigo);

                else if (opcao == "4")
                    telaSelecionada = new TelaEmprestimo(controladorEmprestimo, controladorAmigo, controladorCaixa, controladorRevista); 

                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    telaSelecionada = null;

            } while (OpcaoInvalida(opcao));

            return (ICadastravel)telaSelecionada;
        }

        #region Métodos Privados
        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "S" && opcao != "s")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opção inválida");
                Console.ResetColor();
                Console.ReadLine();
                return true;
            }
            else
                return false;
        }
        #endregion
    }
}
