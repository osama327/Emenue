﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.Core.Dto.ProductAttribute
{
    public class ProductAttributeDto
    {
        public int? Id { get; set; }

        public string? Name { get; set; } = string.Empty;
    }
}
