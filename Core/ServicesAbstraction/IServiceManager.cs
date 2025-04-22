﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IServiceManager
    {
        IProductService productService { get; }
        IBasketService basketService { get; }
        ICacheService cacheService { get; }
        IAuthService authService { get; }
    }
}
