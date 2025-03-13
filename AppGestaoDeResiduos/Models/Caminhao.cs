using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGestaoDeResiduos.Models
{
    [Table("tb_caminhao")]
    public class Caminhao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("caminhao_id")]
        public int CaminhaoId { get; set; }

        [MaxLength(7)]
        [Column("placa")]
        public string? Placa { get; set; }

        [Column("qtd_de_coletas")]
        public int? QtdDeColetas { get; set; }

        [Column("qtd_de_coletas_max")]
        public int? QtdDeColetasMax { get; set; }

        [Column("localizacao_id")]
        public int? LocalizacaoId { get; set; }
        [ForeignKey("LocalizacaoId")]
        public Localizacao? Localizacao { get; set; }

        public ICollection<Coleta>? Coletas { get; set; }
    }
}
