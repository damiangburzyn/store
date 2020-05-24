using Store.Contracts.ViewModel;
using System.Collections.Generic;

namespace Store.Contracts
{
    public interface ILocalPageData
    {
        Dictionary<string, PageViewModel> PageInfo { get; }
       
    }
}