using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentApi.DbLogger;

namespace PaymentApi.Models
{
    /// <summary>
    /// Контекст для работы с базой данных
    /// </summary>
    public class ApplicationContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Белый список IP
        /// </summary>
        public DbSet<IpAddress> IpAddresses { get; set; } = null!;

        /// <summary>
        /// Логи
        /// </summary>
        public DbSet<Log> Logs { get; set; } = null!;

        /// <summary>
        /// Конструктор+
        /// </summary>
        /// <param name="options">Параметры для создания объекта контекста</param>
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="modelBuilder"><inheritdoc/></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
