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
    public class EquipeValidator : AbstractValidator<EquipeDto>
    {
        public EquipeValidator() 
        {
            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("O nome da equipe é obrigatório.");

            RuleFor(e => e.Categoria)
                .NotEmpty().WithMessage("A categoria de equipe é obrigatória.")
                .Must(VerificarCategoria).WithMessage("Categoria de equipe inválida.");
        }

        private bool VerificarCategoria(EquipeCategoria? equipe)
        {
            if (!equipe.HasValue)
                return false;
            return Enum.IsDefined(typeof(EquipeCategoria), equipe.Value);
        }
    }
}
