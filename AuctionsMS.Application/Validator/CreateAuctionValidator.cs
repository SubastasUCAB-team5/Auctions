using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionMS.Commons.Dtos.Request;

namespace AuctionMS.Application.Validator
{
    public class CreateAuctionValidator : ValidatorBase<CreateAuctionDto>
    {
        public CreateAuctionValidator()
        {
            RuleFor(s => s.Name)
                .NotNull().WithMessage("Name no puede ser nulo").WithErrorCode("040")
                .NotEmpty().WithMessage("Name no puede estar vacio").WithErrorCode("041")
                .MinimumLength(3).WithMessage("Name debe tener al menos 3 caracteres").WithErrorCode("042")
                .MaximumLength(50).WithMessage("Name no puede tener más de 50 caracteres").WithErrorCode("043");
            RuleFor(s => s.Description)
                .NotNull().WithMessage("Description no puede ser nulo").WithErrorCode("044")
                .NotEmpty().WithMessage("Description no puede estar vacio").WithErrorCode("045")
                .MinimumLength(3).WithMessage("Description debe tener al menos 3 caracteres").WithErrorCode("046")
                .MaximumLength(500).WithMessage("Description no puede tener más de 500 caracteres").WithErrorCode("047");
            RuleFor(s => s.BasePrice)
                .NotNull().WithMessage("BasePrice no puede ser nulo").WithErrorCode("048");
            RuleFor(s => s.StartTime)
                .NotNull().WithMessage("StartTime no puede ser nulo").WithErrorCode("061")
                .GreaterThan(DateTime.UtcNow).WithMessage("StartTime debe ser una fecha futura").WithErrorCode("062");
            RuleFor(s => s.EndTime)
                .NotNull().WithMessage("EndTime no puede ser nulo").WithErrorCode("063")
                .GreaterThan(s => s.StartTime).WithMessage("EndTime debe ser mayor que StartTime").WithErrorCode("064");
            RuleFor(s => s.MinimumIncrement)
                .NotNull().WithMessage("MinimumIncrement no puede ser nulo").WithErrorCode("065");
            RuleFor(s => s.ReservePrice)
                .NotNull().WithMessage("ReservePrice no puede ser nulo").WithErrorCode("067");
            RuleFor(s => s.AuctionType)
                .NotNull().WithMessage("AuctionType no puede ser nulo").WithErrorCode("069")
                .NotEmpty().WithMessage("AuctionType no puede estar vacio").WithErrorCode("070");
            RuleFor(s => s.Images)
                .NotNull().WithMessage("Images no puede ser nulo").WithErrorCode("055")
                .NotEmpty().WithMessage("Images no puede estar vacio").WithErrorCode("056")
                .Must(x => x.Count <= 5).WithMessage("Images no puede tener más de 5 elementos").WithErrorCode("057")
                .Must(x => x.All(i => i.Length <= 200)).WithMessage("Cada imagen no puede tener más de 200 caracteres").WithErrorCode("058")
                .Must(x => x.All(i => i.StartsWith("http") || i.StartsWith("https"))).WithMessage("Cada imagen debe ser una URL válida").WithErrorCode("059")
                .Must(x => x.All(i => i.EndsWith(".jpg") || i.EndsWith(".png") || i.EndsWith(".jpeg") || i.EndsWith(".webp"))).WithMessage("Cada imagen debe ser una URL válida").WithErrorCode("060");
            RuleFor(s => s.State)
                .NotNull().WithMessage("State no puede ser nulo").WithErrorCode("071")
                .NotEmpty().WithMessage("State no puede estar vacio").WithErrorCode("072");
            RuleFor(s => s.Products)
                .NotNull().WithMessage("Products no puede ser nulo").WithErrorCode("073")
                .NotEmpty().WithMessage("Products no puede estar vacio").WithErrorCode("074");
        }
    }
}
