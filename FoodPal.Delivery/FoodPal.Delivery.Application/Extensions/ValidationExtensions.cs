using FluentValidation;
using FoodPal.Delivery.Common.Exceptions;
using System.Linq;

namespace FoodPal.Delivery.Application.Extensions
{
    public static class ValidationExtensions
    {
        public static void ValidateAndThrowEx<T>(this IValidator<T> validator, T o)
        {
            var result = validator.Validate(o);
            if (!result.IsValid)
            {
                throw new ValidationsException(result.Errors.Select(x => x.ErrorMessage).ToList());
            }
        }
    }
}
