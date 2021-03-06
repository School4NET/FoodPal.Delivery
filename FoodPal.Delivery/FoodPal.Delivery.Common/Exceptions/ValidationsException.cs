﻿using System;
using System.Collections.Generic;

namespace FoodPal.Delivery.Common.Exceptions
{
    public class ValidationsException : Exception
    {
        public ValidationsException(List<string> errors)
        {
            Errors = errors;
        }

        public List<string> Errors { get; }
    }
}
