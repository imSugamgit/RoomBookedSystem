using RoomMainSetup.Entity.RoomModel;
using RoomModel.MainSetup.Assembler;
using RoomModel.MainSetup.Repository;
using RoomModel.Src.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoomModel.MainSetup.Service
{
   public interface IInvoiceService
    {
        Task<InvoiceDto> Insertasync(InvoiceDto dto);
        Task<Invoice> Delete(long Id);
        Task<InvoiceDto> UpdateAsync(InvoiceDto dto);
    }
    public class InvoiceService : IInvoiceService
    {
            private readonly IInvoiceRepository _InvoiceRepository;
            private readonly IInvoiceAssembler _assembler;
            public InvoiceService(IInvoiceRepository InvoiceRepository,
                IInvoiceAssembler assembler)
            {
            _InvoiceRepository = InvoiceRepository;
                _assembler = assembler;
            }
            public async Task<InvoiceDto> Insertasync(InvoiceDto dto)
            {
            Invoice Invoice = new Invoice();
                _assembler.copyTo(Invoice, dto);
                await _InvoiceRepository.AddAsync(Invoice);
                dto.Id = Invoice.Id;
                return dto;
            }

            public async Task<InvoiceDto> UpdateAsync(InvoiceDto dto)
            {
            Invoice Invoice = new Invoice();
                _assembler.modifyTo(Invoice, dto);
                await _InvoiceRepository.UpdateAsync(Invoice);
                return dto;
            }

            public async Task<Invoice> Delete(long Id)
            {
                var invoice = await _InvoiceRepository.GetByIdAsync(Id) ?? throw new Exception();
                return await _InvoiceRepository.DeleteAsync(invoice).ConfigureAwait(true);
            }
        }
    }



