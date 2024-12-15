using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IInvoiceService;
using tnine.Application.Shared.IInvoiceService.Dto;
using tnine.Core;
using tnine.Core.Shared.Infrastructure;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPaymentMethodsRepository _paymentMethodsRepository;
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly IProductInvoicesRepository _productInvoicesRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductVariationsRepository _productVariationsRepository;
        private readonly IColorRepository _colorRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoiceService(
            IInvoiceRepository invoiceRepository,
            ICustomerRepository customerRepository,
            IPaymentMethodsRepository paymentMethodsRepository,
            IPaymentStatusRepository paymentStatusRepository,
            IProductInvoicesRepository productInvoicesRepository,
            IProductRepository productRepository,
            IProductVariationsRepository productVariationsRepository,
            IColorRepository colorRepository,
            ISizeRepository sizeRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
            _paymentMethodsRepository = paymentMethodsRepository;
            _paymentStatusRepository = paymentStatusRepository;
            _productInvoicesRepository = productInvoicesRepository;
            _productRepository = productRepository;
            _productVariationsRepository = productVariationsRepository;
            _colorRepository = colorRepository;
            _sizeRepository = sizeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetInvoiceForViewDto>> GetAll()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            var customers = await _customerRepository.GetAllAsync();
            var paymentMethods = await _paymentMethodsRepository.GetAllAsync();
            var paymentStatus = await _paymentStatusRepository.GetAllAsync();

            return (from invoice in invoices
                    join customer in customers on invoice.CustomerId equals customer.Id
                    join paymentMethod in paymentMethods on invoice.PaymentMethodId equals paymentMethod.Id
                    join paymentStatu in paymentStatus on invoice.PaymentStatusId equals paymentStatu.Id
                    select new GetInvoiceForViewDto
                    {
                        Id = invoice.Id,
                        CreationTime = (DateTime)invoice.CreationTime,
                        CustomerName = customer.FullName == null ? "" : customer.FullName,
                        CustomerTelephone = customer.PhoneNumber == null ? "" : customer.PhoneNumber,
                        PaymentStatusName = paymentStatu.Name == null ? "" : paymentStatu.Name,
                        PaymentMethodName = paymentMethod.Name == null ? "" : paymentMethod.Name,
                        Total = invoice.Total 
                    }).ToList();
        }

        public async Task CreateOrEdit(CreateOrEditInvoiceDto input)
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

        private async Task Create(CreateOrEditInvoiceDto input)
        {
            input.CreationTime = DateTime.Now;
            var invoice = _mapper.Map<Invoice>(input);
            await _invoiceRepository.InsertAsync(invoice);
        }

        private async Task Edit(CreateOrEditInvoiceDto input)
        {
            var invoice = await _invoiceRepository.GetSingleByIdAsync(input.Id.Value);
            _mapper.Map(input, invoice);
            await _invoiceRepository.UpdateAsync(invoice);
        }

        public async Task<GetInvoiceForEditOutputDto> GetById(long id)
        {
            var invoice = await _invoiceRepository.GetSingleByIdAsync(id);

            return new GetInvoiceForEditOutputDto
            {
                Invoice = _mapper.Map<CreateOrEditInvoiceDto>(invoice)
            };
        }

        public async Task Delete(long id)
        {
            await _invoiceRepository.DeleteAsync(id);
        }


        public GetInvoiceDetailDto GetInvoiceDetailInfo(long id)
        {
            var invoiceDetail = (from inv in _invoiceRepository.GetAll()
                               join pm in _paymentMethodsRepository.GetAll() on inv.PaymentMethodId equals pm.Id
                               join ps in _paymentStatusRepository.GetAll() on inv.PaymentStatusId equals ps.Id
                               join c in _customerRepository.GetAll() on inv.CustomerId equals c.Id into customerGroup
                               from c in customerGroup.DefaultIfEmpty()
                               where inv.Id == id
                               select new GetInvoiceDetailDto
                               {
                                   InvoiceNumber = $"Tnine-Inv-{inv.Id}",
                                   Date = (DateTime)inv.CreationTime,
                                   CustomerName = c != null ? c.FullName : "",
                                   PaymentMode = pm.Name,
                                   TotalAmount = inv.Total
                               }).FirstOrDefault();

            if (invoiceDetail == null) throw new Exception($"Invoice with ID {id} not found.");

            invoiceDetail.Items = (from pi in _productInvoicesRepository.GetAll()
                                 join p in _productRepository.GetAll() on pi.ProductId equals p.Id
                                 join pv in _productVariationsRepository.GetAll() on p.Id equals pv.ProductId
                                 join color in _colorRepository.GetAll() on pv.ColorId equals color.Id
                                 join size in _sizeRepository.GetAll() on pv.SizeId equals size.Id
                                 where pi.InvoiceId == id
                                 select new InvoiceItem
                                 {
                                     ItemName = $"{p.Name} ({color.Code}, {size.Name})",
                                     Quantity = pi.Quantity,
                                     UnitPrice = p.Price,
                                 }).ToList();

            return invoiceDetail;
        }

    }
}
