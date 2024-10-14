using System.Collections.Generic;
using tnine.Core;

namespace tnine.Application.Shared.IProductService
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
    }
}
