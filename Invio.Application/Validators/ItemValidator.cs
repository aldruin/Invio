using FluentValidation;
using Invio.Application.DTOs;
using Invio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.Validators
{
    public class ItemValidator : AbstractValidator<ItemDto>
    {
        public ItemValidator() 
        {
            RuleFor(i => i.Nome)
                .NotEmpty().WithMessage("O nome do item é obrigatório");

            RuleFor(i => i.Quantidade)
                .GreaterThan(0).WithMessage("A quantidade do item deve ser maior do que zero.");

            RuleFor(i => i.Descricao)
                .NotEmpty().WithMessage("A descrição do item é obrigatória");

            RuleFor(i => i.Categoria)
                .NotEmpty().WithMessage("A categoria do item é obrigatória")
                .Must(VerificaClasseValida).WithMessage("A categoria do item é inválida");

            RuleFor(i => i.EquipeId)
                .NotEmpty().WithMessage("O ID da Equipe do item é obrigatório.");

            RuleFor(i => i.DataFornecimento)
                .NotEmpty().WithMessage("A data de fornecimento é obrigatória");

            RuleFor(i => i.DataTermino)
                .Must((dto, end) => end == null || end >= dto.DataFornecimento)
                .WithMessage("A data de fim deve ser maior ou igual à data de início.");




        }
        private bool VerificaClasseValida(string? categoria)
        {
            if (string.IsNullOrEmpty(categoria))
                return false;

            return Enum.TryParse(typeof(ItemCategoria), categoria, out var result) && Enum.IsDefined(typeof(ItemCategoria), result);
        }
    }
}
