using System;

namespace tnine.Core.Shared.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        DatabaseContext Init();
    }
}
