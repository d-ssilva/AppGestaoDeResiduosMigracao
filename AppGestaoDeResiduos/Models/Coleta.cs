namespace AppGestaoDeResiduos.Models
{
    public class Coleta
    {
        public string? Id { get; set; }
        public int? QtdDeColeta { get; set; }
        public DateTime DataColeta { get; set; }

        // Subdocumento de Endereco
        public EnderecoColeta? Endereco { get; set; }

        public string? CaminhaoId { get; set; } // Referência ao caminhão
    }

    // Subdocumento para Endereco
    public class EnderecoColeta
    {
        public string? Cep { get; set; }
        public string? Estado { get; set; }
        public string? Cidade { get; set; }
        public string? Rua { get; set; }
        public int? Numero { get; set; }
    }
}