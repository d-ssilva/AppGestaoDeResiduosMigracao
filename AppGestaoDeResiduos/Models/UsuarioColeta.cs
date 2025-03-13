using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGestaoDeResiduos.Models
{
    [Table("tb_usuario_coleta")]
    public class UsuarioColeta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("usuario_coleta_id")]
        public int UsuarioColetaId { get; set; }

        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Required]
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }

        [Required]
        [Column("coleta_id")]
        public int ColetaId { get; set; }
        [ForeignKey("ColetaId")]
        public Coleta? Coleta { get; set; }
    }

}
