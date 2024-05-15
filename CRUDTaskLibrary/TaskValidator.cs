using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CRUDTaskLibrary
{
    public class TaskValidator : AbstractValidator<Task>
    {
        public TaskValidator()
        {
            RuleFor(x => x.judul).NotEmpty().WithMessage("Judul is required.").WithErrorCode("JudulIsNull");
            RuleFor(x => x.username).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.deskripsi).NotEmpty().WithMessage("Deskripsi is required.");
            RuleFor(x => x.tanggalMulai).NotEmpty().WithMessage("Tanggal Mulai is required.");
            RuleFor(x => x.tanggalSelesai).NotEmpty().WithMessage("Tanggal Selesai is required.");
            RuleFor(x => x.tanggalSelesai).GreaterThan(x => x.tanggalMulai).WithMessage("Tanggal Selesai harus lebih dari Tanggal Mulai.");
            RuleFor(x => x.jenisTugas).IsInEnum().WithMessage("Invalid Kode Jenis Tugas.");
            RuleFor(x => x.namaPrioritas).IsInEnum().WithMessage("Invalid Urutan Prioritas.");
            
        }
    }
}

