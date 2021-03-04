using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Api.Dto
{
    public class HttpErrorResponseDto
    {
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public string Details { get; set; }
    }
}
