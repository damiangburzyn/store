﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Store.Contracts;
using Microsoft.AspNetCore.Authorization;
using Store.Services;
using Store.Contracts.ViewModel;
using Store.Data.EF.Entities;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController<Product, ProductViewModel>
    {
        private readonly IProductService productService;
        private readonly StorageHelper storageHelper;

        public ProductsController(
            IOptions<AppSettings> settings, ILocalPageData pageData, IMapper mapper, IProductService productService, IMediaService mediaService, StorageHelper storageHelper)
             : base(settings, pageData, mapper, productService, mediaService)
        {
            this.productService = productService;
            this.storageHelper = storageHelper;
        }








        [HttpGet("search")]
        public override async Task<IActionResult> Search(string query, string culture = null, bool withCount = false, int? page = null, int? pageSize = null)
        {
            return await this.WrapExceptionAsync(async () =>
            {

                var result = await productService.Search();
                var vm = Mapper.Map<DataTableSearchViewModel<ProductViewModel>>(result);
                return Ok(vm);
            });
        }




        [HttpPost]
        public override async Task<ActionResult<ProductViewModel>> Create(ProductViewModel viewModel)
        {
            var model = Mapper.Map<Product>(viewModel);
            var result = await _service.Add(model);
            viewModel.Id = result.Id;
            foreach (var item in viewModel.Attachements)
            {
                await _mediaService.SaveMedia(item, storageHelper.CrateContainer<ProductViewModel>(viewModel));
            }
            foreach (var item in viewModel.Images)
            {
                await _mediaService.SaveMedia(item, storageHelper.CrateContainer<ProductViewModel>(viewModel));
            }
            var vm = Mapper.Map<ProductViewModel>(result);
            return Ok(vm);
        }

        [HttpPut]
        public override async Task<ActionResult<ProductViewModel>> Update(ProductViewModel viewModel)
        {
            return await this.WrapExceptionAsync(async () =>
            {

                var model = await _service.GetById(viewModel.Id);

                var prevImages = new List<GalleryImage>();

                var prevAttachments = new List<ProductFile>();

                foreach (var item in model.Images)
                {
                    prevImages.Add(item);
                }
                foreach (var item in model.ProductFiles)
                {
                    prevAttachments.Add(item);
                }

                Mapper.Map<ProductViewModel, Product>(viewModel, model);
                var result = await _service.Update(model);


                var partialPath = storageHelper.CrateContainer<ProductViewModel>(viewModel);

                foreach (var item in viewModel.Attachements)
                {
                    var prevAttach = prevAttachments.FirstOrDefault(a => a.File.FileName == item.Name)?.File?.FileName;
                    await _mediaService.SaveMedia(item, partialPath, null, prevAttach);
                }
                foreach (var item in viewModel.Images)
                {
                    var prevImg = prevImages.FirstOrDefault(a => a.Name == item.Name)?.Name;
                    await _mediaService.SaveMedia(item, partialPath, null, prevImg);
                }

                foreach (var item in prevImages)
                {
                    if (!viewModel.Images.Any(a => a.Name == item.Name))
                    {
                      await  _mediaService.DeleteMedia(item.Name, partialPath);
                    }
                }

                foreach (var item in prevAttachments)
                {
                    if (!viewModel.Attachements.Any(a => a.Name == item.File.FileName))
                    {
                      await  _mediaService.DeleteMedia(item.File.FileName , partialPath);
                    }
                }


                var vm = Mapper.Map<ProductViewModel>(result);
                return Ok(vm);
            });
        }


    }
}