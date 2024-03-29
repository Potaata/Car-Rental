﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Exceptions
{
    public class ApiException: Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public ApiException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
