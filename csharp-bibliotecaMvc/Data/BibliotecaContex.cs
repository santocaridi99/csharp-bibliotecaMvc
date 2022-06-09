using csharp_bibliotecaMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace csharp_bibliotecaMvc.Data
{
    public class BibliotecaContex : DbContext
    {
        public BibliotecaContex(DbContextOptions<BibliotecaContex> options) : base(options)
        { 
        }
        public DbSet<Libro> Libri { get; set; }
        public DbSet<Prestito> Prestiti { get; set; }
        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Autore> Autori { get; set; }

        /*
         * Di default i nomi delle tabelle sono creati al plurale. Questo metodo 
         * serve per specificare dei nomi diversi, in questo caso al singolare.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Libro>().ToTable("Libri");
            modelBuilder.Entity<Prestito>().ToTable("Prestiti");
            modelBuilder.Entity<Utente>().ToTable("Utenti");
            modelBuilder.Entity<Autore>().ToTable("Autori");
        }
    }
}

