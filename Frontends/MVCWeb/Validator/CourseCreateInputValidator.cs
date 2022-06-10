using FluentValidation;
using MVCWeb.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb.Validator
{
    public class CourseCreateInputValidator :AbstractValidator<CourseCreateInput>
    {

        public CourseCreateInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("isim alani bos olamaz");
            RuleFor(x => x.Description).NotEmpty().WithMessage("aciklama alani bos olamaz");
            RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("süre alani bos olamaz");
            RuleFor(x => x.Price).NotEmpty().WithMessage("fiyat bos olamaz").ScalePrecision(2, 6).WithMessage("hatali tutar formati");
            //virgülden önce 4 karakter, virgülden sonra 2 karakter toplam 6 karakter olacak diyoruz. ($$$$.$$)
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("kategori alani seciniz");
        }
    }
}
