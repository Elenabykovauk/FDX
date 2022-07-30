using FDX.DataAccess.Context.Interfaces;
using FDX.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace FDX.DataAccess.Context
{
    public class MessagesDbContext : DbContext, IMessagesDbContext
    {
        public MessagesDbContext()
        {
        }

        public MessagesDbContext(DbContextOptions<MessagesDbContext> options) : base(options)
        {

        }
        
        public DbSet<Sms> Sms { get; set; }
    }
}
