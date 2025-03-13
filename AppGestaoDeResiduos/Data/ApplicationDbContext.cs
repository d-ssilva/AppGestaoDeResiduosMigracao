using Microsoft.EntityFrameworkCore;
using AppGestaoDeResiduos.Models;

namespace AppGestaoDeResiduos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Endereco>? Enderecos { get; set; }
        public DbSet<Coleta>? Coletas { get; set; }
        public DbSet<Caminhao>? Caminhoes { get; set; }
        public DbSet<Localizacao>? Localizacoes { get; set; }
        public DbSet<Notificacao>? Notificacoes { get; set; }
        public DbSet<UsuarioNotificacao>? UsuarioNotificacoes { get; set; }
        public DbSet<UsuarioColeta>? UsuarioColetas { get; set; }
    }
}
