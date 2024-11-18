using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IColorService;
using tnine.Core;
using tnine.Core.Shared.IColorService.Dto;
using tnine.Core.Shared.Infrastructure;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ColorService(IColorRepository colorRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetColorForViewDto>> GetAll()
        {
            var colors = await _colorRepository.GetAllAsync();
            var filteredColors = colors.Where(x => x.IsDeleted == false);
            return filteredColors.Select(x => new GetColorForViewDto
            {
                Id = x.Id,
                HexCode = x.HexCode,
                Code = x.Code
            }).ToList();
        }
        public async Task CreateOrEdit(CreateOrEditColorDto input)
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
        private async Task Create(CreateOrEditColorDto input)
        {
            var color = _mapper.Map<Colors>(input);
            color.IsDeleted = false;
            await _colorRepository.InsertAsync(color);
        }
        private async Task Edit(CreateOrEditColorDto input)
        {
            var color = await _colorRepository.GetSingleByIdAsync(input.Id.Value);
            _mapper.Map(input, color);
            await _colorRepository.UpdateAsync(color);
        }
        public async Task<GetColorForEditOutputDto> GetById(long id)
        {
            var color = await _colorRepository.GetSingleByIdAsync(id);
            return new GetColorForEditOutputDto
            {
                Id = color.Id,
                Code = color.Code,
                HexCode = color.HexCode,
            };
        }
        public async Task Delete(long id)
        {
            var color = await _colorRepository.GetSingleByIdAsync(id);
            color.IsDeleted = true;
            await _colorRepository.UpdateAsync(color);
        }
    }
}
