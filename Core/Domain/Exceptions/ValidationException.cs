﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ValidationException(IEnumerable<string> errors) : Exception("ValidationError")
    {
        public IEnumerable<string> Errors { get; } = errors;
    }
}
