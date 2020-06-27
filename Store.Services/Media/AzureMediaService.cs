using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Store.Contracts;
using Store.Contracts.ViewModel;

namespace Store.Services
{
    public class AzureMediaService : IMediaService
    {
        private readonly CloudBlobClient _client;
        private readonly CloudBlobContainer _imgContainer;
        private readonly CloudBlobContainer _movieContainer;
        ConfigurationStorage Config;
        public AzureMediaService(IOptions<ConfigurationStorage> config) 
        {
 
            Config = config.Value;
            var account = CloudStorageAccount.Parse(Config.Connection);
            _client = account.CreateCloudBlobClient();
            if (!String.IsNullOrWhiteSpace(Config.ImageContainer)) { 
                _imgContainer = _client.GetContainerReference(Config.ImageContainer);}
            if (!String.IsNullOrWhiteSpace(Config.MovieContainer)) { 
                _movieContainer = _client.GetContainerReference(Config.MovieContainer);}
      
               
            }


            public async Task<bool> DeleteMedia(string mediaFileName, string partialPath = "")
        {
         
                if (!string.IsNullOrWhiteSpace(mediaFileName))
                {
                    var path = Path.Combine(partialPath, mediaFileName);
                    var blob = _imgContainer.GetBlobReference(path);
                    var result = await blob.DeleteIfExistsAsync();
                    if (!result)
                        return result;
                }
            

            return true;
        }

        public  async Task<bool> DeleteMovie(ContentViewModel model, string partialPath = "")
        {
            var mediaMovieProps = model.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(MovieAttribute))).ToList();

            foreach (var mediaMovieProp in mediaMovieProps)
            {
                var mediaMovieName = model.GetType().GetProperty(mediaMovieProp.Name).GetValue(model, null) as string;

                if (!string.IsNullOrWhiteSpace(mediaMovieName))
                {
                    var path = Path.Combine(partialPath, mediaMovieName);
                    var blob = _movieContainer.GetBlobReference(path);
                    var result = await blob.DeleteIfExistsAsync();
                    if (!result)
                        return result;
                }
            }

            return true;
        }

        public  async Task<bool> SaveMedia(ContentViewModel model, string partialPath = "", string propertyName = "", params string[] oldMedia)
        {
            var mediaDataProps = model.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(MediaAttribute))).ToList();

            var mediaFileProps = model.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(MediaFileAttribute)) && (propertyName == "" || prop.Name == propertyName)).ToList();

            foreach (var mediaFileProp in mediaFileProps)
            {
                var relatedProperty = (mediaFileProp.GetCustomAttributes(typeof(MediaFileAttribute), true).First() as MediaFileAttribute).RelatedProperty;

                if (mediaDataProps.Any(p => p.Name == relatedProperty))
                {
                    var mediaDataProp = mediaDataProps.First(p => p.Name == relatedProperty);

                    var mediaData = model.GetType().GetProperty(mediaDataProp.Name).GetValue(model, null) as string;
                    var mediaFileName = model.GetType().GetProperty(mediaFileProp.Name).GetValue(model, null) as string;

                    if (!string.IsNullOrWhiteSpace(mediaData))
                    {
                        var imgData = mediaData.Substring(mediaData.IndexOf(',') + 1);

                        var bytes = Convert.FromBase64String(imgData);

                        if (oldMedia != null && oldMedia.Length > 0)
                        {
                            var container = (_movieContainer == null) ? _imgContainer : _movieContainer;
                            foreach (var oldFile in oldMedia)
                            {
                                if (oldFile != null)
                                {
                                    var pathForDel = Path.Combine(partialPath, oldFile);
                                    var blob = container.GetBlobReference(pathForDel);
                                    var result = await blob.DeleteIfExistsAsync();
                                }
                            }
                        }

                        var path = Path.Combine(partialPath, mediaFileName);

                        var blockBlob = _imgContainer.GetBlockBlobReference(path);
                        if (path.EndsWith(".svg"))
                        {
                            blockBlob.Properties.ContentType = "image/svg+xml";
                        }

                        await blockBlob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);

                    }
                }
            }

            return true;
        }

        public  async Task<bool> UploadMedia(IFormFile media, string partialPath = "", params string[] oldMedia)
        {
            if (media != null && media.Length > 0)
            {
                var fileName = Path.GetFileName(media.FileName);

                if (oldMedia != null && oldMedia.Length > 0)
                {
                    foreach (var o in oldMedia)
                    {
                        if (!String.IsNullOrWhiteSpace(o))
                        {
                            var pathToDel = Path.Combine(partialPath, o);
                            var blob = _movieContainer.GetBlobReference(pathToDel);
                            var result = await blob.DeleteIfExistsAsync();
                        }
                    }
                }

                var path = Path.Combine(partialPath, fileName);

                var container = (_movieContainer == null) ? _imgContainer : _movieContainer;

                var blockBlob = container.GetBlockBlobReference(path);
                if (path.EndsWith(".svg"))
                {
                    blockBlob.Properties.ContentType = "image/svg+xml";
                }
                var fileBytes = new byte[media.Length];

                using (var stream = media.OpenReadStream())
                {
                    await stream.ReadAsync(fileBytes, 0, Convert.ToInt32(media.Length));
                }
                await blockBlob.UploadFromByteArrayAsync(fileBytes, 0, fileBytes.Length);
            }

            return true;
        }

        public  async Task<MemoryStream> GetStreamMedia(string partialPath, string fileName)
        {
            if (partialPath != null && fileName.Length > 0)
            {
                var fName = Path.GetFileName(fileName);

                var path = Path.Combine(partialPath, fName);

                var container = (_movieContainer == null) ? _imgContainer : _movieContainer;

                var blockBlob = container.GetBlockBlobReference(path);

                var stream = new MemoryStream();

                await blockBlob.DownloadToStreamAsync(stream);

                return stream;
            }

            return null;
        }

       

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
