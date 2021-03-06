﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Contracts.ViewModel
{
    public class DeliveryMethodViewModel :BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int MaxCountInPackage { get; set; }

        public decimal Price { get; set; }
    }
}
