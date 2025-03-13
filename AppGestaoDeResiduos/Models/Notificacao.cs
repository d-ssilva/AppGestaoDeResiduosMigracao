using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGestaoDeResiduos.Models
{
    [Table("tb_notificacao")]
    public class Notificacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("notificacao_id")]
        public int? NotificacaoId { get; set; }

        [MaxLength(250)]
        [Column("mensagem")]
        public string? Mensagem { get; set; }

        public ICollection<UsuarioNotificacao>? UsuarioNotificacoes { get; set; }
    }

}
