using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IOrderService;
using tnine.Application.Shared.IOrderService.Dto;
using tnine.Core;
using tnine.Core.Shared.Infrastructure;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetOrderForViewDto>> GetAll()
        {
            var orders = await _orderRepository.GetAllAsync();
            var customers = await _customerRepository.GetAllAsync();
            return (from order in orders
                    join customer in customers on order.CustomerId equals customer.Id
                    select new GetOrderForViewDto
                    {
                        Id = order.Id,
                        CreationTime = (DateTime)order.CreationTime,
                        CustomerName = customer.FullName,
                        CustomerTelephone = customer.PhoneNumber,
                        Total = order.Total
                    }).ToList();
        }

        public async Task CreateOrEdit(CreateOrEditOrderDto input)
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

        private async Task Create(CreateOrEditOrderDto input)
        {
            input.CreationTime = DateTime.Now;
            var order = _mapper.Map<Orders>(input);
            await _orderRepository.InsertAsync(order);
        }

        private async Task Edit(CreateOrEditOrderDto input)
        {
            var order = await _orderRepository.GetSingleByIdAsync(input.Id.Value);
            _mapper.Map(input, order);
            await _orderRepository.UpdateAsync(order);
        }

        public async Task<GetOrderForEditOutputDto> GetById(long id)
        {
            var order = await _orderRepository.GetSingleByIdAsync(id);

            return new GetOrderForEditOutputDto
            {
                Order = _mapper.Map<CreateOrEditOrderDto>(order)
            };
        }

        public async Task Delete(long id)
        {
            await _orderRepository.DeleteAsync(id);
        }
    }
}
