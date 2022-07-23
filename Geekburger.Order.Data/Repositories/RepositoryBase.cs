using Geekburger.Order.Database;

namespace Geekburger.Order.Data.Repositories
{
    public class RepositoryBase
    {
        protected readonly OrderDbContext _ctx;

        public RepositoryBase(OrderDbContext ctx)
        {
            _ctx = ctx;
        }
    }
}