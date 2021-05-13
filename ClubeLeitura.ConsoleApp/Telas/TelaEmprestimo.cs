using ClubeLeitura.ConsoleApp.Controlador;
using ClubeLeitura.ConsoleApp.Dominio;
using System;


namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaEmprestimo : TelaBase, ICadastravel 
    {
        private ControladorCaixa controladorCaixa;

        private ControladorRevista controladorRevista;

        private ControladorAmigo controladorAmigo;

        private ControladorEmprestimo controladorEmprestimo;
        public TelaEmprestimo(ControladorEmprestimo ctlrEmprestimo, ControladorAmigo ctlrAmigo, ControladorCaixa ctlrCaixa,
            ControladorRevista ctlrRevista) : base("Controle de Empréstimos")
        {
            controladorEmprestimo = ctlrEmprestimo;
            controladorAmigo = ctlrAmigo;
            controladorRevista = ctlrRevista;
            controladorCaixa = ctlrCaixa;
        }
        public void InserirNovoRegistro()
        {
            ConfigurarTela("Cadastrando um novo empréstimo...");

            bool conseguiuGravar = GravarEmprestimo(0);

            if (conseguiuGravar)
                ApresentarMensagem("Empréstimo cadastrado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar cadastrar empréstimo", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para realizar empréstimo");
            Console.WriteLine("Digite 2 para vizualizar empréstimos abertos no dia");
            Console.WriteLine("Digite 3 para registrar devolução");
        //    Console.WriteLine("Digite 4 para vizualizar empréstimos fechados"); não consegui fazer

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

           
            return opcao;
        }

        public void EditarRegistro()// registro de devolução
        {
            ConfigurarTela("Registrando Devolução...");

            bool conseguiuRegistrar = GravaDevolução(0);

            if (conseguiuRegistrar)
                ApresentarMensagem("Devolução registrada com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar registrar devolução", TipoMensagem.Erro);
                EditarRegistro();
            }

        }

        public void ExcluirRegistro()
        {
            throw new NotImplementedException();
        }

        public void VisualizarRegistros() //visualizando emprestimos no geral
        {
            ConfigurarTela("Visualizando Empréstimos abertos...");

            string configuracaColunasTabela = "{0,-10} | {1,-35} | {2,-35}| {3,-35}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            Emprestimo[] emprestimos = controladorEmprestimo.SelecionarTodosEmprestimo();

            if (emprestimos.Length == 0)
            {
                ApresentarMensagem("Nenhum empréstimo cadastrado!", TipoMensagem.Atencao);
                return;
            }
  
            for (int i = 0; i < emprestimos.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   emprestimos[i].id, emprestimos[i].dataDevolucao, emprestimos[i].dataEmprestimo, emprestimos[i].nomeAmigo);
            }
        }

        #region Métodos privados

        private static void MontarCabecalhoTabela(string configuracaoColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(configuracaoColunasTabela, "Id", "Devolução", "Data Empréstimo", "Amiguinho");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private void VisualizarRevistas()
        {
            Console.WriteLine();
            Revista[] revistas = controladorRevista.SelecionarTodosRevistas();
            Caixa[] caixas = controladorCaixa.SelecionarTodosCaixas();

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10}  | {1,-30}", "Id", "Revista");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();

            if (revistas.Length == 0)
            {
                ApresentarMensagem("Nenhuma revista disponível!", TipoMensagem.Atencao);
                return;
            }

            foreach (var r in revistas)
            {
                Console.WriteLine("{0,-10} | {1,-30}", r.id, r.colecao);
            }
            Console.WriteLine();
        }

        public bool GravarEmprestimo(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            VisualizarRevistas();

            Console.Write("Digite nome do amiguinho que pegou a revista: ");
            string nomeAmigo = Console.ReadLine();

            Console.Write("Digite a coleção da revista: ");
            string colecao = Console.ReadLine();

            Console.Write("Digite a data do empréstimo: ");
            DateTime dataEmprestimo = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite a data de devolução: ");
            DateTime dataDevolucao = Convert.ToDateTime(Console.ReadLine());

            resultadoValidacao = controladorEmprestimo.RegistrarEmprestimo(
                id, nomeAmigo, colecao, dataEmprestimo, dataDevolucao);

            if (resultadoValidacao != "EMPRESTIMO_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }

        private bool GravaDevolução(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            VisualizarRevistas();

            Console.Write("Digite o id da revista que será devolvida: ");
            int idRevistaEmprestimo = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite a data de devolução: ");
            DateTime dataDevolucao = Convert.ToDateTime(Console.ReadLine());

            resultadoValidacao = controladorEmprestimo.RegistrarDevolucao(
                id, idRevistaEmprestimo, dataDevolucao);

            if (resultadoValidacao != "EMPRESTIMO_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }
        #endregion
    }
}
