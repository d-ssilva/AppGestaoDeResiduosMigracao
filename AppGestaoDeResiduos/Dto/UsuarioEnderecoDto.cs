namespace AppGestaoDeResiduos.DTOs
{
    public class UsuarioEnderecoDTO
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public bool? AgendouColeta { get; set; }
        public bool? FoiNotificado { get; set; }

        public int? Cep { get; set; }
        public string? Estado { get; set; }
        public string? Cidade { get; set; }
        public string? Rua { get; set; }
        public int? Numero { get; set; }
    }
}
