using FDX.DataAccess.Context.Interfaces;
using FDX.DataAccess.Models;
using FDX.DataAccess.Repositories.Interfaces;

namespace FDX.DataAccess.Repositories
{
    public class SmsRepository : Repository<Sms>, ISmsRepository
    {
        private readonly IMessagesDbContext _context;
        public SmsRepository(IMessagesDbContext context) : base(context)
        {
            _context = context;
        }
    }
}