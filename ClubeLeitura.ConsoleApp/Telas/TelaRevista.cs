using ClubeLeitura.ConsoleApp.Controlador;
using ClubeLeitura.ConsoleApp.Dominio;
using System;


namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaRevista : TelaBase, ICadastravel
    {
        private ControladorRevista controladorRevista;

        private ControladorCaixa controladorCaixa;

        public TelaRevista(ControladorRevista ctlrRevista, ControladorCaixa ctlrCaixa)
            : base("Cadastro de Revistas")
        {
            controladorRevista= ctlrRevista;
            controladorCaixa = ctlrCaixa;
        }
        public void EditarRegistro()
        {
            ConfigurarTela("Editando cadastro de uma revista...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do cadastro de uma revista que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = GravarRevista(id);

            if (conseguiuGravar)
                ApresentarMensagem("Revista editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o revista", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um cadastro de revista...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do cadastro de revista que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = controladorRevista.ExcluirRevista(idSelecionado);

            if (conseguiuExcluir)
                ApresentarMensagem("Revista excluída com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir revista", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo uma nova revista...");

            bool conseguiuGravar = GravarRevista(0);

            if (conseguiuGravar)
                ApresentarMensagem("Revista inserida com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir revista", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para cadastrar uma nova revista");
            Console.WriteLine("Digite 2 para visualizar cadastro de revistas");
            Console.WriteLine("Digite 3 para editar um cadastro de revistas");
            Console.WriteLine("Digite 4 para excluir um cadastro de revistas");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando revistas...");

            string configuracaColunasTabela = "{0,-10} | {1,-35} | {2,-35} | {3,-35}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            Revista[] revistas = controladorRevista.SelecionarTodosRevistas();
            Caixa[] caixas = controladorCaixa.SelecionarTodosCaixas();

            if (revistas.Length == 0)
            {
                ApresentarMensagem("Nenhuma revista cadastrada!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < revistas.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   revistas[i].id, revistas[i].ano, revistas[i].colecao, caixas[i].etiqueta);
            }
        }

        #region Métodos Privados
        private bool GravarRevista(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            VisualizarCaixas();

            Console.Write("Digite o id da caixa onde a revista está guardada: ");
            int idCaixaRevista = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o ano da revista: ");
            DateTime ano = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite o nome da coleção: ");
            string colecao = Console.ReadLine();

            Console.Write("Digite o número de edição da revista: ");
            int numeroEdicao = Convert.ToInt32(Console.ReadLine());

            resultadoValidacao = controladorRevista.RegistrarRevista(
                id, idCaixaRevista, ano, colecao, numeroEdicao);

            if (resultadoValidacao != "REVISTA_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }

        private static void MontarCabecalhoTabela(string configuracaoColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(configuracaoColunasTabela, "Id", "Ano", "Coleção", "Caixa");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private void VisualizarCaixas()
        {
            Console.WriteLine();
            Caixa[] caixas = controladorCaixa.SelecionarTodosCaixas();

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-30}| {2,-30}", "Id", "Cor da Caixa", "Etiqueta");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();

            if (caixas.Length == 0)
            {
                ApresentarMensagem("Nenhuma caixa cadastrada!", TipoMensagem.Atencao);
                
                return;
            }

            foreach (var c in caixas)
            {
                Console.WriteLine("{0,-10} | {1,-30}| {2,-30}", c.id, c.cor, c.etiqueta);
            }
            Console.WriteLine();
        }

#endregion
    }
}
