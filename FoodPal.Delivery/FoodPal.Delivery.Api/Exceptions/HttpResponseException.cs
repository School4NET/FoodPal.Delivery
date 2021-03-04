using FoodPal.Delivery.Dto.Intern;
using System;

namespace FoodPal.Delivery.Api.Exceptions
{
    public class HttpResponseException : Exception
    {
        public InternalErrorResponseDto ExceptionDto { get; private set; }

        public HttpResponseException(InternalErrorResponseDto internalResponseDto)
        {
            this.ExceptionDto = internalResponseDto;   
        }
    }
}
