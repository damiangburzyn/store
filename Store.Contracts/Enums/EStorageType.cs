using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Contracts.Enums
{
    public enum EStorageType
    {

        /// <summary>
        /// wykorzystuje hosting plików bloba azure
        /// </summary>
        Azure,
        /// <summary>
        /// server http , lokalny bądź inny hostujący pliki
        /// </summary>
        HttpServer,
        /// <summary>
        /// Korzysta z tego samego servera co aplikacja
        /// ze staticfiles wwwroot
        /// </summary>
        LocalServerWwwRoot

    }
}
