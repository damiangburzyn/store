using Store.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Contracts
{
    public class ConfigurationStorage
    {
        public string Connection { get; set; }

        public string Url { get; set; }

        public string Storage { get; set; }

        public string ImageContainer { get; set; }

        public string MovieContainer { get; set; }

        public EStorageType StorageType { get; set; }
    }
}
