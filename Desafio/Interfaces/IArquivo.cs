namespace Desafio.Interfaces
{
    public interface IArquivo
    {
        public void RemoverArquivo(string caminhoArquivo);
        public void RenomearArquivo(string caminhoArquivo);
        public void CriarArquivoPeloTexto(string texto, string local);
    }
}
