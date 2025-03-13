using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGestaoDeResiduos.Models
{
    [Table("tb_endereco")]
    public class Endereco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("endereco_id")]
        public int EnderecoId { get; set; }

        [MaxLength(8)]
        [Column("cep")]
        public int? Cep { get; set; }

        [MaxLength(2)]
        [Column("estado")]
        public string? Estado { get; set; }

        [MaxLength(50)]
        [Column("cidade")]
        public string? Cidade { get; set; }

        [MaxLength(100)]
        [Column("rua")]
        public string? Rua { get; set; }

        [Column("numero")]
        public int? Numero { get; set; }

        public ICollection<Usuario>? Usuarios { get; set; }
    }

}
