using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IShop;
using tnine.Application.Shared.IShopService.Dto;
using tnine.Core;
using tnine.Core.Shared.Dtos;
using tnine.Core.Shared.Infrastructure;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShopService(
            IShopRepository shopRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _shopRepository = shopRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetShopForViewDto>> GetAll()
        {
            var shops = await _shopRepository.GetAllAsync();

            return shops.Select(s => new GetShopForViewDto
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                PhoneNumber = s.PhoneNumber,
            }).ToList();
        }
    
        public async Task CreateOrEdit(CreateOrEditShopDto input)
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

        private async Task Create(CreateOrEditShopDto input)
        {
            var shop = _mapper.Map<Shop>(input);
            await _shopRepository.InsertAsync(shop);
        }

        private async Task Edit(CreateOrEditShopDto input)
        {
            var shop = await _shopRepository.GetSingleByIdAsync(input.Id.Value);
            _mapper.Map(input, shop);
            await _shopRepository.UpdateAsync(shop);
        }

        public async Task<GetShopForEditOutputDto> GetById(EntityDto<long> input)
        {
            var shop = await _shopRepository.GetSingleByIdAsync(input.Id.Value);
            var output = new GetShopForEditOutputDto
            {
                Shop = _mapper.Map<CreateOrEditShopDto>(shop)
            };
            return output;
        }

        public async Task Delete(EntityDto<long> input)
        {
            await _shopRepository.DeleteAsync(input.Id.Value);
        }
    }
}