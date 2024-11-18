using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IPaymentStatusService;
using tnine.Application.Shared.IPaymentStatusService.Dto;
using tnine.Core;
using tnine.Core.Shared.Dtos;
using tnine.Core.Shared.Infrastructure;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class PaymentStatusService : IPaymentStatusService
    {
        private readonly IPaymentStatusRepository _paymentStatusRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PaymentStatusService(IPaymentStatusRepository paymentStatusRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _paymentStatusRepo = paymentStatusRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetPaymentStatusForViewDto>> GetAll()
        {
            var paymentStatus = await _paymentStatusRepo.GetAllAsync();
            var filteredPaymentStatus = paymentStatus.Where(x => x.IsDeleted == false);
            return filteredPaymentStatus.Select(p => new GetPaymentStatusForViewDto
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList();
        }
        public async Task CreateOrEdit(CreateOrEditPaymentStatusDto input)
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

        private async Task Create(CreateOrEditPaymentStatusDto input)
        {
            var paymentStatus = _mapper.Map<PaymentStatus>(input);
            paymentStatus.IsDeleted = false;
            await _paymentStatusRepo.InsertAsync(paymentStatus);
        }

        private async Task Edit(CreateOrEditPaymentStatusDto input)
        {
            var paymentStatus = await _paymentStatusRepo.GetSingleByIdAsync(input.Id.Value);
            _mapper.Map(input, paymentStatus);
            await _paymentStatusRepo.UpdateAsync(paymentStatus);
        }

        public async Task<GetPaymentStatusForEditOutputDto> GetById(EntityDto<long> input)
        {
            var paymentStatus = await _paymentStatusRepo.GetSingleByIdAsync(input.Id.Value);
            var output = new GetPaymentStatusForEditOutputDto
            {
                PaymentStatus = _mapper.Map<CreateOrEditPaymentStatusDto>(paymentStatus)
            };
            return output;
        }

        public async Task Delete(EntityDto<long> input)
        {
            var paymentStatus = await _paymentStatusRepo.GetSingleByIdAsync(input.Id.Value);
            paymentStatus.IsDeleted = true;
            await _paymentStatusRepo.DeleteAsync(input.Id.Value);
        }
    }
}