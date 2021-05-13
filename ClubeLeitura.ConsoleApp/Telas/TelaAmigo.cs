using System;
using ClubeLeitura.ConsoleApp.Controlador;
using ClubeLeitura.ConsoleApp.Dominio;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaAmigo : TelaBase, ICadastravel
    {
        private ControladorAmigo controladorAmigo;
        public TelaAmigo(ControladorAmigo controlador)
            : base("Cadastro de Amigos")
        {
            controladorAmigo = controlador;
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo amigo...");

            bool conseguiuGravar = GravarAmigo(0);

            if (conseguiuGravar)
                ApresentarMensagem("Amigo inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir amigo", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando cadastro de um amigo...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do cadastro de um amigo que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = GravarAmigo(id);

            if (conseguiuGravar)
                ApresentarMensagem("Amigo editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o amigo", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um cadastro de amigo...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do cadastro de amigo que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = controladorAmigo.ExcluirAmigo(idSelecionado);

            if (conseguiuExcluir)
                ApresentarMensagem("Amigo excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o amigo", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando amigos...");

            string configuracaColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            Amigo[] amigos = controladorAmigo.SelecionarTodosAmigos();

            if (amigos.Length == 0)
            {
                ApresentarMensagem("Nenhum amigo cadastrado!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < amigos.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   amigos[i].id, amigos[i].nome, amigos[i].nomeResponsavel);
            }
        }

        public string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para cadastrar um novo amigo");
            Console.WriteLine("Digite 2 para visualizar cadastro de amigos");
            Console.WriteLine("Digite 3 para editar um cadastro de amigos");
            Console.WriteLine("Digite 4 para excluir um cadastro de amigos");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        #region Métodos Privados
        private bool GravarAmigo(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            Console.Write("Digite o nome do amiguinho: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o número de telefone: ");
            int telefone = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite de onde é o amiguinho: ");
            string lugar = Console.ReadLine();

            resultadoValidacao = controladorAmigo.RegistrarAmigo(
                id, nome, nomeResponsavel, telefone, lugar);

            if (resultadoValidacao != "AMIGO_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }

        private static void MontarCabecalhoTabela(string configuracaoColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(configuracaoColunasTabela, "Id", "Nome", "Nome Responsável");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }
        #endregion
    }
}
