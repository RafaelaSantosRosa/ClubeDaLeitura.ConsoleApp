using ClubeLeitura.ConsoleApp.Controlador;
using ClubeLeitura.ConsoleApp.Dominio;
using System;


namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaCaixa : TelaBase, ICadastravel
    {
        private ControladorCaixa controladorCaixa;

        private ControladorRevista controladorRevista;
        public TelaCaixa(ControladorCaixa ctlrCaixa, ControladorRevista ctlrRevista)
            : base ("Cadastro de Caixa")
        {
            controladorRevista = ctlrRevista;
            controladorCaixa = ctlrCaixa;
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando uma caixa...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número da caixa que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = GravarCaixa(id);

            if (conseguiuGravar)
                ApresentarMensagem("Caixa editada com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar caixa", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo uma caixa...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número da caixa que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = controladorCaixa.ExcluirCaixa(idSelecionado);

            if (conseguiuExcluir)
                ApresentarMensagem("Caixa excluída com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir caixa", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo uma caixa...");

            bool conseguiuGravar = GravarCaixa(0);

            if (conseguiuGravar)
                ApresentarMensagem("Caixa inserida com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir caixa", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }
        public void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando caixas...");

            string configuracaColunasTabela = "{0,-10} | {1,-55} | {2,-35}| {3,-35}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            Caixa[] caixas = controladorCaixa.SelecionarTodosCaixas();
            Revista[] revistas = controladorRevista.SelecionarTodosRevistas();

            if (caixas.Length == 0)
            {
                ApresentarMensagem("Nenhuma caixa cadastrada!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < caixas.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   caixas[i].id, caixas[i].etiqueta, caixas[i].cor, revistas[i].colecao);
            }
        }
        public string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para cadastrar uma nova caixa");
            Console.WriteLine("Digite 2 para visualizar cadastro de caixas");
            Console.WriteLine("Digite 3 para editar um cadastro de caixas");
            Console.WriteLine("Digite 4 para excluir um cadastro de caixas");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }
        #region Métodos Privados
        private bool GravarCaixa(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            VisualizarRevistas();

            Console.Write("Digite a cor da caixa: ");
            string cor = Console.ReadLine();

            Console.Write("Digite a etiqueta da caixa: ");
            string etiqueta = Console.ReadLine();

            resultadoValidacao = controladorCaixa.RegistrarCaixa(
                id, etiqueta, cor);

            if (resultadoValidacao != "CAIXA_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }

        private static void MontarCabecalhoTabela(string configuracaoColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(configuracaoColunasTabela, "Id", "Etiqueta", "Cor", "Revista");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }
        private void VisualizarRevistas()
        {
            Console.WriteLine();
            Revista[] revistas = controladorRevista.SelecionarTodosRevistas();

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-30}", "Id", "Revista");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();

            if (revistas.Length == 0)
            {
                ApresentarMensagem("Nenhuma revista nesta caixa ainda!", TipoMensagem.Atencao);
                return;
            }

            foreach (var r in revistas)
            {
                Console.WriteLine("{0,-10} | {1,-30}", r.id, r.colecao);
            }
            Console.WriteLine();
        }
        #endregion
    }
}
