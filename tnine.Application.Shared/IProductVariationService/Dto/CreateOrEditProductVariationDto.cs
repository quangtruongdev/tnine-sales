﻿namespace tnine.Application.Shared.IProductVariationService.Dto
{
    public class CreateOrEditProductVariaionDto
    {
        public long ProductId { get; set; }
        public long ColorId { get; set; }
        public long SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
