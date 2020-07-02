using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Store.Contracts.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Store.Contracts
{
    public class StorageHelper
    {

        private ConfigurationStorage configStorage;
        private IUrlHelperFactory urlHelperFactory;
        private readonly IActionContextAccessor actionAccessor;


        public StorageHelper(IOptions<ConfigurationStorage> storage, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor)
        {
            configStorage = storage.Value;
            this.urlHelperFactory = urlHelperFactory;
            this.actionAccessor = actionAccessor;
        }


        public string CrateContainer<T>(T viewModel) where T : BaseViewModel
        {
            string name = GetName<T>();

            return $"{name}\\{viewModel.Id}";
        }

        private static string GetName<T>() where T : BaseViewModel
        {
            return typeof(T).Name.Replace("ViewModel", string.Empty);
        }

        public string GetImageUrl<T>(T viewModel, string imageName) where T : BaseViewModel
        {

            var urlHelper = this.urlHelperFactory.GetUrlHelper(this.actionAccessor.ActionContext);

            string name = GetName<T>();
            var res = string.Empty;
            //(httpserver hostuje i storage zapisuje) or storage link np na azure
            if (!string.IsNullOrWhiteSpace(configStorage.Url))
            {
                res = $"{configStorage.Url}/{name}/{viewModel.Id}/{imageName}";
            }
            //local folder or wwwroot
            else if (!string.IsNullOrWhiteSpace(configStorage.Storage))
            {

                res = $"/{configStorage.Storage.Replace('\\', '/')}/{name}/{viewModel.Id}/{imageName}";
                res = urlHelper.Content(res);

            }
            //wwwrooot
            else
            {
                res = $"/{name}/{viewModel.Id}/{imageName}";
                res = urlHelper.Content(res);


            }
            return res;
        }


    }

}
