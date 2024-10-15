﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Application.DTOs
{
    public class ResponseDTO
    {
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public object? Model { get; set; }
    }
}
