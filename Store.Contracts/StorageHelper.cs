using Microsoft.Extensions.Options;
using Store.Contracts.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Contracts
{
   public  class StorageHelper
    {
      
        private ConfigurationStorage configStorage;

        public StorageHelper(IOptions<ConfigurationStorage> storage)
        {
            configStorage = storage.Value;
        }

        public string CrateContainer<T>(T viewModel) where T: BaseViewModel
        {
            string name = GetName<T>();

            return $"{name}/{viewModel.Id}";
        }

        private static string GetName<T>() where T : BaseViewModel
        {
            return typeof(T).Name.Replace("ViewModel", string.Empty);
        }

        public string GetImageUrl<T>(T viewModel,string imageName ) where T : BaseViewModel
        {
            string name = GetName<T>();
            var res = string.Empty;
            //httpserver or storage link
            if (!string.IsNullOrWhiteSpace(configStorage.Url))
            {
                res = $"{configStorage.Url}/{name}/{viewModel.Id}/{imageName}";
            }
            //local folder
            else if (!string.IsNullOrWhiteSpace(configStorage.Storage))
            {
                res = $"{configStorage.Storage.Replace('\\', '/')}/{name}/{viewModel.Id}/{imageName}";
            }
            //wwwrooot
            else {
                res = $"{name}/{viewModel.Id}/{imageName}";
            }
            return res;
        }

    }



   
}
