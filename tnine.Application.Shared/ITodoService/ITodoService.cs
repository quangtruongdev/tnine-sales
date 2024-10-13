using System.Collections.Generic;
using tnine.Core;

namespace tnine.Application.Shared.ITodo
{
    public interface ITodoService
    {
        IEnumerable<Todo> GetAll();
    }
}
