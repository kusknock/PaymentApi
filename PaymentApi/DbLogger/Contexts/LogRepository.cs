using Microsoft.EntityFrameworkCore;
using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.DbLogger
{
    /// <summary>
    /// Реализация с помощью DbContext
    /// </summary>
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Log> _dbSet;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public LogRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<Log>();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="count"><inheritdoc/></param>
        /// <param name="desc"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IEnumerable<Log> Get(int count, bool desc)
        {
            return _dbSet.OrderByDescending(i => i.Id).Take(count).ToList();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public IEnumerable<Log> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="item"><inheritdoc/></param>
        public Log Log(Log item)
        {
            var addedItem = _dbSet.Add(item);
            _context.SaveChanges();

            return addedItem.Entity;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="log"><inheritdoc/></param>
        public void Remove(Log log)
        {
            _dbSet.Remove(log);
            _context.SaveChanges();
        }
    }
}
