using Microsoft.AspNetCore.Http;
using Store.Contracts.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Store.Contracts
{
    public interface IMediaService : IDisposable
    {
        Task<bool> SaveMedia(BaseViewModel model, string partialPath = "", string propertyName = "", params string[] oldMedia);

        Task<bool> DeleteMedia(BaseViewModel model, string partialPath = "");

        Task<bool> UploadMedia(IFormFile media, string partialPath = "", params string[] oldMedia);

        Task<bool> DeleteMovie(BaseViewModel model, string partialPath = "");

        Task<MemoryStream> GetStreamMedia(string partialPath, string fileName);
    }
}
