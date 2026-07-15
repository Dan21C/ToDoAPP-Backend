// Importamos EF Core para poder usar DbContext y DbSet
using Microsoft.EntityFrameworkCore;
// Importamos la entidad Todo para representarla como tabla
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Infrastructure.Data;

// AppDbContext es la conexión a la base de datos
// Hereda de DbContext que es la clase base de EF Core
// Piénsalo como el "puente" entre tu código C# y SQL Server
public class AppDbContext : DbContext
{
    // El constructor recibe las opciones de configuración (connection string, proveedor, etc.)
    // Esas opciones vienen de Program.cs via Inyección de Dependencias
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSet<Todo> representa la tabla "Todos" en la base de datos
    // Con esto puedes hacer _context.Todos.ToList() y EF Core hace SELECT * FROM Todos
    public DbSet<Todo> Todos { get; set; }

    // OnModelCreating es donde le dices a EF Core cómo configurar las tablas
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(entity =>
        {
            // Definimos que Id es la llave primaria
            entity.HasKey(e => e.Id);

            // Title es obligatorio y máximo 200 caracteres
            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            // CreatedAt toma la fecha actual de SQL Server por defecto
            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("GETUTCDATE()");
        });
    }
}