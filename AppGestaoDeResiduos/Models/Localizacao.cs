using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGestaoDeResiduos.Models
{
    [Table("tb_localizacao")]
    public class Localizacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tb_localizacao")]
        public int LocalizacaoId { get; set; }

        [Column("longitude")]
        public int? Longitude { get; set; }

        [Column("latitude")]
        public int? Latitude { get; set; }

        [Column("data_hora")]
        public DateTime DataHora { get; set; }

        public ICollection<Caminhao>? Caminhoes { get; set; }
    }

}
