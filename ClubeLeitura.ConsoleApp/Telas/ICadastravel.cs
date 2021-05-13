
namespace ClubeLeitura.ConsoleApp.Telas
{
    public interface ICadastravel
    {
        void InserirNovoRegistro();

        void EditarRegistro();

        void ExcluirRegistro();

        void VisualizarRegistros();

        string ObterOpcao();
    }
}
