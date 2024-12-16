using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.ISupplierService;
using tnine.Application.Shared.ISupplierService.Dto;
using tnine.Core;
using tnine.Core.Shared.Infrastructure;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SupplierService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetSupplierForViewDto>> GetAll()
        {
            var suppliers = await _supplierRepository.GetAllAsync();
            var filteredSuppliers = suppliers.Where(x => x.IsDeleted == false);
            return filteredSuppliers.Select(x => new GetSupplierForViewDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                PhoneNumber = x.PhoneNumber
            }).ToList();
        }
        public async Task CreateOrEdit(CreateOrEditSupplierDto input)
        {
            if (input.Id.HasValue)
            {
                await Edit(input);
            }
            else
            {
                await Create(input);
            }
        }
        private async Task Create(CreateOrEditSupplierDto input)
        {
            var supplier = _mapper.Map<Suppliers>(input);
            supplier.IsDeleted = false;
            await _supplierRepository.InsertAsync(supplier);
        }
        private async Task Edit(CreateOrEditSupplierDto input)
        {
            var supplier = await _supplierRepository.GetSingleByIdAsync(input.Id.Value);
            _mapper.Map(input, supplier);
            await _supplierRepository.UpdateAsync(supplier);
        }
        public async Task<GetSupplierForEditOutputDto> GetById(long id)
        {
            var supplier = await _supplierRepository.GetSingleByIdAsync(id);
            return new GetSupplierForEditOutputDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Address = supplier.Address,
                PhoneNumber = supplier.PhoneNumber
            };
        }
        public async Task Delete(long id)
        {
            var supplier = await _supplierRepository.GetSingleByIdAsync(id);
            supplier.IsDeleted = true;
            await _supplierRepository.UpdateAsync(supplier);
        }
    }
}
