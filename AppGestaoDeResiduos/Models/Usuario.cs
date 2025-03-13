using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGestaoDeResiduos.Models
{
    [Table("tb_usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("00")]
        public int UsuarioId { get; set; }

        
        [MaxLength(20)]
        [Column("nome")]
        public string? Nome { get; set; }

        
        [EmailAddress]
        [Column("email")]
        public string? Email { get; set; }

        
        [Column("agendou_coleta")]
        public bool? AgendouColeta { get; set; }
        
        [Column("foi_notificado")]
        public bool? FoiNotificado { get; set; }

        [Column("endereco_id")]
        public int? EnderecoId { get; set; }
        [ForeignKey("EnderecoId")]
        public Endereco? Endereco { get; set; }

        public ICollection<UsuarioNotificacao>? UsuarioNotificacoes { get; set; }
        public ICollection<UsuarioColeta>? UsuarioColetas { get; set; }
    }
}
