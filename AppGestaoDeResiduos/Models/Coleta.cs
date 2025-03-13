using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGestaoDeResiduos.Models
{
    [Table("tb_coleta")]
    public class Coleta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("coleta_id")]
        public int ColetaId { get; set; }

        [Column("qtd_de_coletas")]
        public int? QtdDeColeta { get; set; }

        [Column("data_coleta")]
        public DateTime? DataColeta { get; set; }

        [Column("endereco_id")]
        public int? EnderecoId { get; set; }
        [ForeignKey("EnderecoId")]
        public Endereco? Endereco { get; set; }

        [Column("caminhao_id")]
        public int CaminhaoId { get; set; }
        [ForeignKey("CaminhaoId")]
        public Caminhao? Caminhao { get; set; }

        public ICollection<UsuarioColeta>? UsuarioColetas { get; set; }
    }

}
