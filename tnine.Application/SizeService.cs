using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.ISizeService;
using tnine.Application.Shared.ISizeService.Dto;
using tnine.Core;
using tnine.Core.Shared.Infrastructure;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SizeService(ISizeRepository sizeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetSizeForViewDto>> GetAll()
        {
            var sizes = await _sizeRepository.GetAllAsync();
            return sizes.Select(x => new GetSizeForViewDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
        }
        public async Task CreateOrEdit(CreateOrEditSizeDto input)
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
        private async Task Create(CreateOrEditSizeDto input)
        {
            var size = _mapper.Map<Sizes>(input);
            await _sizeRepository.InsertAsync(size);
        }
        private async Task Edit(CreateOrEditSizeDto input)
        {
            var size = await _sizeRepository.GetSingleByIdAsync(input.Id.Value);
            _mapper.Map(input, size);
        }
        public async Task<GetSizeForEditOutputDto> GetById(long id)
        {
            var size = await _sizeRepository.GetSingleByIdAsync(id);
            return new GetSizeForEditOutputDto
            {
                Id = size.Id,
                Name = size.Name,
                Description = size.Description,
            };
        }
        public async Task Delete(long id)
        {
            await _sizeRepository.DeleteAsync(id);
        }
    }
}
