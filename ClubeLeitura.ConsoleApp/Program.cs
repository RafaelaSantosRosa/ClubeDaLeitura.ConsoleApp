using System;
using ClubeLeitura.ConsoleApp.Telas;
using ClubeLeitura.ConsoleApp.Controlador;

namespace ClubeLeitura.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ControladorAmigo ctlrAmigo = new ControladorAmigo();

            ControladorCaixa ctrlCaixa = new ControladorCaixa();

            ControladorRevista ctlrRevista = new ControladorRevista();

            ControladorEmprestimo ctrlEmprestimo = new ControladorEmprestimo();

            TelaPrincipal telaPrincipal = new TelaPrincipal(ctrlCaixa, ctlrRevista, ctlrAmigo, ctrlEmprestimo);

            while (true)
            {
                ICadastravel telaSelecionada = telaPrincipal.ObterOpcao();

                if (telaSelecionada == null)
                    break;

                Console.Clear();

                if (telaSelecionada is TelaBase)
                    Console.WriteLine(((TelaBase)telaSelecionada).Titulo); Console.WriteLine();

                string opcao = telaSelecionada.ObterOpcao();

                if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (opcao == "1")
                    telaSelecionada.InserirNovoRegistro();

                else if (opcao == "2")
                {
                    telaSelecionada.VisualizarRegistros();
                    Console.ReadLine();
                }

                else if (opcao == "3")
                    telaSelecionada.EditarRegistro();

                else if (opcao == "4")
                    telaSelecionada.ExcluirRegistro();

                Console.Clear();
            }
        }


    }
    
}
