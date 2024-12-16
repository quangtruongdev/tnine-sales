using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IWarehouseReceiptService;
using tnine.Application.Shared.IWarehouseReceiptService.Dto;
using tnine.Core;
using tnine.Core.Shared.Dtos;
using tnine.Core.Shared.Infrastructure;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class WarehouseReceiptService : IWarehouseReceiptService
    {
        private readonly IWarehouseReceiptRepository _warehouseReceiptRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProductRepository _productRepository;
        private readonly IColorRepository _colorRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IProductWarehouseReceiptRepository _productWarehouseReceiptRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WarehouseReceiptService(
            IWarehouseReceiptRepository warehouseReceiptRepository,
            ISupplierRepository supplierRepository,
            IProductRepository productRepository,
            IColorRepository colorRepository,
            ISizeRepository sizeRepository,
            IProductWarehouseReceiptRepository productWarehouseReceiptRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _warehouseReceiptRepository = warehouseReceiptRepository;
            _supplierRepository = supplierRepository;
            _productRepository = productRepository;
            _colorRepository = colorRepository;
            _sizeRepository = sizeRepository;
            _productWarehouseReceiptRepository = productWarehouseReceiptRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateOrEdit(CreateOrEditWarehouseReceiptDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Edit(input);
            }
        }
        protected async Task Create(CreateOrEditWarehouseReceiptDto input)
        {
            var warehouseReceipt = new WarehouseReceipt() { CreationTime = DateTime.Now, SupplierId = input.SupplierId, IsDeleted = false };

            var warehouseReceiptId = await _warehouseReceiptRepository.InsertAndGetIdAsync(warehouseReceipt);
            foreach (var productInWarehouseReceipt in input.ProductInWarehouseReceipts)
            {
                foreach (var variationProduct in productInWarehouseReceipt.VariationProducts)
                {
                    var productWarehouseReceipt = new ProductWarehouseReceipt()
                    {
                        WarehouseReceiptId = warehouseReceiptId,
                        ProductId = productInWarehouseReceipt.ProductId,
                        ColorId = variationProduct.ColorId,
                        SizeId = variationProduct.SizeId,
                        Quantity = variationProduct.Quantity,
                        Price = productInWarehouseReceipt.Price
                    };
                    await _productWarehouseReceiptRepository.InsertAsync(productWarehouseReceipt);
                }
            }
        }
        protected async Task Edit(CreateOrEditWarehouseReceiptDto input)
        {

        }
        public async Task Delete(long Id)
        {
            var warehouseReceipt = await _warehouseReceiptRepository.FirstOrDefaultAsync(x => x.Id == Id);
            warehouseReceipt.IsDeleted = true;
            await _warehouseReceiptRepository.UpdateAsync(warehouseReceipt);
        }
        public async Task<PagedResultDto<GetWarehouseReceiptForViewDto>> GetAll()
        {
            var warehouseReceipts = await _warehouseReceiptRepository.GetAllAsync();
            var suppliers = await _supplierRepository.GetAllAsync();

            var query = from w in warehouseReceipts
                        join s in suppliers on w.SupplierId equals s.Id
                        select new GetWarehouseReceiptForViewDto
                        {
                            Id = w.Id,
                            SupplierName = s.Name,
                            CreationTime = (DateTime)w.CreationTime,
                            Total = w.Total,
                        };
            var totalCount = query.Count();
            var result = query.ToList();
            return new PagedResultDto<GetWarehouseReceiptForViewDto>(totalCount, result);
        }
        public async Task<List<GetWarehouseReceiptForEditDto>> GetWarehouseReceiptForEdit(long Id)
        {
            var warehouseReceipt = await _warehouseReceiptRepository.GetAllAsync();
            var productWarehouseReceipt = await _productWarehouseReceiptRepository.GetAllAsync();
            var supplier = await _supplierRepository.GetAllAsync();
            var color = await _colorRepository.GetAllAsync();
            var size = await _sizeRepository.GetAllAsync();
            var product = await _productRepository.GetAllAsync();
            var query = from w in warehouseReceipt
                        join s in supplier on w.SupplierId equals s.Id
                        join pwr in productWarehouseReceipt on w.Id equals pwr.WarehouseReceiptId
                        join c in color on pwr.ColorId equals c.Id
                        join si in size on pwr.SizeId equals si.Id
                        join p in product on pwr.ProductId equals p.Id
                        where w.Id == Id && w.IsDeleted == false
                        select new GetWarehouseReceiptForEditDto
                        {
                            ProductName = p.Name,
                            ColorName = c.Code,
                            SizeName = si.Name,
                            Quantity = pwr.Quantity,
                            Price = pwr.Price
                        };
            return query.ToList();
        }
        public async Task<PagedResultDto<GetWarehouseReceiptForViewDto>> GetWarehouseReceiptBySupplierId(long SupplierId)
        {
            var warehouseReceipts = await _warehouseReceiptRepository.GetAllAsync();
            var suppliers = await _supplierRepository.GetAllAsync();

            var query = from w in warehouseReceipts
                        join s in suppliers on w.SupplierId equals s.Id
                        where w.SupplierId == SupplierId && w.IsDeleted == false
                        select new GetWarehouseReceiptForViewDto
                        {
                            Id = w.Id,
                            SupplierName = s.Name,
                            CreationTime = (DateTime)w.CreationTime,
                            Total = w.Total,
                        };
            var totalCount = query.Count();
            var result = query.ToList();
            return new PagedResultDto<GetWarehouseReceiptForViewDto>(totalCount, result);
        }
        public async Task<GetWarehouseReceiptForViewDto> GetDetailWarehouseReceipt(long id)
        {
            var warehouseReceipt = await _warehouseReceiptRepository.GetAllAsync();
            var supplier = await _supplierRepository.GetAllAsync();

            var query = from w in warehouseReceipt
                        join s in supplier on w.SupplierId equals s.Id
                        where w.Id == id && w.IsDeleted == false
                        select new GetWarehouseReceiptForViewDto
                        {
                            SupplierName = s.Name,
                            CreationTime = (DateTime)w.CreationTime,
                            Total = w.Total,
                        };
            var result = query.FirstOrDefault();
            return result;
        }

        public async Task<decimal> GetTotalWarehouseReceipt(long id)
        {
            var warehouseReceipt = await _warehouseReceiptRepository.FirstOrDefaultAsync(x => x.Id == id);
            return warehouseReceipt.Total;
        }

        public async Task<string> GetSupplier(long id)
        {
            var warehouseReceipt = await _warehouseReceiptRepository.FirstOrDefaultAsync(x => x.Id == id);
            var supplier = await _supplierRepository.FirstOrDefaultAsync(x => x.Id == warehouseReceipt.SupplierId);
            return supplier.Name;
        }
    }
}
