﻿using AutoMapper;
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

            var query = from invoice in invoices
                    join customer in customers on invoice.CustomerId equals customer.Id into customerjoin
                    from customer in customerjoin.DefaultIfEmpty()
                    join paymentMethod in paymentMethods on invoice.PaymentMethodId equals paymentMethod.Id
                    join paymentStatu in paymentStatus on invoice.PaymentStatusId equals paymentStatu.Id
                    select new GetInvoiceForViewDto
                    {
                        Id = invoice.Id,
                        CreationTime = (DateTime)invoice.CreationTime,
                        CustomerName = customer == null ? "One Time Customer" : customer.FullName,
                        CustomerTelephone = customer == null ? "" : customer.PhoneNumber,
                        PaymentStatusName = paymentStatu.Name,
                        PaymentMethodName = paymentMethod.Name,
                        Total = invoice.Total
                    };
            return query.ToList();
        }

        public async Task CreateOrEdit(InvoiceAndInvoiceDetailsDto input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input), "Input cannot be null");

            if (input.Invoice == null) throw new ArgumentNullException(nameof(input.Invoice), "Invoice cannot be null");

            if (input.Invoice.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Edit(input);
            }
        }

        private async Task Create(InvoiceAndInvoiceDetailsDto input)
        {
            var productVariations = _productVariationsRepository.GetAll();
            foreach (var item in input.Items)
            {
                var productVariation = productVariations.FirstOrDefault(pv => pv.ProductId == item.ProductId && pv.SizeId == item.SizeId && pv.ColorId == item.ColorId);
                if (productVariation == null) 
                    throw new Exception($"Product variation not found for product ID {item.ProductId}, size ID {item.SizeId}, color ID {item.ColorId}");
                if (productVariation.Quantity < item.Quantity) 
                    throw new Exception($"Not enough stock for product ID {item.ProductId}, size ID {item.SizeId}, color ID {item.ColorId}");
            }

            input.Invoice.CreationTime = DateTime.Now;
            input.Invoice.CustomerId = input.Invoice.CustomerId == 0 ? null : input.Invoice.CustomerId;
            var invoice = _mapper.Map<Invoice>(input.Invoice);
            var invoiceId = await _invoiceRepository.InsertAndGetIdAsync(invoice);

            foreach (var item in input.Items)
            {
                var productInvoice = new ProductInvoices
                {
                    InvoiceId = invoiceId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    SizeId = item.SizeId,
                    ColorId = item.ColorId
                };
                await _productInvoicesRepository.InsertAsync(productInvoice);
            }
        }

        private async Task Edit(InvoiceAndInvoiceDetailsDto input)
        {
            var invoice = await _invoiceRepository.GetSingleByIdAsync(input.Invoice.Id.Value);
            _mapper.Map(input.Invoice, invoice);
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
                                   CustomerName = c != null ? c.FullName : "One time customer",
                                   CustomerAddress = c != null ? c.Address : "",
                                   CustomerPhoneNumber = c != null ? c.PhoneNumber : "",
                                   PaymentMode = pm.Name,
                                   TotalAmount = inv.Total
                               }).FirstOrDefault();

            if (invoiceDetail == null) throw new Exception($"Invoice with ID {id} not found.");

            invoiceDetail.Items = (from pi in _productInvoicesRepository.GetAll()
                                 join p in _productRepository.GetAll() on pi.ProductId equals p.Id
                                 join pv in _productVariationsRepository.GetAll() on p.Id equals pv.ProductId
                                 join color in _colorRepository.GetAll() on pi.ColorId equals color.Id
                                 join size in _sizeRepository.GetAll() on pi.SizeId equals size.Id
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
