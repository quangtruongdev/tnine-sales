using System;

namespace tnine.Application.Shared.IDashboardService
{
    public class GetValueForDashboardDto
    {
        public DateTime? DateTime { get; set; }
        public decimal Value { get; set; }
    }
}
