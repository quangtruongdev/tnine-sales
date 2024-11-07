using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Core.Shared.Dtos;
using tnine.Core.Shared.Infrastructure;
using tnine.Core;
using tnine.Application.Shared.ICustomerService;
using tnine.Core.Shared.Repositories;
using tnine.Application.Shared.ICustomerService.Dto;

namespace tnine.Application
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetCustomerForViewDto>> GetAll()
        {
            var customers = await _customerRepository.GetAllAsync();

            return customers.Select(e => new GetCustomerForViewDto
            {
                Id = e.Id,
                Username = e.Username,
                FullName = e.FullName,
                Address = e.Address,
                PhoneNumber = e.PhoneNumber
            }).ToList();
        }

        public async Task CreateOrEdit(CreateOrEditCustomerDto input)
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

        private async Task Create(CreateOrEditCustomerDto input)
        {
            var customer = _mapper.Map<Customer>(input);
            await _customerRepository.InsertAsync(customer);
        }

        private async Task Edit(CreateOrEditCustomerDto input)
        {
            var customer = await _customerRepository.GetSingleByIdAsync(input.Id.Value);
            _mapper.Map(input, customer);
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task<GetCustomerForEditOutputDto> GetCustomerForEdit(EntityDto<long> input)
        {
            var customer = await _customerRepository.GetSingleByIdAsync(input.Id.Value);
            var output = new GetCustomerForEditOutputDto
            {
                Customer = _mapper.Map<CreateOrEditCustomerDto>(customer)
            };

            return output;
        }

        public async Task Delete(EntityDto<long> input)
        {
            await _customerRepository.DeleteAsync(input.Id.Value);
        }
    }
}
