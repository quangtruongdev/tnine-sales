using System;

namespace tnine.Core.Shared.Infrastructure
{
    public class DbFactory : IDbFactory, IDisposable
    {
        private DatabaseContext dbContext;

        public DatabaseContext Init()
        {
            return dbContext ?? (dbContext = new DatabaseContext());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
