using tnine.Core.Shared.Infrastructure;

namespace tnine.Core.Shared.Repositories
{
    public interface ITodoRepository : IRepository<Todo, long>
    {
    }

    public class TodoRepository : Repository<Todo, long>, ITodoRepository
    {
        public TodoRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
