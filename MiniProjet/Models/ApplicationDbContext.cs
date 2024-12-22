using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MiniProjet.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets pour chaque entité
        public DbSet<Client> Clients { get; set; }
        public DbSet<ReclamationModel> Reclamations { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<Technicien> Techniciens { get; set; }
        public DbSet<PieceDeRechange> PiecesDeRechange { get; set; }

        // Configuration des relations entre entités
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration de la relation entre Client et Reclamation
            modelBuilder.Entity<ReclamationModel>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Reclamations)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration de la relation entre Reclamation et Intervention
            modelBuilder.Entity<Intervention>()
                .HasOne(i => i.Reclamation)
                .WithMany(r => r.Interventions)
                .HasForeignKey(i => i.ReclamationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration de la relation entre Technicien et Intervention
            modelBuilder.Entity<Intervention>()
                .HasOne(i => i.Technicien)
                .WithMany(t => t.Interventions)
                .HasForeignKey(i => i.TechnicienId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configuration de la relation entre Article et Intervention
            modelBuilder.Entity<Article>()
                .HasMany(a => a.Interventions)
                .WithOne(i => i.Article)
                .HasForeignKey(i => i.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration de la relation entre PieceDeRechange et Article
            modelBuilder.Entity<PieceDeRechange>()
                .HasOne(p => p.Article)
                .WithMany(a => a.PiecesDeRechange)
                .HasForeignKey(p => p.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration de la relation entre Intervention et PieceDeRechange
            modelBuilder.Entity<Intervention>()
                .HasMany(i => i.PiecesDeRechange)
                .WithMany(p => p.Interventions)
                .UsingEntity(j => j.ToTable("InterventionPieces"));

            // Configuration de la précision pour les propriétés décimales
            modelBuilder.Entity<Intervention>()
                .Property(i => i.TotalCost)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PieceDeRechange>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
