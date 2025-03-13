namespace AppGestaoDeResiduos.Dto
{
    public class NotificarUsuarioRequest
    {
        public int UsuarioId { get; set; }
        public required string Mensagem { get; set; }
    }
}
