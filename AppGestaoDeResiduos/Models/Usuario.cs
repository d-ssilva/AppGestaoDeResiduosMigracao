namespace AppGestaoDeResiduos.Models
{
    public class Usuario
    {
        public Usuario()
        {
            Coletas = new List<Coleta>();
            Notificacoes = new List<Notificacao>();
        }

        public string? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public bool? AgendouColeta { get; set; }
        public bool? FoiNotificado { get; set; }

        // Subdocumento de Endereco
        public EnderecoUsuario Endereco { get; set; }

        public List<Coleta> Coletas { get; set; } // Array de subdocumentos
        public List<Notificacao> Notificacoes { get; set; } // Array de subdocumentos
    }

    // Subdocumento para Endereco
    public class EnderecoUsuario
    {
        public string? Cep { get; set; }
        public string? Estado { get; set; }
        public string? Cidade { get; set; }
        public string? Rua { get; set; }
        public int? Numero { get; set; }
    }
}