using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Core.Shared.Dtos;
using tnine.Core.Shared.Infrastructure;
using tnine.Core.Shared.Repositories;
using tnine.Core;
using tnine.Application.Shared.IPaymentMethodsService;
using tnine.Application.Shared.IPaymentMethodsService.Dto;

namespace tnine.Application
{
    public class PaymentMethodsService : IPaymentMethodsService
    {
        private readonly IPaymentMethodsRepository _paymentMethodsRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PaymentMethodsService(IPaymentMethodsRepository paymentMethodsRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _paymentMethodsRepo = paymentMethodsRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetPaymentMethodsForViewDto>> GetAll()
        {
            var paymentMethods = await _paymentMethodsRepo.GetAllAsync();
            return paymentMethods.Select(p => new GetPaymentMethodsForViewDto
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList();
        }
        public async Task CreateOrEdit(CreateOrEditPaymentMethodsDto input)
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

        private async Task Create(CreateOrEditPaymentMethodsDto input)
        {
            var paymentMethods = _mapper.Map<PaymentMethods>(input);
            await _paymentMethodsRepo.InsertAsync(paymentMethods);
        }

        private async Task Edit(CreateOrEditPaymentMethodsDto input)
        {
            var paymentMethods = await _paymentMethodsRepo.GetSingleByIdAsync(input.Id.Value);
            _mapper.Map(input, paymentMethods);
            await _paymentMethodsRepo.UpdateAsync(paymentMethods);
        }

        public async Task<GetPaymentMethodsForEditOutputDto> GetById(EntityDto<long> input)
        {
            var paymentMethods = await _paymentMethodsRepo.GetSingleByIdAsync(input.Id.Value);
            var output = new GetPaymentMethodsForEditOutputDto
            {
                PaymentMethods = _mapper.Map<CreateOrEditPaymentMethodsDto>(paymentMethods)
            };
            return output;
        }

        public async Task Delete(EntityDto<long> input)
        {
            await _paymentMethodsRepo.DeleteAsync(input.Id.Value);
        }
    }
}
