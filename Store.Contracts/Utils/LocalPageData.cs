using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Contracts.ViewModel;
using Microsoft.Extensions.Logging;
using Store.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace GC5.Application.Utils
{
    public class LocalPageData : ILocalPageData
    {
        private IMemoryCache _appState;
        private ILogger<LocalPageData> _logger;
       
        private const string pageInfoDataKey = "pageInfo_positions";




        public Dictionary<string, PageViewModel> PageInfo
        {
            get
            {
                Dictionary<string, PageViewModel> pages = null;
                if (!_appState.TryGetValue(pageInfoDataKey, out pages))
                {
                    LoadPageData().Wait();
                    _appState.TryGetValue(pageInfoDataKey, out pages);
                }
                return pages;
            }
        }

      
        public LocalPageData(IMemoryCache appState,
           
            ILogger<LocalPageData> logger)
        {

            //  refresher.RefreshOccured += this.refreshEventOccured;
            this._appState = appState;
            this._logger = logger;
          //  Task.Run(() => LoadAllDataAsync());
        }

        async void refreshEventOccured(object sender, EventArgs e)
        {
            await LoadPageData();
        }

      

      

    

    


        private async Task LoadPageData()
        {//TODO: Pobrac dane stron z bazy dnaych i złaładować je do memoryCache
            var pageData = new DataTableSearchViewModel<PageViewModel>();
            var dict = new Dictionary<string, PageViewModel>();
            if (pageData != null && pageData.Data != null)
            {
                foreach (var item in pageData.Data)
                {
                    dict.Add(item.PageName.ToLower(), item);
                }
            }
            _appState.Set(pageInfoDataKey, dict);
        }


    }
}
