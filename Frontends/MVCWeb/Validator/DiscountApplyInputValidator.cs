using FluentValidation;
using MVCWeb.Models.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb.Validator
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("indirim kupon alanı boş olamaz");
        }
    }
}
