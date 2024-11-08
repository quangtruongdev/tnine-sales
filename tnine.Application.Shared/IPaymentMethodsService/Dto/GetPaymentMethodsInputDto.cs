using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IPaymentMethodsService.Dto
{
    public class GetPaymentMethodsInputDto : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
    }
}
