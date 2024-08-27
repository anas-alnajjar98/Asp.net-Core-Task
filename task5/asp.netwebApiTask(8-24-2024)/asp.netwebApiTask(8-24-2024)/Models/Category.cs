﻿using System;
using System.Collections.Generic;

namespace asp.netwebApiTask_8_24_2024_.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? CategoryImage { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
