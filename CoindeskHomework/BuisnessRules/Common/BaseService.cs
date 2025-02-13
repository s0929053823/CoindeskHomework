using CoindeskHomework.Data;

namespace CoindeskHomework.BuisnessRules.Common
{
    public abstract class BaseService
    {
        protected readonly ApplicationDbContext _context;

        protected BaseService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
    }
}
