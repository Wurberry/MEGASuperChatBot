using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MEGASuperChatBot
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : 
        base(options)
        {}
        public DbSet<CommandEntity> CommandEntities{ get; set; }
    }
}
