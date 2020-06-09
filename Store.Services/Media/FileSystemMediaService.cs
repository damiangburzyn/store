using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Store.Contracts;
using Store.Contracts.ViewModel;
using Store.Services;

namespace Store.Services
{
    public class FileSystemMediaService : IMediaService
    {

        public FileSystemMediaService(IOptions<ConfigurationStorage> config,
           IHostingEnvironment environment)
        {
            this.config = config.Value;
            _environment = environment;
        }

        ConfigurationStorage config { get; set; }
        private readonly IHostingEnvironment _environment;

        public async Task<bool> DeleteMedia(ContentViewModel model, string partialPath = "")
        {
            var path = GetPath(partialPath);

            var mediaFileProps = model.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(MediaFileAttribute))).ToList();

            foreach (var mediaFileProp in mediaFileProps)
            {
                var mediaFileName = model.GetType().GetProperty(mediaFileProp.Name).GetValue(model, null) as string;

                if (!string.IsNullOrWhiteSpace(mediaFileName))
                {
                    if (!Directory.Exists(path))
                    {
                        await Task.Run(() => Directory.CreateDirectory(path));
                        break;
                    }

                    var pathToFile = Path.Combine(path, mediaFileName);

                    if (File.Exists(pathToFile))
                        await Task.Run(() => File.Delete(pathToFile));
                }
            }

            return true;
        }

        public async Task<bool> DeleteMovie(ContentViewModel model, string partialPath = "")
        {
            var path = GetPath(partialPath);

            var mediaMovieProps = model.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(MovieAttribute))).ToList();

            foreach (var mediaMovieProp in mediaMovieProps)
            {
                var mediaMovieName = model.GetType().GetProperty(mediaMovieProp.Name).GetValue(model, null) as string;

                if (!string.IsNullOrWhiteSpace(mediaMovieName))
                {
                    if (!Directory.Exists(path))
                    {
                        await Task.Run(() => Directory.CreateDirectory(path));
                        break;
                    }


                    var pathToMovie = Path.Combine(path, mediaMovieName);

                    if (File.Exists(pathToMovie))
                        await Task.Run(() => File.Delete(pathToMovie));
                }
            }

            return true;
        }

        public Task<MemoryStream> GetStreamMedia(string partialPath, string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveMedia(ContentViewModel model, string partialPath = "", string propertyName = "", params string[] oldMedia)
        {
            var path = GetPath(partialPath);

            var mediaDataProps = model.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(MediaAttribute))).ToList();

            var mediaFileProps = model.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(MediaFileAttribute)) && (string.IsNullOrWhiteSpace(propertyName) || prop.Name == propertyName)).ToList();

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

                        if (!Directory.Exists(path))
                            await Task.Run(() => Directory.CreateDirectory(path));


                        if (oldMedia != null && oldMedia.Length > 0)
                        {
                            foreach (var oldFile in oldMedia)
                            {
                                var pathForDel = Path.Combine(path, oldFile);
                                if (File.Exists(pathForDel))
                                    await Task.Run(() => File.Delete(pathForDel));
                            }
                        }

                        var pathWithFile = Path.Combine(path, mediaFileName);

                        using (var imageFile = new FileStream(pathWithFile, FileMode.Create))
                        {
                            await imageFile.WriteAsync(bytes, 0, bytes.Length);
                            await imageFile.FlushAsync();
                        }
                    }
                }
            }

            return true;
        }

        public async Task<bool> UploadMedia(IFormFile media, string partialPath = "", params string[] oldMedia)
        {
            if (media != null && media.Length > 0)
            {
                var fileName = Path.GetFileName(media.FileName);

                var path = GetPath(partialPath);

                if (!Directory.Exists(path))
                    await Task.Run(() => Directory.CreateDirectory(path));

                if (oldMedia != null && oldMedia.Length > 0)
                {
                    oldMedia.ToList().ForEach(o =>
                    {
                        var tempPath = Path.Combine(path, o);

                        if (File.Exists(tempPath))
                            File.Delete(tempPath);
                    });
                }

                path = Path.Combine(path, fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await media.CopyToAsync(fileStream);
                }

            }

            return true;
        }




        private string GetPath(params string[] pathElements)
        {
            var res = string.Empty;
            IEnumerable<string> el;

            if (!string.IsNullOrWhiteSpace(config.Storage))
            {
                el = pathElements.Prepend(config.Storage);
            }
            else
            {
                el = pathElements.ToList();
            }
            switch (config.StorageType)
            {
                case Contracts.Enums.EStorageType.HttpServer:
                    res = Path.Combine(el.ToArray());
                    break;
                case Contracts.Enums.EStorageType.LocalServerWwwRoot:
                    el = el.Prepend(_environment.WebRootPath);
                    res = Path.Combine(el.ToArray());
                    break;
                default:
                    break;
            }
            return res;
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
