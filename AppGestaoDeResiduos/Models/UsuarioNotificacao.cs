using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGestaoDeResiduos.Models
{
    [Table("tb_usuario_notificacao")]
    public class UsuarioNotificacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("usuario_notificacao_id")]
        public int UsuarioNotificacaoId { get; set; }

        [Column("data_notificacao")]
        public DateTime DataNotificacao { get; set; }

        [Column("notificacao_id")]
        public int NotificacaoId { get; set; }
        [ForeignKey("NotificacaoId")]
        public Notificacao? Notificacao { get; set; }

        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }        
    }

}
